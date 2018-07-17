using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    private Animator animator;

    public Transform[] groundPoints;
    public float groundRadius;
    public LayerMask whatIsGround, whatIsDeadly;
    public float jumpForce = 200f;

    private float startTouchPosition;
    private float endTouchPosition;
   

    public Rigidbody2D RigidBody { get; set; }

    public bool Jump { get; set; }

    public bool Slide { get; set; }

    public bool OnGround { get; set; }

    public bool Kunai { get; set; }

    public bool Dead { get; set; }

    void Start () {
        RigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        if (!Dead) { 
            if (Jump)
            {
                animator.SetTrigger("Jump");
            }
            if (Slide)
            {
                animator.SetTrigger("Slide");
            }
            if (Kunai)
            {
                animator.SetTrigger("Throw");
            }
        }
    }

    void FixedUpdate()
    {
        Dead = IsDead();
        HandleDeath();
        OnGround = IsGrounded();
        HandleMovement();
        HandleLayers();
       
    }
    void HandleMovement()
    {    
        if (RigidBody.velocity.y < 0 && !Dead)
        {
            animator.SetBool("Land", true);
        }
        if (Jump && RigidBody.velocity.y == 0 && !Dead)
        {
            RigidBody.AddForce(new Vector2(0, jumpForce));
        }
      //  if (GameController.instance.scrollSpeed >0)
            animator.SetFloat("Speed", 1.0f);
    }
   
    bool IsGrounded()
    {
        if (RigidBody.velocity.y <= 0)
        {
            foreach (Transform  point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    bool IsDead()
    {
        if (RigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsDeadly);
                for (int i = 0; i < colliders.Length; i++)
                {
                    return true;
                }
            }
        }

        return false;
    }
    void HandleDeath() {
        if (IsDead())
        {
            animator.SetTrigger("Dead");
            GameController.instance.PlayerDied();
        }
    }
    void HandleLayers()
    {
        if (!OnGround)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

}
