using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Animator anim;

    [SerializeField] GameObject dustEffect;
    private Transform CheckPointPos;

    //RunVariables
    [SerializeField] private float speed;
    private float moveInput;

    //JumpVariables
    [SerializeField] private float jumpForce;
    [SerializeField] Transform feetPos;
    [SerializeField] float checkHeight;
    [SerializeField] float checkWidth;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpTime;
    public int BonusJumps;
    private int bonusJumps;
    public float jumpTimeCounter;
    private bool isGrounded = true;
    private bool isJumping;
    private bool hasFallen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        bonusJumps = BonusJumps;
    }

    private void FixedUpdate()
    {
        //Running
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    void Update()
    {
        //Flipping player on movement
        if (moveInput > 0)
            transform.eulerAngles = new Vector2(0, 0);
        else if (moveInput < 0)
            transform.eulerAngles = new Vector2(0, -180);

        #region Animations

        //Setting the Running animation
        if (Mathf.Abs(moveInput) > .1f && isGrounded)
            anim.SetBool("IsRunning", true);

        else if (moveInput == 0 && isGrounded)
            anim.SetBool("IsRunning", false);

        //Setting the Fell animation
        if (isGrounded){
            if (hasFallen == true){
                bonusJumps = BonusJumps;
                anim.SetTrigger("Fell");
                Instantiate(dustEffect, feetPos.position, Quaternion.identity);
                hasFallen = false;
            }
        }else{
            hasFallen = true;
        }

        #endregion

        Jumping();
    }

   
    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("CheckPoint")){
            CheckPointPos = collision.transform;
        }

        if (collision.CompareTag("Deadly")){
            Instantiate(dustEffect, feetPos.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            transform.position = CheckPointPos.position;
        }

    }

    private void Jumping()
    {

        isGrounded = Physics2D.OverlapBox(feetPos.position, new Vector2(checkWidth, checkHeight), 0f, whatIsGround);

        //When begining to jump.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; //Resets the "big jump" counter.
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump"); //Setting the Jumping animation
            Instantiate(dustEffect, feetPos.position, Quaternion.identity);
        }
        else if (bonusJumps > 0 && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; //Resets the "big jump" counter.
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump"); //Setting the Jumping animation
            bonusJumps--;
        }

        //While we're in the air.
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //If we let go of the jump key.
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

}
