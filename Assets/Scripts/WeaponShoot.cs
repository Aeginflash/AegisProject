using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    
    public PlayerController playerController;
    public GameObject weaponBullet;

    //��Ļ��ʱ��
    private float invokeTime = 0;
    //��Ļ������
    private float currentTime = 0.4f;
    public PlayerHurtDamage playerHurtDamage;
    public bool isGameOver;

    void Start()
    {
        playerHurtDamage=FindObjectOfType<PlayerHurtDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerHurtDamage.isGameOver;
        if (isGameOver == false)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                //����Z����ʼ��ʱ
                invokeTime += Time.deltaTime;
                //�������ʱִ��
                if (invokeTime > currentTime)
                {
                    Instantiate(weaponBullet, transform.position, weaponBullet.transform.rotation);
                    //���ü�ʱ
                    invokeTime = 0;
                }

            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //�ɿ�Z������
            invokeTime = currentTime;


        }
    }
}
