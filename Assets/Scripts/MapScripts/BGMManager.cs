using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip musicClip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(musicClip,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
