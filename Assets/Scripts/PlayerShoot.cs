using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletYRange = 12f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
        if (transform.position.y > bulletYRange)
        {
            Destroy(gameObject);
        }

    }

}


