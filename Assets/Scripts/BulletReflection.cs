using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReflection : MonoBehaviour
{
    public float reflectCount=180f;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检测碰撞对象是否是敌人子弹
        if (collision.gameObject.CompareTag("enmBullet"))
        {
            Rigidbody2D bulletRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (bulletRigidbody != null)
            {
                // 获取入射角度
                Vector2 incomingDirection = bulletRigidbody.velocity.normalized;
                float reflectionAngle = Vector2.Angle(incomingDirection, collision.contacts[0].normal);

                // 计算反射角度
                float angle = reflectCount - reflectionAngle;

                // 旋转敌人子弹朝向反射角度
                bulletRigidbody.velocity = Quaternion.Euler(0, 0, angle) * bulletRigidbody.velocity;
                bulletRigidbody.rotation += angle;

                

                // 播放音效、粒子效果等
                // ...
            }
        }
    }
}
