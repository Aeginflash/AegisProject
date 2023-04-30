using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public float destroyTime = 0.5f;

    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.main.duration + destroyTime);
    }
}
