using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction jump;

    [SerializeField] float jumpforce = 100f;

    [SerializeField] InputAction Sidejump;
    [SerializeField] float Sidejumpforce = 100f;
    
    [SerializeField] InputAction rightjump;
    [SerializeField] float rightjumpforce = 100f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        jump.Enable();
        Sidejump.Enable();
        rightjump.Enable();
    }

    private void FixedUpdate()
    {
        if (jump.IsPressed())
        {
            //rb.AddRelativeForce(Vector3.up * jumpforce * Time.fixedDeltaTime);

            Vector2 playerVelocity = rb.linearVelocity;
           playerVelocity.y = 0f;
            rb.linearVelocity = playerVelocity;

            rb.AddForce(Vector2.up * jumpforce, ForceMode.Impulse);
        }
        if (Sidejump.IsPressed())
        {
            Vector2 playerVelocity = rb.linearVelocity;
            playerVelocity.x = 0f;
            rb.linearVelocity = playerVelocity;

            rb.AddForce(Vector2.left * jumpforce, ForceMode.Impulse);
        }
        if(rightjump.IsPressed())
        {
            Vector2 playerVelocity = rb.linearVelocity;
            playerVelocity.x = 0f;
            rb.linearVelocity = playerVelocity;

            rb.AddForce(Vector2.right * rightjumpforce, ForceMode.Impulse);
        }
    }
}
