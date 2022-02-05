using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public GameData gameData;

    public float groundCheckRadius;
    public float PlayerSpeed = 10.0f;

    public float jumpForce = 16.0f;
    public float jumpTimerSet = 0.15f;
    public float varibleJumpHeightMultiplier = 0.5f;
    public int amountOfJumps = 2;

    public float turnTimerSet = 0.1f;
    public int facingDirection = 1;





    private int amountOfJumpsLeft;
    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;



    private Animator anim;

    private bool isFacingRight = true;

    private bool isGrounded;
    private bool canNormalJump;
    private bool isAttemptingToJump;
    private bool checkJumpMiultiplier;
    private bool canMove;
    private bool canFlip;
    private bool isWalking;

    public LayerMask whatisGround;



    private void Awake()
    {
      
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckJump();
        MouseClick();
        //UpdateAnimation();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
        
    }



    //private void UpdateAnimation()
    //{
    //    anim.SetBool("isWalking", isWalking);
    //    anim.SetBool("isGrounded", isGrounded);
    //    anim.SetFloat("yVelocity", rb.velocity.y);
    //    anim.SetBool("isWallSlide", isWallSliding);
    //    anim.SetInteger("playermovement", (int)PlayerMovingState);
    //}

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded || (amountOfJumpsLeft>0) || (amountOfJumpsLeft > 0 && !isGrounded))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }
        if(Input.GetButtonDown("Horizontal"))
        {
            if(!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
                
            }
        }

        if (!canMove)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMiultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMiultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varibleJumpHeightMultiplier);
            
        }
    }


    private void ApplyMovement()
    {
        if (!isGrounded && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * varibleJumpHeightMultiplier, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(PlayerSpeed * movementInputDirection, rb.velocity.y);
        }


    }




  

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

    }

    public void Flip()
    {
        if (canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }


    }


   private void CheckJump()//Jump again with a dely input
    {
        if (jumpTimer > 0)
        {
            
            if (isGrounded)
            {
                NormalJump();
            }
        }
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        
    }
    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMiultiplier = true;


        }
    }
    
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
       
    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0.01f))
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }

        else
        {
            canNormalJump = true;
        }
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

 
    private void MouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
          
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                if (distance < gameData.OrangeMagFieldRaidus)
                {
                    rb.AddForce(hit.collider.transform.position.normalized * Mathf.Lerp(0, 20, distance));
                    
                }
                
            }
        }
    }


  
}
