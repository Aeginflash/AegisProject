using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public GameObject explosionEffect;

    void OnDestroy()
    {
        if (explosionEffect != null)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
