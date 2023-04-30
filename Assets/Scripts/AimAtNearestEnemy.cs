using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AimAtNearestEnemy : MonoBehaviour
{
    public float speed = 10f; // 子弹速度

    private void Update()
    {
        // 查找离自机最近的敌人
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // 计算子弹朝向目标的方向向量
            Vector2 direction = closestEnemy.transform.position - transform.position;

            // 设置子弹的旋转角度，使其朝向目标
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 减去90度修正
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // 移动子弹
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        }
    }

    GameObject FindClosestEnemy()
    {
        // 获取场景中所有的敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        // 遍历所有敌人，找到离自机最近的敌人
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


