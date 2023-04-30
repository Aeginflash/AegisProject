using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float floatingDuration = 0.5f; // 文字浮动的总时长
    public float floatingHeight = 2f; // 文字浮动的高度
    public AnimationCurve floatingCurve; // 文字浮动的曲线

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

