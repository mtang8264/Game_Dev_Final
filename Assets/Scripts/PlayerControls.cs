using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool active;
    public Vector2 velocity;
    [Header("PHYSICS")]
    public float horizontalAcceloration;
    public float horizontalVelocityCap;
    [Range(0,1)]
    public float groundDrag;
    [Range(0, 1)]
    public float airDrag;
    public float jumpForce;
    public float gravityAcceloration;
    public bool grounded;
    public float groundCheckDistance;
    public Vector2 groundCheckOffset;
    [Header("REFERENCES")]
    public SpriteRenderer renderer;
    public Animator animator;
    private Collider2D collider;
    private Rigidbody2D rb;
    private bool jumpFlag;
    
    void Start()
    {
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        active = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded && active)
            jumpFlag = true;

        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("direction", true);
        }
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            animator.SetBool("direction", false);
        }

        animator.SetBool("walking", Mathf.Abs(velocity.x) > 0.01f);
        animator.SetBool("grounded", grounded);
    }

    private void FixedUpdate()
    {
        if(velocity.x > horizontalVelocityCap)
        {
            velocity.x = horizontalVelocityCap;
        }
        if (velocity.x < -horizontalVelocityCap)
        {
            velocity.x = -horizontalVelocityCap;
        }

        if (jumpFlag)
        {
            Debug.Log("JUMP");
            jumpFlag = false;
            velocity.y += jumpForce;
        }

        if (grounded)
            velocity.x *= groundDrag;
        else
            velocity.x *= airDrag;

        // Debug.DrawRay(transform.position - new Vector3(groundCheckOffset.x, groundCheckOffset.y), Vector2.down);
        // Debug.DrawRay(transform.position - new Vector3(-groundCheckOffset.x, groundCheckOffset.y), Vector2.down);
        RaycastHit2D left = Physics2D.Raycast(transform.position - new Vector3(groundCheckOffset.x, groundCheckOffset.y), Vector2.down, groundCheckDistance);
        RaycastHit2D right = Physics2D.Raycast(transform.position - new Vector3(-groundCheckOffset.x, groundCheckOffset.y), Vector2.down, groundCheckDistance);
        
        grounded = left.collider != null && right.collider != null && (left.collider.tag == "GROUND" || right.collider.tag == "GROUND");

        if (!grounded)
        {
            velocity.y -= gravityAcceloration * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (active)
        {
            if (Input.GetKey(KeyCode.D))
            {
                velocity.x += horizontalAcceloration * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity.x -= horizontalAcceloration * Time.deltaTime;
            }
        }

        rb.MovePosition((Vector2)transform.position + velocity);
    }
}
