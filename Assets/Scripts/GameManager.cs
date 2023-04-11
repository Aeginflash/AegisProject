using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    //重启用
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI quitText;
    public PlayerHurtDamage playerHurtDamage;

    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerHurtDamage = FindObjectOfType<PlayerHurtDamage>();
        isGameOver = playerHurtDamage.isGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            Debug.Log("gameover");
        }

        Timer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();

        }
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                
                Restart();
                Time.timeScale = 1;
                paused = false;
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                Application.Quit();
            }
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
    public void Restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
