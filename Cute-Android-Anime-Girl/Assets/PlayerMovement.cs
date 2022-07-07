using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] Idle;
    public Sprite[] Walk;
    public Sprite[] jump;
   public Sprite currentSprite;
    public Rigidbody2D rb;
    SpriteRenderer Renderer;
    public GameObject groundcheck;
    [SerializeField] LayerMask Ground;
    float horizontalMove;
    [SerializeField] float speed = 5f;
    bool isFacingRight = true;
    public float JumpPower = 5f;

    public bool isDashing = false;
    bool CanDash = true;
    public float DashCoolDown = 1f;
    public float DashSpeed = 10f;
    public float DashDuration = 1f;
    public Vector3 localScale;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = FindObjectOfType<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if(!isDashing)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            Debug.Log(rb.velocity);
            Flip();
            Jump();
            AnimationSetter();
        }
        DashSequences();
    }
    void DashSequences()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            StartCoroutine("Dash");
        }
    }
    IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        float realGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * DashSpeed , 0f);
        yield return new WaitForSeconds(DashDuration);
        isDashing = false;
        rb.gravityScale = realGravity;
        yield return new WaitForSeconds(DashCoolDown);
        CanDash = true;
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.transform.position , 0.2f , Ground);
    }
    void Flip()
    {
        if(isFacingRight && horizontalMove < 0 || !isFacingRight && horizontalMove > 0)
        {
            isFacingRight = !isFacingRight;
            localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    void Jump()
    {
        if(Input.GetButton("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.5f , JumpPower);
        }
    }
    void AnimationSetter()
    {
        if(rb.velocity.magnitude == 0)
        {
            animator.AnimationSwapper(Idle);
        }
        if(rb.velocity.x != 0 && rb.velocity.y == 0)
        {
            animator.AnimationSwapper(Walk);
        }
        if(rb.velocity.y != 0)
        {
            animator.AnimationSwapper(jump);
        }
    }
}
