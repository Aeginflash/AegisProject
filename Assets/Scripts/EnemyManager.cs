using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    void Start()
    {
        StartCoroutine(SpawnEnemyAfterDelay(1f, enemy, new Vector3(0f, 0f, 10f)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyAfterDelay(float delay, GameObject enemyPrefab, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

}
