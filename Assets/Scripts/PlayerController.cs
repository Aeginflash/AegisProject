using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    //�ƶ��ٶ�
    public float speed = 8.0f;
    public float lowSpeed = 4.0f;
    //�߽�
    public float XRange = 14.7f;
    public float YRange = 11.6f;


    //��Ļ��ʱ��
    private float invokeTime = 0;
    //��Ļ������
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
        //�����ӵ�
        if (Input.GetKey(KeyCode.Z))
        {
            //����Z����ʼ��ʱ
            invokeTime += Time.deltaTime;
            //�������ʱִ��
            if (invokeTime > currentTime)
            {
                Instantiate(plyBullet, transform.position, plyBullet.transform.rotation);
                //���ü�ʱ
                invokeTime = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //�ɿ�Z������
            invokeTime = currentTime;
        }
        //bomb����
       
        //�ж�x��߽�
        if (transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }
        //�ж�y��߽�
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
