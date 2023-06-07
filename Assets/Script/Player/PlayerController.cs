using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator animator;

    [SerializeField] private AudioSource footstep;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private int maxJumpCount = 2;
    private float dirX = 0f;
    private bool isFacingRight = true;
    private int jumpCount = 0;

    private enum MovementState { idle, run, jump, fall }
    [SerializeField] private ParticleSystem doubleJumpParticles;

 
    void Start()
    {
        doubleJumpParticles.Stop();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
            PlayerMovement();
        
    }

    private void CheckInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                jumpCount = 1;
                AudioManager.instance.Play("FirstJump");

            }
            else if (jumpCount < maxJumpCount)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                jumpCount++;
                AudioManager.instance.Play("SecondJump");

                PlayDoubleJumpParticles();
            }
        }
    }
    private void PlayDoubleJumpParticles()
    {
        if (doubleJumpParticles != null)
        {
            doubleJumpParticles.Play();
        }
    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && dirX < 0)
        {

            Flip();
        } 
        else if (!isFacingRight && dirX > 0)
        {

            Flip();
        }
    }

    private void PlayerMovement()
    {

        body.velocity = new Vector2(dirX * speed, body.velocity.y);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (IsGrounded())
        {
            if (dirX > 0f)
            {
                state = MovementState.run;


            }
            else if (dirX < 0f)
            {
                state = MovementState.run;


            }
            else
            {
                state = MovementState.idle;
            }
            animator.SetInteger("State", (int)state);
        }
        else
        {
            if (body.velocity.y > 0.1f)
            {
                state = MovementState.jump;
            }
            else 
            {
                state = MovementState.fall;
            }
            animator.SetInteger("State", (int)state);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
    }
    
    private void FootStep()
    {
        footstep.Play();
    }

}
