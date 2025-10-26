using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Patrol();
        animator.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));
    }

    void Patrol()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        bool reachedRight = movingRight && transform.position.x >= rightLimit.position.x;
        bool reachedLeft = !movingRight && transform.position.x <= leftLimit.position.x;
        bool noGroundAhead = !IsGroundAhead();

        if (reachedRight || reachedLeft || noGroundAhead)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !movingRight; // หันขวาเมื่อ movingRight = true
    }

    bool IsGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.3f, Color.red);
        return hit.collider != null;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHP player = collision.collider.GetComponent<PlayerHP>();
        if (player != null)
        {
            player.ApplyDamage(10);
        }
    }
}
