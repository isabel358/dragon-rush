using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction jump;
    [SerializeField] float jumpforce = 100f;

    [SerializeField] InputAction move;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    Rigidbody rb;
    Vector2 playerVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        jump.Enable();
        move.Enable();
    }
    private void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleJump();
    }
    private void HandleJump()
    {
        if (jump.IsPressed() && isGrounded)
        {
            //rb.AddRelativeForce(Vector3.up * jumpforce * Time.fixedDeltaTime);

            Vector2 playerVelocity = rb.linearVelocity;
            playerVelocity.y = 0f;
            rb.linearVelocity = playerVelocity;

            rb.AddForce(Vector2.up * jumpforce, ForceMode.Impulse);
        }
    }
    
    private void HandleMove()
    {
        float moveInput = move.ReadValue<float>();
        playerVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;

        if (moveInput != 0)
        {
            float yRotation = moveInput > 0 ? 0f : 180f;
            rb.MoveRotation(Quaternion.Euler(0, yRotation, 0));
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
    }

}
    
    

