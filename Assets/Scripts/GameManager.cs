using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //计时用
    private float actualTime;
    private float maxTime = 60.0f;
    public TextMeshProUGUI timerText;
    //暂停用
    public GameObject pauseScreen;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }
    //计时器
    void Timer()
    {
        actualTime = maxTime - Time.time;
        timerText.text = actualTime.ToString("#0.00");
    }
    //暂停器
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
