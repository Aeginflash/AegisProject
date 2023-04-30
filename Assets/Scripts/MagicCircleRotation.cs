using UnityEngine;

public class MagicCircleRotation : MonoBehaviour
{
    public Transform bossTransform; // Boss的Transform组件
    public float rotationSpeed = 30.0f; // 旋转速度

    void Update()
    {
        // 将魔法阵的位置设置为Boss的位置
        

        // 计算旋转角度
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // 绕Y轴旋转魔法阵
        transform.RotateAround(transform.position, Vector3.forward, rotationAngle);
    }
}
