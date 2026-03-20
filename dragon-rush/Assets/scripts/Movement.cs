using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction jump;

    [SerializeField] float jumpforce = 100f;
    
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        jump.Enable();
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
    }
}
