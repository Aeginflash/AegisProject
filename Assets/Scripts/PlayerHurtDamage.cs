using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    public GameManager gameManager;
    public bool isGameOver=false;
    //�е����޵�ʱ��
    public float noHurtTime=3f;
    public bool isNoHurt = false;



    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        healthBar.value = healthBar.maxValue = playerHealth;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //�Ի�Ѫ����0ʱ����Ϸ����
        GameOver(playerHealth);
        
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
        if (isNoHurt) { return; }
        lastPlayerHurt = enmAtk * enmBulletCount;
        playerHealth -= lastPlayerHurt;
        healthBar.value = playerHealth;

        StartCoroutine(NoHurtMode());
    }
    public void GameOver(float playerHealth)
    {
        if (playerHealth <= 0)
        {
            isGameOver = true;
            Destroy(gameObject);
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);


        }
    }
    IEnumerator NoHurtMode()
    {
        isNoHurt = true;
        Debug.Log("�޵�ʱ�俪ʼ");
        yield return new WaitForSeconds(noHurtTime);
        isNoHurt = false;
        Debug.Log("�޵�ʱ�����");
    }


}
