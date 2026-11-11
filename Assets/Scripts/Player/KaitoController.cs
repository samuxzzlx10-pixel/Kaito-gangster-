using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class KaitoController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Min(0f)] private float moveSpeed = 4.5f;
    [SerializeField, Min(0f)] private float sprintMultiplier = 1.5f;
    [SerializeField, Min(0f)] private float turnSpeed = 12f;
    [SerializeField] private float gravity = -20f;

    private CharacterController characterController;
    private Vector2 moveInput;
    private bool sprintHeld;
    private float verticalVelocity;

    public void SetMoveInput(Vector2 input) => moveInput = Vector2.ClampMagnitude(input, 1f);
    public void SetSprintHeld(bool held) => sprintHeld = held;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 forward = cameraTransform != null ? cameraTransform.forward : transform.forward;
        Vector3 right = cameraTransform != null ? cameraTransform.right : transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.ClampMagnitude(forward * moveInput.y + right * moveInput.x, 1f);
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * Time.deltaTime);
        }

        if (characterController.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        float speed = moveSpeed * (sprintHeld ? sprintMultiplier : 1f);
        Vector3 velocity = direction * speed;
        velocity.y = verticalVelocity;
        characterController.Move(velocity * Time.deltaTime);
    }
}
