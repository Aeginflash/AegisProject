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
        // �����ײ�����Ƿ��ǵ����ӵ�
        if (collision.gameObject.CompareTag("enmBullet"))
        {
            Rigidbody2D bulletRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (bulletRigidbody != null)
            {
                // ��ȡ����Ƕ�
                Vector2 incomingDirection = bulletRigidbody.velocity.normalized;
                float reflectionAngle = Vector2.Angle(incomingDirection, collision.contacts[0].normal);

                // ���㷴��Ƕ�
                float angle = reflectCount - reflectionAngle;

                // ��ת�����ӵ�������Ƕ�
                bulletRigidbody.velocity = Quaternion.Euler(0, 0, angle) * bulletRigidbody.velocity;
                bulletRigidbody.rotation += angle;

                

                // ������Ч������Ч����
                // ...
            }
        }
    }
}
