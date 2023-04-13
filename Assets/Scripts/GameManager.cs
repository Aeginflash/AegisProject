using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    //����
    public PlayerController playerController;
    public TextMeshProUGUI grazeText;

    public bool isGameOver;
    public bool isNoHurt;
    public GameObject noHurtText;
    // Start is called before the first frame update
    void Start()
    {
        playerHurtDamage = FindObjectOfType<PlayerHurtDamage>();
        playerController = FindObjectOfType<PlayerController>();
       
        
        
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerHurtDamage.isGameOver;
        isNoHurt = playerHurtDamage.isNoHurt;
        NoHurtEffect();

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
        //��������ʾ
        grazeText.text = "graze " + playerController.grazeCount/2;

        if (isGameOver && Input.GetKeyDown(KeyCode.Z))
        {
            Restart();
            isGameOver = false;
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
    //�޵�ʱ����ʾ
    public void NoHurtEffect()
    {
        if(isNoHurt)
        {
            noHurtText.SetActive(true);
        }
        else
        {
            noHurtText.SetActive(false);
        }
    }

    

}
