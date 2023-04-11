using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Ѫ��
    public Slider healthBar;
    //Ѫ��
    public float health = 500;
    public int damage = 5;
    //���˹�����
    public float enmAtk = 100;

    public GameObject stageClearText;
    // Start is called before the first frame update
    void Start()
    {
        stageClearText.SetActive(false);
        healthBar.value = healthBar.maxValue = health;
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
            stageClearText.SetActive(true);
        }
    }
}
