using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    public BulletObject bullet;

    public float currentAngle = 0;
    public float currentAngularVelocity = 0;
    public float currentTime = 0;
    public AudioClip enemySendSE;
    public SEManager seManager;
    public float randomAngle;
    public bool isPlayerAim=false;
    // Start is called before the first frame update
    private void Awake()
    {
        
        currentAngle = bullet.InitRotation;
        currentAngularVelocity = bullet.AngularVelocity;

        seManager=FindObjectOfType<SEManager>();
        enemySendSE = seManager.enemySendSE;
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
            currentTime -= bullet.SendInterval;
            SendByCount(bullet.Count,currentAngle);
            
        }

        
        

    }
    //按条数发射子弹
    private void SendByCount(int count,float angle)
    {
        AudioManager.instance.PlaySFX(enemySendSE, 0.05f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (isPlayerAim)
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
            Send(temp,randomAngle);
        }
    }
    //发射子弹
    private void Send(float angle,float randomAngle)
    {
      
        GameObject go = Instantiate(bullet.prefabs);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(0, 0, angle+Random.Range(0,randomAngle));
        var bh =go.AddComponent<BulletBehaviour>();
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
}
