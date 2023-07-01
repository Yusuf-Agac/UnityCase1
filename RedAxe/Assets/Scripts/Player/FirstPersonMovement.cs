using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float gravity = 9.81f;

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalRotation = 0f;
    private Vector3 currentVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = transform.GetChild(0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;

        float desiredSpeed = movement.magnitude * speed;
        currentVelocity = Vector3.Lerp(currentVelocity, movement * desiredSpeed, Time.deltaTime * acceleration);

        currentVelocity.y = characterController.isGrounded ? 0f : currentVelocity.y - (gravity * Time.deltaTime * 10);

        currentVelocity = Vector3.ClampMagnitude(currentVelocity, maxSpeed);

        characterController.Move(currentVelocity * Time.deltaTime);
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
