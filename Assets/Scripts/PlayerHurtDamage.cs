using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    public GameManager gameManager;
    public bool isGameOver=false;
    //中弹后无敌时间
    public float noHurtTime=3f;
    public bool isNoHurt = false;
    //无敌时间闪烁
    public Color normalColor;
    public Color specialColor;
    public float blinkInterval = 0.1f; // 闪烁间隔时间

    private SpriteRenderer spriteRenderer;
    //miss音效
    public AudioClip missSE;

    public SEManager seManager;

    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        healthBar.value = healthBar.maxValue = playerHealth;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        seManager = FindObjectOfType<SEManager>();
        missSE = seManager.missSE;

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //自机血量归0时，游戏结束
        GameOver(playerHealth);
        StartCoroutine(Blink());

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
        if (isNoHurt) { return; }
        lastPlayerHurt = enmAtk * enmBulletCount;
        playerHealth -= lastPlayerHurt;
        healthBar.value = playerHealth;
        AudioManager.instance.PlaySFX(missSE, 1);
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
        
        yield return new WaitForSeconds(noHurtTime);
        isNoHurt = false;
        
    }
    IEnumerator Blink()
    {
        float startTime = Time.time;
        float interval = blinkInterval;

        // 每个间隔时间交替设置自机的颜色
        while (isNoHurt)
        {
            if (Time.time - startTime >= interval)
            {
                startTime = Time.time;

                // 交替设置自机颜色
                spriteRenderer.color = spriteRenderer.color == specialColor ? normalColor: specialColor;
            }

            yield return null;
        }

        // 在无敌时间结束时将自机的颜色设置回来
        spriteRenderer.color = normalColor;
    }

}
