using Cinemachine;
using Mirror;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using Kaleidoscoped;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class LukePlayerMovement : NetworkBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 4.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Rotation speed of the character")]
        public float RotationSpeed = 1.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.1f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.5f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;

        // cinemachine
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        // NEW STUFF

        public Transform povOrigin;
        public Transform projectileOrigin;
        public GameObject projectilePrefab;

        public ProjectilePool projectilePool;

        public string currentColor = "blue";
        public Color color = Color.blue;

        public string weapon = "rifle";

        public GameObject pauseMenuUI;
        public GameObject pauseMenuPrefab;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        public override void OnStartLocalPlayer()
        {
            GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0).transform;
            transform.GetComponent<LukePlayerMovement>().povOrigin = GameObject.FindGameObjectWithTag("PovOrigin").transform;
            transform.GetComponent<LukePlayerMovement>().projectileOrigin = GameObject.FindGameObjectWithTag("ProjectileOrigin").transform;
            transform.GetComponent<LukePlayerMovement>().projectilePool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ProjectilePool>();

            // transform.GetComponent<LukePlayerMovement>().pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenu");
            GameObject pauseMenu = Instantiate(pauseMenuPrefab);
            transform.GetComponent<LukePlayerMovement>().pauseMenuUI = pauseMenu;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.enabled = true;
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            transform.GetComponent<PlayerHealth>().respawnManager = GameObject.FindGameObjectWithTag("RespawnManager").GetComponent<RespawnManager>();
            transform.GetComponent<PlayerHealth>().respawnMessageController = GameObject.FindGameObjectWithTag("RespawnMessageController").GetComponent<RespawnMessageController>();
            GameObject.FindGameObjectWithTag("RespawnManager").GetComponent<RespawnManager>().respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;

        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            // if there is an input
            if (_input.look.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

                // clamp our pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Update Cinemachine camera target pitch
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                // move
                inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
            }

            // move the player
            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        // private void OnPrimaryAttack()
        // {
        //     if (!isLocalPlayer)
        //     {
        //         return;
        //     }

        //     if (weapon == "shotgun")
        //     {
        //         GameObject projectile1 = projectilePool.GetProjectile();
        //         projectile1.transform.position = projectileOrigin.position;
        //         projectile1.transform.rotation = Quaternion.LookRotation(povOrigin.forward) * Quaternion.Euler(Vector3.forward * 10);

        //         GameObject projectile2 = projectilePool.GetProjectile();
        //         projectile2.transform.position = projectileOrigin.position;
        //         projectile2.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

        //         GameObject projectile3 = projectilePool.GetProjectile();
        //         projectile3.transform.position = projectileOrigin.position;
        //         projectile3.transform.rotation = Quaternion.LookRotation(povOrigin.forward) * Quaternion.Euler(Vector3.forward * -10);

        //         Projectile projectileScript1 = projectile1.GetComponent<Projectile>();
        //         Projectile projectileScript2 = projectile2.GetComponent<Projectile>();
        //         Projectile projectileScript3 = projectile3.GetComponent<Projectile>();

        //         if (projectileScript1 != null && projectileScript2 != null && projectileScript3 != null)
        //         {
        //             projectileScript1.Initialize(projectilePool, Quaternion.AngleAxis(10, Vector3.up) * povOrigin.forward, 15f, color, currentColor);
        //             projectileScript2.Initialize(projectilePool, povOrigin.forward, 15f, color, currentColor);
        //             projectileScript3.Initialize(projectilePool, Quaternion.AngleAxis(-10, Vector3.up) * povOrigin.forward, 15f, color, currentColor);
        //         }
        //     }
        //     if (weapon == "rifle")
        //     {
        //         GameObject projectile = projectilePool.GetProjectile();
        //         projectile.transform.position = projectileOrigin.position;
        //         projectile.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

        //         Projectile projectileScript = projectile.GetComponent<Projectile>();

        //         if (projectileScript != null) projectileScript.Initialize(projectilePool, povOrigin.forward, 75f, color, currentColor);
        //     }

        // }

        [Command]
        private void CmdOnPrimaryAttack(Vector3 position, Quaternion rotation, Vector3 forward, GameObject shooter)
        {
            // var projectilePoolObj = NetworkServer.spawned[poolNetId].gameObject.GetComponent<ProjectilePool>();
            // if (projectilePoolObj != null)
            // {
            //     var projectile = projectilePoolObj.GetProjectile();
            //     projectile.transform.position = position;
            //     projectile.transform.rotation = rotation;
            //     NetworkServer.Spawn(projectile);
            //     // Additional setup for the projectile as necessary

            //     Projectile projectileScript = projectile.GetComponent<Projectile>();
            //     if (projectileScript != null) projectileScript.Initialize(projectilePoolObj, forward, 75f, color, currentColor);
            // }





            // GameObject projectile = projectilePool.GetProjectile();
            // projectile.transform.position = position;
            // projectile.transform.rotation = rotation;
            // NetworkServer.Spawn(projectile);

            // Projectile projectileScript = projectile.GetComponent<Projectile>();
            // if (projectileScript != null) projectileScript.Initialize(projectilePool, forward, 75f, color, currentColor);

            RpcOnPrimaryAttackEffects(position, rotation, forward, shooter);
        }


        [ClientRpc]
        void RpcOnPrimaryAttackEffects(Vector3 position, Quaternion rotation, Vector3 forward, GameObject shooter)
        {
            // Handle sounds here I think
            GameObject projectileInstance = Instantiate(projectilePrefab, position, rotation);
            ProjectileNoPool projectileScript = projectileInstance.GetComponent<ProjectileNoPool>();

            // NetworkServer.Spawn(projectileInstance);

            if (projectileScript != null)
            {
                projectileScript.Initialize(forward, 75f, color, currentColor, gameObject);
            }

        }

        private void OnPrimaryAttack()
        {
            if (!isLocalPlayer) return;

            // var poolNetId = projectilePool.GetComponent<NetworkIdentity>().netId;

            CmdOnPrimaryAttack(projectileOrigin.position, Quaternion.LookRotation(povOrigin.forward), povOrigin.forward, gameObject);
            print(gameObject);
        }


        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }
    }
}