using ChacoUtil.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChacoUtil.Input
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : SingletonPersistent<InputManager>
    {
        public delegate void PrimaryActionEvent();
        public event PrimaryActionEvent OnPrimaryAction;
        public delegate void SecondaryActionEvent();
        public event SecondaryActionEvent OnSecondaryAction;
        public delegate void JumpEvent();
        public event JumpEvent OnJump;

        public delegate void StartTouchEvent(Vector2 position, float time);
        public event StartTouchEvent OnStartTouch;
        public delegate void EndTouchEvent(Vector2 position, float time);
        public event EndTouchEvent OnEndTouch; 
        
        private PlayerControls _playerControls;
        private bool _isMoving;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Default.Movement.started += ctx => _isMoving = true;
            _playerControls.Default.Movement.canceled += ctx => _isMoving = false;
            _playerControls.Default.PrimaryAction.started += PrimaryAction;
            _playerControls.Default.SecondaryAction.started += SecondaryAction;
            _playerControls.Default.Jump.started += Jump;

            _playerControls.Touch.TouchPress.started += StartTouch;
            _playerControls.Touch.TouchPress.canceled += EndTouch;
        }

        private void Update()
        {
            if (_isMoving)
            {
                Debug.Log(GetMovement());
            }
        }
        
        public Vector2 GetMovement()
        {
            return _playerControls.Default.Movement.ReadValue<Vector2>();
        }

        private void PrimaryAction(InputAction.CallbackContext context)
        {
            OnPrimaryAction?.Invoke();
        }
        
        private void SecondaryAction(InputAction.CallbackContext obj)
        {
            OnSecondaryAction?.Invoke();
        }
        
        private void Jump(InputAction.CallbackContext obj)
        {
            OnJump?.Invoke();
        }
        
        private void StartTouch(InputAction.CallbackContext context)
        {
            Vector2 touchPosition = _playerControls.Touch.TouchPosition.ReadValue<Vector2>();
            OnStartTouch?.Invoke(touchPosition, (float)context.startTime);
        }
        
        private void EndTouch(InputAction.CallbackContext context)
        {
            Vector2 touchPosition = _playerControls.Touch.TouchPosition.ReadValue<Vector2>();
            OnEndTouch?.Invoke(touchPosition, (float)context.time);
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
        
    }
}