using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    public BulletObject bullet;

    public float currentAngle = 0;
    public float currentAngularVelocity = 0;
    public float currentTime = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        currentAngle = bullet.InitRotation;
        currentAngularVelocity = bullet.AngularVelocity;
        
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
    private void SendByCount(int count,float angle)
    {
        float temp = count % 2 == 0 ? angle + bullet.LineAngle / 2 : angle;
        for(int i=0;i<count;i++)
        {
            temp += Mathf.Pow(-1, i) * i * bullet.LineAngle;
            Send(temp);
        }
    }
    private void Send(float angle)
    {
        GameObject go = Instantiate(bullet.prefabs);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(0, 0, angle);
        var bh =go.AddComponent<BulletBehaviour>();
        InitBullet(bh);
    }
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
