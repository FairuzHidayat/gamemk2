using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Pengaturan Gerak")]
    public float speed = 8f;
    public float jumpForce = 12f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    private bool isGrounded;

    [SerializeField] private Rigidbody2D rb;
    private Collider2D playerCollider; // Tambahan untuk membaca collider utama player

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // Mengambil komponen Collider2D yang ada di objek Mario
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Mengecek apakah collider Mario sedang menyentuh layer tanah
        if (playerCollider != null)
        {
            isGrounded = playerCollider.IsTouchingLayers(groundLayer);
        }

        // Pengecekan Debug di Console
        Debug.Log("isGrounded: " + isGrounded);

        // Hanya melompat jika menyentuh tanah
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Fitur kontrol tinggi lompatan (staccato jump)
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}