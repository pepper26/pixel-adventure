using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    PlayerControls controls;
    float Direction;
    //float moveX;
    private SpriteRenderer spriteRenderer;


    [SerializeField] private LayerMask GroundLayerMask;
    public Transform GroundCheck;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float doublejumpForce = 6f;
    bool facingRight = true;

    [SerializeField] private bool canDoubleJump = false;

    private float wallSlideSpeed = 1;
    private bool isWallSliding = false;
    public Transform wallCheckPoint;

    private bool isWalljumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public static int targetFrameRate;

    [SerializeField] ParticleSystem wallslidingParticle;

    audiomanager audiomanager;

    private enum MovementState { idle, run, jump, fall, doublejump, wallsliding };
    private MovementState state = MovementState.idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        Application.targetFrameRate = 300;

    }

    // Update is called once per frame
    void Update()
    {
        //moveX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(Direction  * moveSpeed, rb.velocity.y);
        Jump();
        updateAnimationState();
        Flip();
        WallSliding();
        WallJump();
    }

    void Flip()
    {
        if (Direction > 0 && !facingRight || Direction < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded()|| Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            audiomanager.PlaySFX(audiomanager.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump || Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            audiomanager.PlaySFX(audiomanager.jump);
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, doublejumpForce);
        }
    }
    void updateAnimationState()
    {
        MovementState state;
        if (Direction < 0f && IsGrounded())
        {
            state = MovementState.run;
        }
        else if (Direction > 0f && IsGrounded())
        {
            state = MovementState.run;
        }
        else if (rb.velocity.y > 0.1f && canDoubleJump == false)
        {
            state = MovementState.doublejump;
        }
        else if (canDoubleJump && rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (IsWalled() && !IsGrounded())
        {
            state = MovementState.wallsliding;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("State", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(GroundCheck.position, new Vector2(0.49f, 0.04f), 0.1f, GroundLayerMask);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapBox(wallCheckPoint.position,new Vector2(0.07f, 0.6f), 0.5f, GroundLayerMask);
    }
    private void WallSliding()
    {
        if (IsWalled() && !IsGrounded())
        {
            wallslidingParticle.Play();
            isWallSliding = true;
            rb.velocity = new Vector2(0f, -wallSlideSpeed);
        }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallSliding = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && Direction == 0)
        {
            isWalljumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * 3.5f, 8f);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            else
            {
                isWallSliding = false;
            }
        }
    }
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();

        controls = new PlayerControls();
        controls.Enable();

        controls.land.move.performed += ctx =>
        {
            Direction = ctx.ReadValue<float>();
        };
        controls.land.Jump.performed += ctx => jump();

    }
    public void jump()
    {
        if (IsGrounded())
        {
            audiomanager.PlaySFX(audiomanager.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            audiomanager.PlaySFX(audiomanager.jump);
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, doublejumpForce);
        }

        if (isWallSliding)
        {
            isWallSliding = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (wallJumpingCounter > 0f && Direction == 0)
        {
            isWalljumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * 3.5f, 8f);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            else
            {
                isWallSliding = false;
            }
        }
    }

}
