using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [Header("Movement Attributes")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpForce;
    private GameObject groundCheck;
    private Rigidbody2D rb;
    private Animator anim;
    private bool grounded = true;
    private bool jump = false;
    private float currentMoveSpeed;
    private GroundCheck gcheck;
    private float direction;

    public float Direction
    {
        get { return direction; }
        set { direction = value; }
    }


    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        groundCheck = transform.GetChild(0).gameObject;
        gcheck = groundCheck.GetComponent<GroundCheck>();
        currentMoveSpeed = maxMoveSpeed;
        direction = 1;
    }

    private void Update()
    {
        grounded = gcheck.Grounded;
        if (grounded)
        {
           
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

        }

    }

    private void FixedUpdate()
    {
     
        float horizontalMovement = Input.GetAxis("Horizontal");

        if(horizontalMovement != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAttack"))
        {
            anim.SetBool("Running", true);
            if (direction == -1 && horizontalMovement > 0)
            {
                //Do this while flipping the sprite later
                direction = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (direction == 1 && horizontalMovement < 0)
            {
                direction = -1;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            rb.velocity = new Vector2(horizontalMovement * currentMoveSpeed, rb.velocity.y);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }

    


}
