using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveScale=0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �����µ� y ����
        float newY = Mathf.Sin(Time.time * speed) * moveScale; // 0.5f ��ʾ�˶�����

        // ���������λ��
        transform.Translate(0, newY - transform.position.y, 0);
    }
}
