using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Sender : MonoBehaviour
{
    public BulletObject bullet;

    public float currentAngle = 0;
    public float currentAngularVelocity = 0;
    public float currentTime = 0;
    public AudioClip enemySendSE;
    public SEManager seManager;
    

    private float timer; // 计时器
    private Vector3 originalPosition; // 初始位置

    // Start is called before the first frame update
    private void Awake()
    {

        currentAngle = bullet.InitRotation;
        currentAngularVelocity = bullet.AngularVelocity;

        seManager=FindObjectOfType<SEManager>();
        enemySendSE = seManager.enemySendSE;
    }
    private void Start()
    {
        originalPosition = transform.position; // 记录初始位置
        timer = 0.0f; // 初始化计时器
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        currentAngularVelocity=Mathf.Clamp(currentAngularVelocity+bullet.SenderAcceleration*Time.fixedDeltaTime,-bullet.MaxSenderAngularVelocity,bullet.MaxSenderAngularVelocity);
        currentAngle += currentAngularVelocity * Time.fixedDeltaTime;
        if(Mathf.Abs(currentAngle)>720f)
        {
            currentAngle-=Mathf.Sign(currentAngle)*360f;
        }
        currentTime += Time.fixedDeltaTime;
        if (currentTime > bullet.SendInterval)
        {
            AudioManager.instance.PlaySFX(enemySendSE, 0.5f);
            currentTime -= bullet.SendInterval;
            SendByCount(bullet.Count,currentAngle);
            
        }
        RandomMoveSender();

        
        

    }
    //按条数发射子弹
    private void SendByCount(int count,float angle)
    {
        

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (bullet.IsPlayerAim)
        {
            if (player != null)
            {
                Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
                Vector2 direction = playerPosition - new Vector2(transform.position.x, transform.position.y);
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }

        }
        float temp = count % 2 == 0 ? angle + bullet.LineAngle / 2 : angle;
        for(int i=0;i<count;i++)
        {
            temp += Mathf.Pow(-1, i) * i * bullet.LineAngle;
            Send(temp);
        }
    }
    //发射子弹
    private void Send(float angle)
    {

        if (bullet.IsBulletGroup) // 判断是否发射子弹组
        {
            CreateBulletGroup(angle); // 发射子弹组
        }
        else
        {
            CreateBullet(angle); // 发射普通子弹
        }
    }
    //普通子弹
    private void CreateBullet(float angle)
    {
        GameObject go = Instantiate(bullet.prefabs);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(0f, bullet.RandomAngle));

        var bh = go.AddComponent<BulletBehaviour>();
        InitBullet(bh);
    }
    //这是类似樱花弹的子弹组，但是现在还做不出来，暂时放弃了。目前两个bug，1个是半径太小时形状崩坏，1个是和自机狙不兼容
    private void CreateBulletGroup(float angle)
    {
        float radius = 2f; // 子弹组绕圆旋转的半径
        int bulletCount = 6; // 子弹组中的子弹数量
        float angleIncrement = 60; // 每次旋转的角度增量

        GameObject bulletGroup = new GameObject("BulletGroup"); // 创建子弹组的父物体
        bulletGroup.transform.position = transform.position; // 设置父物体的初始位置
        

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletAngle = angle + i * angleIncrement; // 计算每个子弹的角度偏移
            float x = Mathf.Cos(Mathf.Deg2Rad * bulletAngle) * radius; // 子弹的 x 坐标
            float y = Mathf.Sin(Mathf.Deg2Rad * bulletAngle) * radius; // 子弹的 y 坐标

            GameObject go = Instantiate(bullet.prefabs); // 创建子弹实例
            go.transform.SetParent(bulletGroup.transform); // 将子弹设置为父物体的子物体
            go.transform.localPosition = new Vector3(x, y, 0); // 设置子弹的局部坐标
            go.transform.rotation = Quaternion.Euler(0, 0, bulletAngle); // 设置子弹的旋转角度

            if (i == bulletCount - 1)
            {
                bulletGroup.transform.rotation = go.transform.rotation; // 将最后一颗子弹的旋转角度设置为子弹组的旋转角度
            }
        }

        var bh = bulletGroup.AddComponent<BulletBehaviour>();
        InitBullet(bh);
    }

    //初始化子弹
    private void InitBullet(BulletBehaviour bh)
    {
        
        bh.LinearVelocity=bullet.LinearVelocity;
        bh.Acceleration=bullet.Acceleration;
        bh.AngularAcceleration=bullet.AngularAcceleration;
        bh.AngularVelocity=bullet.AngularVelocity;
        bh.LifeTime = bullet.LifeTime;
        bh.MaxVelocity=bullet.MaxVelocity;
        
    }
    //发射器随机移动
    public void RandomMoveSender()
    {
        timer += Time.deltaTime;

        // 检查是否超过时间间隔
        if (timer >=bullet.SenderMoveInterval)
        {
            // 生成随机位置
            Vector3 randomOffset = new Vector3(Random.Range(-bullet.MoveRangeX, bullet.MoveRangeX), Random.Range(-bullet.MoveRangeY, bullet.MoveRangeY), 0);
            Vector3 newPosition = originalPosition + randomOffset;

            // 设置敌人的位置为随机位置
            transform.position = newPosition;

            // 重置计时器
            timer = 0.0f;
        }
    }
}
