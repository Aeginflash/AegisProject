using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //血条
    public Slider healthBar;
    //血量
    public float health = 500;
    public int damage = 5;
    //敌人攻击力
    public float enmAtk = 100;


    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
