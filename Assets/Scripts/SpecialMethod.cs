using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpecialMethod : MonoBehaviour
{
    public int bombDamage = 5;
    public float bombDuration = 5;
    public Enemy enemy;
    private bool bombCoolDown = false;
    public PostProcessVolume postProcessVolume;
    private Bloom bloom;
    private ColorGrading colorGrading;

    public float timer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.enabled = false;

        
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
        postProcessVolume.enabled = true;
        bombCoolDown = true;

        float startTime = Time.time;

        postProcessVolume.profile.TryGetSettings(out bloom);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        StartCoroutine(Timer());

        while (Time.time < startTime + bombDuration)
        {
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(bombDamage * Time.deltaTime);
                }
            }
            yield return null;
        }
        bombCoolDown = false;
        postProcessVolume.enabled = false;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            bloom.enabled.value = !bloom.enabled.value;
            colorGrading.enabled.value = !colorGrading.enabled.value;
        }
    }
}

