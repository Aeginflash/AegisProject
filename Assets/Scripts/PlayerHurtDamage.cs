using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtDamage : MonoBehaviour
{
    // Start is called before the first frame update
    float enmBulletCount = 0.5f;
    //自机血量
    public float playerHealth = 300;
    //自机受到的最终伤害
    private float lastPlayerHurt;
    //引用Enemy.cs
    public Enemy enemy;

    public Slider healthBar;

    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        healthBar.value = healthBar.maxValue = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //自机血量归0时，游戏结束
        if (playerHealth <= 0)
        {
            Debug.Log("GameOver!");
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("enmBullet"))
        {
            PlayerTakeDamage(enemy.enmAtk, enmBulletCount);
            Destroy(other.gameObject);
        }
    }
    //自机受到的伤害计算公式
    public void PlayerTakeDamage(float enmAtk, float enmBulletCount)
    {
        lastPlayerHurt = enmAtk * enmBulletCount;
        playerHealth -= lastPlayerHurt;
        healthBar.value = playerHealth;
    }

}
