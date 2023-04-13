using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public PlayerHurtDamage playerHurtDamage;
    public bool isGameOver;

    void Start()
    {
        playerHurtDamage= FindObjectOfType<PlayerHurtDamage>();
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
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}

