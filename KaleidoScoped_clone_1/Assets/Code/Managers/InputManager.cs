using UnityEngine;
using UnityEngine.InputSystem;

namespace Kaleidoscoped
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool Jump { get; private set; }
        public bool Shoot { get; private set; }
        public bool Pause { get; private set; }

        // Player Actions
        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _shootAction;
        private InputAction _pauseAction;
        private InputAction _jumpAction;

        // Start is called before the first frame update
        private void Awake()
        {
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _shootAction = _currentMap.FindAction("Shoot");
            _pauseAction = _currentMap.FindAction("Pause");

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun;
            _jumpAction.performed += onJump;
            _shootAction.performed += onShoot;
            _pauseAction.performed += onPause;

            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;
            _jumpAction.canceled += onJump;
            _shootAction.canceled += onShoot;
            _pauseAction.canceled += onPause;

        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void onShoot(InputAction.CallbackContext context)
        {
            Shoot = context.ReadValueAsButton();
        }

        private void onJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }

        private void onPause(InputAction.CallbackContext context)
        {
            Pause = context.ReadValueAsButton();
        }
    }
}