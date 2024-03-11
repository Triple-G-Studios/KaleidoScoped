using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class PlayerController2 : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private float _xRotation;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 5f;
        private Vector2 _currVelocity;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("xVelocity");
            _yVelHash = Animator.StringToHash("yVelocity");
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

            _currVelocity.x = targetSpeed * _inputManager.Move.x;
            _currVelocity.y = targetSpeed * _inputManager.Move.y;

            var xVelDifference = Mathf.Lerp(_currVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            var yVelDifference = Mathf.Lerp(_currVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, yVelDifference)), ForceMode.VelocityChange);

            _animator.SetFloat(_xVelHash, _currVelocity.x);
            _animator.SetFloat(_yVelHash, _currVelocity.y);
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            var xMouse = _inputManager.Look.x;
            var yMouse = _inputManager.Look.y;
            Camera.position = CameraRoot.position;

            _xRotation -= yMouse * MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up, xMouse * MouseSensitivity * Time.deltaTime);
        }
    }
}