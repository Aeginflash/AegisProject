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

    public static MapPlayerController instance;

    private Rigidbody2D playerRb;

    private Animator anim;

    public bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
       

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MapFastMode();
            MapPlayerMove();

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
        }
    }
    public void MapPlayerMove()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);

        playerRb.velocity = new Vector2(horizontalInput, verticalInput) * speed;


    }
    public void MapFastMode()
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
    








   
