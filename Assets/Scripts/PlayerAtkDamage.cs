using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public PlayerHurtDamage playerHurtDamage;
    public bool isGameOver;
    public AudioClip damageSE;
    public SEManager seManager;

    void Start()
    {
        playerHurtDamage= FindObjectOfType<PlayerHurtDamage>();
        seManager=FindObjectOfType<SEManager>();
        damageSE = seManager.damageSE;
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerHurtDamage.isGameOver;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver==false&&other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySFX(damageSE, 0.4f);
            other.GetComponent<Enemy>().TakeDamage(damage);
            
            Destroy(gameObject);
        }

    }
}

