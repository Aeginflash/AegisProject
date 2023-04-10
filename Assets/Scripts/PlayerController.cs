using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    //移动速度
    public float speed = 8.0f;
    public float lowSpeed = 4.0f;
    //边界
    public float XRange = 14.7f;
    public float YRange = 11.6f;


    //弹幕计时器
    private float invokeTime = 0;
    //弹幕发射间隔
    public float currentTime = 0.2f;

    public int bombDamage = 50;

    public GameObject plyBullet;

    void Start()
    {
        invokeTime = currentTime;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = lowSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8.0f;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        //发射子弹
        if (Input.GetKey(KeyCode.Z))
        {
            //按下Z键开始计时
            invokeTime += Time.deltaTime;
            //超过间隔时执行
            if (invokeTime > currentTime)
            {
                Instantiate(plyBullet, transform.position, plyBullet.transform.rotation);
                //重置计时
                invokeTime = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //松开Z键重置
            invokeTime = currentTime;
        }
        //bomb测试
       
        //判定x轴边界
        if (transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }
        //判定y轴边界
        if (transform.position.y < -YRange)
        {
            transform.position = new Vector3(transform.position.x, -YRange, transform.position.z);
        }
        if (transform.position.y > YRange)
        {
            transform.position = new Vector3(transform.position.x, YRange, transform.position.z);
        }

    }
}
