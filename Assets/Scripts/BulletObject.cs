using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Create BulletAsset")]
public class BulletObject : ScriptableObject
{
    [Header("�ӵ���ʼ����")]
    //�ӵ��ٶȱ���
    //���ٶ�
    public float LinearVelocity = 0;
    //�߼��ٶ�
    public float Acceleration = 0;
    //���ٶ�
    public float AngularVelocity = 0;
    //�Ǽ��ٶ�
    public float AngularAcceleration = 0;
    //����ٶ�
    public float MaxVelocity = int.MaxValue;
    //��������
    public float LifeTime = 5f;
    //�ӵ��˺�
    public float enmBulletCount = 0.5f;

    [Header("��������ʼ����")]
    //��ʼ��ת�Ƕ�
    public float InitRotation = 0;
    //���������ٶ�
    public float SenderAngularVelocity = 0;
    //�����������ٶ�
    public float MaxSenderAngularVelocity = int.MaxValue;
    //���������ٶ�
    public float SenderAcceleration = 0;
    //�ӵ�����
    public int Count = 0;
    //�ӵ��н�
    public float LineAngle = 30;
    //������
    public float SendInterval = 0.1f;

    [Header("�ӵ�����")]
    public BulletAppearance[] bulletAppearances;
    //�ӵ�����
    [System.Serializable]
    public class BulletAppearance
    {
        public GameObject shape;
        public Color[] colors;
    }

    [Header("Ԥ����")]
    public GameObject prefabs;

}
