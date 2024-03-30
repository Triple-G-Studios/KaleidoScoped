using UnityEngine;

namespace Kaleidoscoped
{
	public class FPSController : MonoBehaviour
	{
		[SerializeField] private Transform CameraRoot;
		[SerializeField] private Transform Camera;
		private Animator _animator;
		public Transform projectileOrigin;
		public GameObject projectilePrefab;
		public ProjectilePool projectilePool;
		public GameObject PaintballGun;
		public GameObject pauseMenuUI;

		//State Tracking
		public string currentColor = "";
		public Color color = Color.blue;
		public string weapon = "rifle";

		[SerializeField] private float UpperLimit = -40f;
		[SerializeField] private float BottomLimit = 70f;
		public static float mouseSensitivity = 300f;
		[SerializeField] private float AnimBlendSpeed = 8.9f;

		private float _xRotation;
		private bool _hasAnimator;
		private const float _walkSpeed = 2f;
        private const float _runSpeed = 5f;
        private Vector2 _currVelocity;
        private int _xVelHash;
        private int _yVelHash;

		private Rigidbody _playerRigidbody;
		private InputManager _input;

		private void Start()
		{
			_input = GetComponent<InputManager>();
			_playerRigidbody = GetComponent<Rigidbody>();
			_hasAnimator = TryGetComponent<Animator>(out _animator);
			_xVelHash = Animator.StringToHash("xVelocity");
			_yVelHash = Animator.StringToHash("yVelocity");
			pauseMenuUI.SetActive(false);
		}

		private void FixedUpdate()
		{
			Move();
			Pause();
		}

		private void LateUpdate()
		{
			CamMovements();
		}

		public void Pause()
		{
			if (_input.Pause)
			{
				pauseMenuUI.SetActive(true);
				Time.timeScale = 0f;
				MenuController.isPaused = true;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		private void CamMovements()
		{
			if (!_hasAnimator) return;

			var xMouse = _input.Look.x;
			var yMouse = _input.Look.y;
			Camera.position = CameraRoot.position;

			_xRotation -= yMouse * mouseSensitivity * Time.smoothDeltaTime;
			_xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

			Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
			_playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, xMouse * mouseSensitivity * Time.smoothDeltaTime, 0));
		}

		private void Move()
		{
			if (!_hasAnimator) return;

			float targetSpeed = _input.Run ? _runSpeed : _walkSpeed;
			if (_input.Move == Vector2.zero) targetSpeed = 0;

			_currVelocity.x = targetSpeed * _input.Move.x;
			_currVelocity.y = targetSpeed * _input.Move.y;

			var xVelDifference = Mathf.Lerp(_currVelocity.x, _input.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
			var yVelDifference = Mathf.Lerp(_currVelocity.y, _input.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

			_playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, yVelDifference)), ForceMode.VelocityChange);

			_animator.SetFloat(_xVelHash, _currVelocity.x);
			_animator.SetFloat(_yVelHash, _currVelocity.y);
		}

		private void OnShoot()
		{
			//GameObject projectile = projectilePool.GetProjectile();
			//projectile.transform.position = projectileOrigin.position;
			//projectile.transform.rotation = Quaternion.LookRotation(CameraRoot.forward);

			//Projectile projectileScript = projectile.GetComponent<Projectile>();
			//if (projectileScript != null)
			//{
			//	projectileScript.Initialize(projectilePool, CameraRoot.forward, 75f, color, currentColor);
			//}

			GameObject projectile = projectilePool.GetProjectile();

			Vector3 shootPosition = CameraRoot.position + CameraRoot.forward *  1;

			projectile.transform.position = shootPosition;

			projectile.transform.rotation = Quaternion.LookRotation(CameraRoot.forward);

			Projectile projectileScript = projectile.GetComponent<Projectile>();
			if (projectileScript != null)
			{
				projectileScript.Initialize(projectilePool, CameraRoot.forward, 75f, color, currentColor);
			}
		}
	}
}
