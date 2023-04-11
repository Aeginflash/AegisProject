using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    
    public PlayerController playerController;
    public GameObject weaponBullet;

    //弹幕计时器
    private float invokeTime = 0;
    //弹幕发射间隔
    public float currentTime = 0.2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            //按下Z键开始计时
            invokeTime += Time.deltaTime;
            //超过间隔时执行
            if (invokeTime > currentTime)
            {
                Instantiate(weaponBullet, transform.position, weaponBullet.transform.rotation);
                //重置计时
                invokeTime = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //松开Z键重置
            invokeTime = currentTime;


        }
    }
}
