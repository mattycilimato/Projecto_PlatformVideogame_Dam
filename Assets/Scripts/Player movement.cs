using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.InputAction;

public class Playermovement : MonoBehaviour
{
    [Header("General settinngs")]
    public float playerSpeed = 10;
    public float JumpForce = 5;



    [Header("Gravity settinngs")]
    public float baseGravity = 2;
    public float maxFallSpeed = 10f;
      public float fallSpeedMultiplier = 2f;

  [Header("Ground settings")]
    public Transform groundCheckTransform;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);

    public LayerMask groundLayer;


    [Header("Wall settings")]
    public Transform wallCheckTransform;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.1f);

    public LayerMask wallLayer;


    [Header("SFX")]
    public AudioClip SFXjump;
    
    [Header("Componets")]
    public Animator playerAnimator;
    public SpriteRenderer playerRenderer;
    public AudioSource audioSource;


    public void Update()
    {
        playerAnimator.SetFloat("X ABSpeed", Mathf.Abs (body.linearVelocityX));
        if (Mathf.Abs(body.linearVelocityX) > 0.01) 
        {
            bool needFlip = body.linearVelocityX < 0;
            playerRenderer.flipX = needFlip;


        }

        playerAnimator.SetFloat("Y speed", body.linearVelocityY);    

        

    }









    private void SetGravity()
    {
        if(body.linearVelocityY < 0)
        {
            body.gravityScale = baseGravity;
            body.linearVelocityY = Mathf.Max(body.linearVelocityY, -maxFallSpeed);

        }
        else
        {
            body.gravityScale = baseGravity;
        }
    }

    Rigidbody2D body;
    
    float horizantalMovement = 0;
    float verticalMovment = 1/2;


    bool isGrounded = false;
    bool isWalled = false;
  
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }



    public void FixedUpdate()
    {
        body.linearVelocityX = horizantalMovement * playerSpeed;
        GroundCheck();  
        SetGravity();   
    }

   
    public void GroundCheck()
    {
       if( Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0 , groundLayer))
        
            isGrounded = true;  
       else
            isGrounded = false;
        
    }

    

    
    public void WallCheck()
    {
        if (Physics2D.OverlapBox(wallCheckTransform.position, wallCheckSize, 0, wallLayer))

            isWalled = true;
        else
            isWalled = false;

    }

    public void PlayerInput_Move(CallbackContext context)
    {
        horizantalMovement = context.ReadValue<Vector2>().x;



    }

    public void PlayerInpunt_Jump(CallbackContext context)
    {
        if (isGrounded) 
        {
            if (context.performed)
            {
                body.linearVelocityY = JumpForce;
                
                audioSource.PlayOneShot(SFXjump);
            }
            

        }
        if (context.canceled && body.linearVelocityY >0)
        {
            body.linearVelocityY = body.linearVelocityY / 2;
        }


    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundCheckTransform.position, groundCheckSize);
        Gizmos.DrawCube(wallCheckTransform.position, wallCheckSize);
    }










}
