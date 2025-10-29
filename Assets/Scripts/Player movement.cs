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

  [Header("Ground seting")]
    public Transform groundCheckTransform;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);

    public LayerMask groumdLayer;

    [Header("Componets")]
    public Animator playerAnimator;
    public SpriteRenderer playerRenderer;


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

    bool isGrounded = false;

  
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }



    public void FixedUpdate()
    {
        body.linearVelocityX = horizantalMovement * playerSpeed;
        GroundCheck();  
    }

   
    public void GroundCheck()
    {
       if( Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0 , groumdLayer))
        
            isGrounded = true;  
       else
            isGrounded = false;
        
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
    }










}
