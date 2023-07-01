using UnityEngine;

namespace Player
{
    public class FirstPersonMovement : MonoBehaviour
    {
        [SerializeField] private float acceleration = 100f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float gravity = 9.81f;

        private CharacterController _characterController;
        private Transform _cameraTransform;
        private float _verticalRotation = 0f;
        private Vector3 _currentVelocity;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _cameraTransform = transform.GetChild(0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;

            float desiredSpeed = movement.magnitude * speed;
            _currentVelocity = Vector3.Lerp(_currentVelocity, movement * desiredSpeed, Time.deltaTime * acceleration);

            _currentVelocity.y = _characterController.isGrounded ? 0f : _currentVelocity.y - (gravity * Time.deltaTime * 10);

            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, maxSpeed);

            _characterController.Move(_currentVelocity * Time.deltaTime);
        
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }
    }
}
