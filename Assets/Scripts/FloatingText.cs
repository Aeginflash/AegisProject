using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float floatingDuration = 0.5f; // ���ָ�������ʱ��
    public float floatingHeight = 2f; // ���ָ����ĸ߶�
    public AnimationCurve floatingCurve; // ���ָ���������

    private RectTransform rectTransform;
    private Vector3 initialPosition;
    private float startTime;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        startTime = Time.time;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        if (elapsed < floatingDuration)
        {
            Vector3 newPos = initialPosition + new Vector3(0, floatingCurve.Evaluate(elapsed / floatingDuration) * floatingHeight, 0);
            rectTransform.anchoredPosition = newPos;
        }
    }
}

