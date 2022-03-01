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

    public float MagFieldRaidus = 5.0f;

    public bool inMagneticZone = false;

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

    private float origGravityScale = 0.0f;
    private float selfMagneticScale = 0.0f;
    private float moveForce = 0.0f;

    private void Awake()
    {
      
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        origGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckJump();
        MouseClick();
        applyMagneticZoneForceUp();
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
<<<<<<< Updated upstream
=======

        moveForce = inZeroGravityZone ? zeroGravMoveForce : normalMoveForce;
        rb.gravityScale = inZeroGravityZone ? 0f : origGravityScale;
        rb.drag = inZeroGravityZone ? zeroGravLinearDrag : origLinearDrag;
        rb.angularDrag = inZeroGravityZone ? zeroGravAngularDrag : origAngularDrag;
        
>>>>>>> Stashed changes
    }


    private void ApplyMovement()
    {
        if (!isGrounded && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * varibleJumpHeightMultiplier, rb.velocity.y);
        }
        else if (canMove)
        {
<<<<<<< Updated upstream
            rb.velocity = new Vector2(PlayerSpeed * movementInputDirection, rb.velocity.y);
=======
            Debug.Log("move inside zero gravity");
            rb.gravityScale = inZeroGravityZone ? 0f : origGravityScale;
            rb.drag = inZeroGravityZone ? zeroGravLinearDrag : origLinearDrag;
            rb.angularDrag = inZeroGravityZone ? zeroGravAngularDrag : origAngularDrag;
            Debug.Log("player gravity scale in zero gravity is " + rb.gravityScale);
            float h = Input.GetAxisRaw("Horizontal") * moveForce;
            float v = inZeroGravityZone ? Input.GetAxisRaw("Vertical") * moveForce : 0f;
            rb.AddForce(new Vector2(h, v));
            rb.AddTorque(-rotForce);
>>>>>>> Stashed changes
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
        Gizmos.DrawWireSphere(transform.position, MagFieldRaidus);

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

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 position = transform.position;
            Vector2 direction = mousePosition - position;


            RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Positive")
                {
                    Debug.Log("You selected the " + hit.transform.name);
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if (distance <= MagFieldRaidus)
                    {
                        if(transform.position.y> hit.point.y)
                        {
                            rb.AddForce(direction.normalized * Mathf.Lerp(0,10, distance));
                        }
                        else
                        {
                            rb.AddForce(direction.normalized * Mathf.Lerp(0, 10, distance));
                        }
                    }

                }
                else if (hit.collider.tag == "Negative")
                {
                    Debug.Log("You selected the " + hit.transform.name);
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if (distance <= MagFieldRaidus)
                    {
                        
                        rb.AddForce(direction.normalized * -(Mathf.Lerp( 0,10, distance)));
                       
                    }

                }
            }
        }

        if (Input.GetMouseButton(1))
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 position = transform.position;
            Vector2 direction = mousePosition - position;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Positive")
                {
                    Debug.Log("You selected the " + hit.transform.name);
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if (distance <= MagFieldRaidus)
                    {
                        rb.AddForce(direction.normalized * -(Mathf.Lerp(0, 10, distance)));
                    }

                }
                else if (hit.collider.tag == "Negative")
                {
                    Debug.Log("You selected the " + hit.transform.name);
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if (distance <= MagFieldRaidus)
                    {
                        rb.AddForce(direction.normalized * Mathf.Lerp(0, 10, distance));
                    }

                }else if (hit.collider.tag == "PositiveDynamic" || hit.collider.tag == "NegativeDynamic")
                {
                    Debug.Log("You selected the " + hit.transform.name);
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if (distance <= MagFieldRaidus)
                    {
                        hit.collider.attachedRigidbody.AddForce(-direction.normalized * Mathf.Lerp(0, 20, distance));
                    }
                }
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Positive"){
            Debug.Log("Enter magnetic zone");
            inMagneticZone = true;
            GameObject gb = other.gameObject;
            MagneticZone zone = gb.GetComponent<MagneticZone>();
            selfMagneticScale = zone.magneticMoveForce;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Positive") {
            Debug.Log("Exit magnetic zone");
            inMagneticZone = false;
            selfMagneticScale = 0.0f;
        }
    }

    private void applyMagneticZoneForceUp(){
        if (Input.GetKey(";")) {
            Debug.Log("press on Apply Magnetic");
            Debug.Log("is in magnetic zone: " + inMagneticZone);
            float magneticForce = inMagneticZone ? selfMagneticScale : 0.0f;
            rb.gravityScale = inMagneticZone ? 0.0f : origGravityScale;
            Debug.Log("player gravity scale is " + rb.gravityScale);
            float h = magneticForce;
            float v = inMagneticZone ? (magneticForce) : 0;
            Debug.Log("h: " + h + ", v: " + v);
            rb.AddForce(new Vector2(0, v));
        }
        else {
            rb.gravityScale = origGravityScale;
        }
    }


}
