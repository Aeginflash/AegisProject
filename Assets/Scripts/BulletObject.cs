using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Create BulletAsset")]
public class BulletObject : ScriptableObject
{
    [Header("子弹初始配置")]
    //子弹速度变量
    //线速度
    public float LinearVelocity = 0;
    //线加速度
    public float Acceleration = 0;
    //角速度
    public float AngularVelocity = 0;
    //角加速度
    public float AngularAcceleration = 0;
    //最大速度
    public float MaxVelocity = int.MaxValue;
    //生命周期
    public float LifeTime = 5f;
    //子弹伤害
    public float enmBulletCount = 0.5f;

    [Header("发射器初始配置")]
    //初始旋转角度
    public float InitRotation = 0;
    //发射器角速度
    public float SenderAngularVelocity = 0;
    //发射器最大角速度
    public float MaxSenderAngularVelocity = int.MaxValue;
    //发射器加速度
    public float SenderAcceleration = 0;
    //子弹条数
    public int Count = 0;
    //子弹夹角
    public float LineAngle = 30;
    //发射间隔
    public float SendInterval = 0.1f;

    [Header("子弹外形")]
    public BulletAppearance[] bulletAppearances;
    //子弹外形
    [System.Serializable]
    public class BulletAppearance
    {
        public GameObject shape;
        public Color[] colors;
    }

    [Header("预制体")]
    public GameObject prefabs;

}
