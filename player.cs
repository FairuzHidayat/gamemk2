using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Pengaturan Gerak")]
    public float speed = 8f;
    public float jumpForce = 12f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    [SerializeField] private Rigidbody2D rb;

    void Awake()
    {
        // Auto-assign Rigidbody2D if not set in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // Auto-assign groundCheck if not set in Inspector
        if (groundCheck == null)
        {
            Transform existing = transform.Find("GroundCheck");
            if (existing != null)
            {
                groundCheck = existing;
            }
            else
            {
                GameObject gc = new GameObject("GroundCheck");
                gc.transform.SetParent(transform);
                gc.transform.localPosition = new Vector2(0, -1f); // adjust Y to fit your sprite
                groundCheck = gc.transform;
            }
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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