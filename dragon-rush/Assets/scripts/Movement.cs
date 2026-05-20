using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField] AudioClip jumpSFX;
    [SerializeField] ParticleSystem jumpparticles;
    Animator anim;
    private bool canDoubleJump;



    Rigidbody rb;
    Vector2 playerVelocity;
    AudioSource AudioSource;
    internal static float movespeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        jump.Enable();
        move.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
        move.Disable();
    }

    private void Start()
    {
        jump.performed += ctx => HandleJump();
    }
    private void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        HandleMove();
    }
    private void HandleJump()
    {
        if (isGrounded || canDoubleJump)
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(jumpSFX);
            }
            jumpparticles.Play();

            playerVelocity = rb.linearVelocity;
            playerVelocity.y = 0f;
            rb.linearVelocity = playerVelocity;

            rb.AddForce(Vector2.up * jumpforce, ForceMode.Impulse);
            canDoubleJump = !canDoubleJump;
        }
        else
        {
            AudioSource.Stop();
            jumpparticles.Stop();
        }
    

    }
    private void HandleMove()
    {
        float moveInput = move.ReadValue<float>();
        playerVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;

        anim.SetBool("IsWalking", playerVelocity != Vector2.zero);

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



