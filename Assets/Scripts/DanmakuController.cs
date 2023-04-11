using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuController : MonoBehaviour
{
    public BulletObject bulletObject;
    public float changeTime=2f;
    public float changeLineAngle = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= changeTime)
        {
            bulletObject.LineAngle = changeLineAngle;
        }
    }
}
