using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutScript : MonoBehaviour
{
    public float duration = 2.0f; // 动画持续时间
    public float speed = 1.0f; // 动画速度

    private float startTime=0; // 动画开始时间
    private SpriteRenderer spriteRenderer; // SpriteRenderer组件
    public BulletBehaviour bulletBehaviour;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletBehaviour = GetComponent<BulletBehaviour>();
        
    }

    private void Update()
    {
        if(bulletBehaviour.isBulletDead==true)
        {
            GetComponent<Collider2D>().enabled = false;
            if (startTime == 0)
            {
                startTime = Time.time;
            }

            // 计算当前时间和动画开始时间之间的差
            float timePassed = Time.time - startTime;

            // 计算当前透明度
            float alpha = 1.0f - (timePassed / duration) * speed;

            // 应用透明度到SpriteRenderer组件中的Color属性
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            // 如果动画完成，销毁物体
            if (alpha <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        
    }

    public void StartFadeOut()
    {
        // 启动动画
        startTime = 0;
    }
}

