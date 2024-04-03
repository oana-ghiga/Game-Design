using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown = 0.5f;
    private float horizontalInput;
    

    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
 
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
 
        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2, 2, 2);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2, 2, 2);
 
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            Jump();
 
        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
        anim.SetBool("fall", body.velocity.y < -0.1f);
        
       //Wall jump logic
        if (wallJumpCooldown > 0.5f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

           if (onWall() && !IsGrounded())
           {
               body.gravityScale = 0;
               body.velocity = new Vector2(0, 0);
           }
           else
               body.gravityScale = 3;
           
           if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && IsGrounded())
               Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }
 
    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall()&& !IsGrounded())
        {   if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x * 10), 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x * 3), 4);
            }
            wallJumpCooldown = 0;
        }
    }
    
    private void Fall()
    {
        body.velocity = new Vector2(body.velocity.x, -speed);
        anim.SetTrigger("fall");
    }
    
    
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }
}