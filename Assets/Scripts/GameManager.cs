using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��ʱ��
    private float actualTime;
    private float maxTime = 60.0f;
    public TextMeshProUGUI timerText;
    //��ͣ��
    public GameObject pauseScreen;
    private bool paused;
    //������
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
    //��ʱ��
    void Timer()
    {
        actualTime = maxTime - Time.time;
        timerText.text = actualTime.ToString("#0.00");
    }
    //��ͣ��
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
