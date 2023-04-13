using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 2.0f;
    public float fastSpeed = 4.0f;
    public float normalSpeed = 2.0f;
    public bool isFastMode = false;

    public Transform boundary;
    private Rigidbody2D playerRb;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FastMode();
        PlayerMove();

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (horizontalInput > 0)
            {
                anim.Play("WalkRight");
                
            }
            else if (horizontalInput < 0)
            {
                anim.Play("WalkLeft");
            }
            else if (verticalInput > 0)
            {
                anim.Play("WalkBack");
            }
            else if (verticalInput < 0)
            {
                anim.Play("WalkForward");
            }
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isIdle", true);
        }

        if (transform.position.x < boundary.position.x - boundary.localScale.x / 2 ||
           transform.position.x > boundary.position.x + boundary.localScale.x / 2 ||
           transform.position.y < boundary.position.y - boundary.localScale.y / 2 ||
           transform.position.y > boundary.position.y + boundary.localScale.y / 2)
        {
            playerRb.velocity = Vector2.zero;
        }
    }
    public void PlayerMove()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);

        playerRb.velocity = new Vector2(horizontalInput, verticalInput) * speed;


    }
    public void FastMode()
    {
        speed = normalSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isFastMode = true;
            speed = fastSpeed;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isFastMode = false;
            speed = normalSpeed;
            
        }
    }
}
    








   
