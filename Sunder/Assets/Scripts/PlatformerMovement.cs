using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement")]
    public float speed = 8f;
    public float jumpingPower = 16f;
    public int MaxJumpCount = 1;
    public float accelerationRate = 5f;
    public float maxSpeedX = 10f;
    float timer2;

    private float horizontal;

    private bool isFacingRight = true;
   
    private int jumpCount = 0;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    AudioSource audioSource;

    [Header("Wall Customization")]
    public float wallSlidingSpeed = 1.5f;
    public float wallJumpingDuration = 0.6f;
    public float wallJumpingTime = 0.8f;
    
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;
   
    private Vector2 wallJumpingPower = new Vector2(6f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private InputActionReference moveActionToUse;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }
    private void Update()
    {
        
        timer2 += Time.deltaTime;
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 moveInput = moveActionToUse.action.ReadValue<Vector2>();
        horizontal += moveInput.x;
        if (horizontal == Input.GetAxisRaw("Horizontal"))
        {
            audioSource.PlayOneShot(walkSound);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < MaxJumpCount)
        {
            if (audioSource != null && jumpSound != null)
            {
                //play the jump sound
                audioSource.PlayOneShot(jumpSound);
            }
            jumpCount++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (IsGrounded())
        {
            jumpCount = 0;
        }
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            Vector2 currentVelocity = rb.velocity;

            // Gradually increase the X-component towards maxSpeedX
            // You can adjust the direction (e.g., Input.GetAxis("Horizontal") for player input)
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, maxSpeedX, accelerationRate * Time.fixedDeltaTime);

            // Apply the modified velocity back to the Rigidbody2D
            rb.velocity = currentVelocity;
        }
    }

   

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            jumpCount = 0;
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            if (audioSource != null && jumpSound != null)
            {
                //play the jump sound
                audioSource.PlayOneShot(jumpSound);
            }
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}