using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtDamage : MonoBehaviour
{
    // Start is called before the first frame update
    float enmBulletCount = 0.5f;
    //�Ի�Ѫ��
    public float playerHealth = 300;
    //�Ի��ܵ��������˺�
    private float lastPlayerHurt;
    //����Enemy.cs
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
        //�Ի�Ѫ����0ʱ����Ϸ����
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
    //�Ի��ܵ����˺����㹫ʽ
    public void PlayerTakeDamage(float enmAtk, float enmBulletCount)
    {
        lastPlayerHurt = enmAtk * enmBulletCount;
        playerHealth -= lastPlayerHurt;
        healthBar.value = playerHealth;
    }

}
