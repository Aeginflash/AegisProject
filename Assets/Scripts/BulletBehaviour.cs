using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //���ٶȣ��߼��ٶȣ����ٶȣ��Ǽ��ٶȣ�����ٶ�
    public float LinearVelocity = 0;
    public float Acceleration = 0;
    public float AngularVelocity = 0;
    public float AngularAcceleration = 0;
    public float MaxVelocity = int.MaxValue;
    public float LifeTime =5;
    public GameObject enmBullet;
    //����
    public bool isGrazed=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //�������ٶȽ��ٶ�
        LinearVelocity=Mathf.Clamp(LinearVelocity+Acceleration*Time.fixedDeltaTime,-MaxVelocity,MaxVelocity);
        AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;
        //�����ӵ�λ��
        transform.Translate(LinearVelocity * Vector2.right * Time.fixedDeltaTime, Space.Self);
        transform.rotation*=Quaternion.Euler(new Vector3(0,0,1)*AngularVelocity*Time.fixedDeltaTime);

        LifeTime-=Time.fixedDeltaTime;
        if(LifeTime<=0)
        {
            Destroy(gameObject);
        }
    }
}
