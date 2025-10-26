using UnityEngine;
using Mono.Cecil;

public class PlayerMoveControls : MonoBehaviour
{

    public float speed = 5f;
    private int direction = 1;
    public float jumpForce = 5;
    private bool grounded = false;

    private GatherInput gatherInput;
    private Rigidbody2D rigidbody2;
    private Animator animator;
    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        SetAnimatorValues();
        Flip();
        rigidbody2.linearVelocity = new Vector2(speed * gatherInput.valueX,
            rigidbody2.linearVelocity.y);
        JumpPlayer();
        Debug.Log("valueX: " + gatherInput.valueX);
    }
    public Transform leftPoint;
    public float rayLength;
    public LayerMask groundLayer;
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position,
            Vector2.down, rayLength, groundLayer);

        grounded = leftCheckHit;
        print(grounded);
    }
    private void JumpPlayer()
    {
        if (gatherInput.jumpInput)
        {
            rigidbody2.linearVelocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }
    private void SetAnimatorValues()
    {
        animator.SetFloat("speed", Mathf.Abs(rigidbody2.linearVelocityX));
        animator.SetFloat("vSpeed", rigidbody2.linearVelocityY);
        animator.SetBool("ground", grounded);
    }
    /// <summary>
    /// for flip character
    /// </summary>
    private void Flip()
    {
        if (gatherInput.valueX * direction < 0)
        {
            direction *= -1;
            transform.localScale = new Vector3(originalScale.x * direction, originalScale.y, originalScale.z);
        }
    }
         
}
