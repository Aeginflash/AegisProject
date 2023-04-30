using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AimAtNearestEnemy : MonoBehaviour
{
    public float speed = 10f; // �ӵ��ٶ�

    private void Update()
    {
        // �������Ի�����ĵ���
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // �����ӵ�����Ŀ��ķ�������
            Vector2 direction = closestEnemy.transform.position - transform.position;

            // �����ӵ�����ת�Ƕȣ�ʹ�䳯��Ŀ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // ��ȥ90������
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // �ƶ��ӵ�
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        }
    }

    GameObject FindClosestEnemy()
    {
        // ��ȡ���������еĵ���
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        // �������е��ˣ��ҵ����Ի�����ĵ���
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}


