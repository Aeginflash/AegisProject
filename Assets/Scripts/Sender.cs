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
    

    private float timer; // ��ʱ��
    private Vector3 originalPosition; // ��ʼλ��

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
        originalPosition = transform.position; // ��¼��ʼλ��
        timer = 0.0f; // ��ʼ����ʱ��
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
    //�����������ӵ�
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
    //�����ӵ�
    private void Send(float angle)
    {

        if (bullet.IsBulletGroup) // �ж��Ƿ����ӵ���
        {
            CreateBulletGroup(angle); // �����ӵ���
        }
        else
        {
            CreateBullet(angle); // ������ͨ�ӵ�
        }
    }
    //��ͨ�ӵ�
    private void CreateBullet(float angle)
    {
        GameObject go = Instantiate(bullet.prefabs);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(0f, bullet.RandomAngle));

        var bh = go.AddComponent<BulletBehaviour>();
        InitBullet(bh);
    }
    //��������ӣ�������ӵ��飬�������ڻ�������������ʱ�����ˡ�Ŀǰ����bug��1���ǰ뾶̫Сʱ��״������1���Ǻ��Ի��Ѳ�����
    private void CreateBulletGroup(float angle)
    {
        float radius = 2f; // �ӵ�����Բ��ת�İ뾶
        int bulletCount = 6; // �ӵ����е��ӵ�����
        float angleIncrement = 60; // ÿ����ת�ĽǶ�����

        GameObject bulletGroup = new GameObject("BulletGroup"); // �����ӵ���ĸ�����
        bulletGroup.transform.position = transform.position; // ���ø�����ĳ�ʼλ��
        

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletAngle = angle + i * angleIncrement; // ����ÿ���ӵ��ĽǶ�ƫ��
            float x = Mathf.Cos(Mathf.Deg2Rad * bulletAngle) * radius; // �ӵ��� x ����
            float y = Mathf.Sin(Mathf.Deg2Rad * bulletAngle) * radius; // �ӵ��� y ����

            GameObject go = Instantiate(bullet.prefabs); // �����ӵ�ʵ��
            go.transform.SetParent(bulletGroup.transform); // ���ӵ�����Ϊ�������������
            go.transform.localPosition = new Vector3(x, y, 0); // �����ӵ��ľֲ�����
            go.transform.rotation = Quaternion.Euler(0, 0, bulletAngle); // �����ӵ�����ת�Ƕ�

            if (i == bulletCount - 1)
            {
                bulletGroup.transform.rotation = go.transform.rotation; // �����һ���ӵ�����ת�Ƕ�����Ϊ�ӵ������ת�Ƕ�
            }
        }

        var bh = bulletGroup.AddComponent<BulletBehaviour>();
        InitBullet(bh);
    }

    //��ʼ���ӵ�
    private void InitBullet(BulletBehaviour bh)
    {
        
        bh.LinearVelocity=bullet.LinearVelocity;
        bh.Acceleration=bullet.Acceleration;
        bh.AngularAcceleration=bullet.AngularAcceleration;
        bh.AngularVelocity=bullet.AngularVelocity;
        bh.LifeTime = bullet.LifeTime;
        bh.MaxVelocity=bullet.MaxVelocity;
        
    }
    //����������ƶ�
    public void RandomMoveSender()
    {
        timer += Time.deltaTime;

        // ����Ƿ񳬹�ʱ����
        if (timer >=bullet.SenderMoveInterval)
        {
            // �������λ��
            Vector3 randomOffset = new Vector3(Random.Range(-bullet.MoveRangeX, bullet.MoveRangeX), Random.Range(-bullet.MoveRangeY, bullet.MoveRangeY), 0);
            Vector3 newPosition = originalPosition + randomOffset;

            // ���õ��˵�λ��Ϊ���λ��
            transform.position = newPosition;

            // ���ü�ʱ��
            timer = 0.0f;
        }
    }
}
