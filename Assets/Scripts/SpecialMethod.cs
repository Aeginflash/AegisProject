using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpecialMethod : MonoBehaviour
{
    //每秒伤害
    public int bombDamage = 5;
    public float bombDuration = 5;
    public Enemy enemy;
    private bool bombCoolDown = false;
    //特效
    public PostProcessVolume postProcessVolume;
    private Bloom bloom;
    private ColorGrading colorGrading;

    public AudioClip bombSE;
    public SEManager seManager;
    //判断特效计时器运行
    private bool isTimerRunning = false;
    public GameObject bombText;

    public float timer = 0.5f;
    public PlayerHurtDamage playerHurtDamage;
    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.enabled = false;
        bombText.SetActive(false);
        playerHurtDamage=FindObjectOfType<PlayerHurtDamage>();

        seManager = FindObjectOfType<SEManager>();
        bombSE = seManager.bombSE;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Bomb();
        }
    }
    public void Bomb()
    { 
        // 检查必杀技是否在冷却中
        if (bombCoolDown)
        {
            return;
        }

        // 开始必杀技
        StartCoroutine(BombActive());
    }

    IEnumerator BombActive()
    {
        bombText.SetActive(true);
        AudioManager.instance.PlaySFX(bombSE, 1);
        postProcessVolume.enabled = true;
        bombCoolDown = true;

        float startTime = Time.time;

        postProcessVolume.profile.TryGetSettings(out bloom);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        if (!isTimerRunning)
        {
            StartCoroutine(Timer());
        }

        while (Time.time < startTime + bombDuration)
        {
            playerHurtDamage.isNoHurt = true;

            foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
                if (enemy != null&&enemy.health>0)
                {
                    enemy.TakeDamage(bombDamage * Time.deltaTime);
                   
                }
                if(enemy.health<=0)
                {
                    postProcessVolume.enabled = false;
                    bombText.SetActive(false);
                    yield break;
                }
                
            }
            yield return null;
        }
        playerHurtDamage.isNoHurt = false;
        bombText.SetActive (false);
        bombCoolDown = false;
        postProcessVolume.enabled = false;
        isTimerRunning = false;
        StopCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        isTimerRunning = true;
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        while (isTimerRunning==true&&enemy.health>0)
        {
            yield return new WaitForSeconds(timer);
            bloom.enabled.value = !bloom.enabled.value;
            colorGrading.enabled.value = !colorGrading.enabled.value;
        }        
    }
}

