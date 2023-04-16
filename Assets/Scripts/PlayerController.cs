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
    public float normalSpeed = 8.0f;
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
    public GameObject decisionPoint;
    public PlayerHurtDamage playerHurtDamage;
    
    public bool isGameOver;
    public bool isSlowMode;
    //子机
    public GameObject[] weaponPrefabs;
    public float[] weaponOffsetX;
    public float[] weaponOffsetY; // 每个武器在Y轴的偏移量
    public float weaponDistance = 1.5f;
    public float weaponDistanceSlow = 0.8f;// 武器与玩家的距离
    public float moveSpeed = 5f;
    private List<GameObject> weapons = new List<GameObject>();

    //擦弹
    public float grazeRadius = 3f; // 擦弹半径
    public int grazeCount = 0; // 擦弹次数
    


    void Start()
    {
        isSlowMode = false;
        invokeTime = currentTime;
        playerHurtDamage = FindObjectOfType<PlayerHurtDamage>();
        
 
        //子机
        for (int i = 0; i < 4; i++)
        {
            GameObject weapon = Instantiate(weaponPrefabs[i], transform.position, Quaternion.identity);
            weapons.Add(weapon);
            
        }
        decisionPoint.SetActive(false);

    }


    void Update()
    {
        isGameOver = playerHurtDamage.isGameOver;
        if (isGameOver == false)
        {
            SlowMode();
            PlayerMove();
            PlayerShoot(plyBullet);
            PlayerRange(XRange, YRange);

            if (!isSlowMode)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    Vector3 weaponPos = transform.position + new Vector3(weaponOffsetX[i], weaponOffsetY[i], 0).normalized * weaponDistance;
                    weapons[i].transform.position = weaponPos;
                }
            }
            if (isSlowMode)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    Vector3 weaponPos = transform.position + new Vector3(weaponOffsetX[i], weaponOffsetY[i], 0).normalized * weaponDistanceSlow;
                    weapons[i].transform.position = weaponPos;
                }
            }
            //擦弹
            foreach (BulletBehaviour enmBullet in FindObjectsOfType<BulletBehaviour>())
            {
                if (enmBullet.isGrazed) break;
                
                Vector2 offset = enmBullet.transform.position - transform.position;
                if (offset.magnitude <= grazeRadius) // 判断是否在擦弹范围内
                {
                    enmBullet.isGrazed = true;
                    grazeCount++;
                }
                else
                {
                    enmBullet.isGrazed = false;
                }
                
            }

        }

    }
    public void SlowMode()
    {
        speed = normalSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSlowMode = true;
            speed = lowSpeed;
            decisionPoint.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSlowMode = false;
            speed = normalSpeed;
            decisionPoint.SetActive(false);
        }
    }
    public void PlayerMove()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
    }

    public void PlayerShoot(GameObject plyBullet)
    {
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
    } 
    public void PlayerRange(float XRange,float YRange)
    {
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
