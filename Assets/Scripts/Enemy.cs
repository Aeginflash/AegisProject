using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public AudioClip enemyFailSE;
    public SEManager seManager;
    public GameObject stageClearText;
    // Start is called before the first frame update
    void Start()
    {
        stageClearText.SetActive(false);
        healthBar.value = healthBar.maxValue = health;

        seManager=FindObjectOfType<SEManager>();
        enemyFailSE = seManager.enemyFailSE;
    }

    // Update is called once per frame
    void Update()
    {

        GameClear();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
    public void GameClear()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(enemyFailSE, 0.8f);

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("enmBullet");
            foreach (GameObject bullet in enemyBullets)
            {
                Destroy(bullet);
            }
            stageClearText.SetActive(true);
        }
    }
}
