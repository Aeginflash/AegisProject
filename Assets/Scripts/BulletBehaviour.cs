using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //线速度，线加速度，角速度，角加速度，最大速度
    public float LinearVelocity = 0;
    public float Acceleration = 0;
    public float AngularVelocity = 0;
    public float AngularAcceleration = 0;
    public float MaxVelocity = int.MaxValue;
    public float LifeTime =5;
    public GameObject enmBullet;
    //擦弹
    public bool isGrazed=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //更新线速度角速度
        LinearVelocity=Mathf.Clamp(LinearVelocity+Acceleration*Time.fixedDeltaTime,-MaxVelocity,MaxVelocity);
        AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;
        //更新子弹位置
        transform.Translate(LinearVelocity * Vector2.right * Time.fixedDeltaTime, Space.Self);
        transform.rotation*=Quaternion.Euler(new Vector3(0,0,1)*AngularVelocity*Time.fixedDeltaTime);

        LifeTime-=Time.fixedDeltaTime;
        if(LifeTime<=0)
        {
            Destroy(gameObject);
        }
    }
}
