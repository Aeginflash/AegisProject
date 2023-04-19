using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public string musicName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.instance.StopBGM();
            SceneManager.LoadScene(1);
            //销毁多余的audiolistener组件
            AudioListener[] listeners = FindObjectsOfType<AudioListener>();
            foreach (AudioListener listener in listeners)
            {
                if (listener.gameObject.scene != gameObject.scene)
                {
                    Destroy(listener.gameObject);
                }
            }



        }
    }
}
