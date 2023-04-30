using UnityEngine;

public class MagicCircleRotation : MonoBehaviour
{
    public Transform bossTransform; // Boss��Transform���
    public float rotationSpeed = 30.0f; // ��ת�ٶ�

    void Update()
    {
        // ��ħ�����λ������ΪBoss��λ��
        

        // ������ת�Ƕ�
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // ��Y����תħ����
        transform.RotateAround(transform.position, Vector3.forward, rotationAngle);
    }
}
