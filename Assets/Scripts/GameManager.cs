using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

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
    //擦弹
    public PlayerController playerController;
    public TextMeshProUGUI grazeText;

    public bool isGameOver;
    public bool isNoHurt;
    public GameObject noHurtText;


    // Start is called before the first frame update
    void Awake()
    {
        playerHurtDamage = FindObjectOfType<PlayerHurtDamage>();
        playerController = FindObjectOfType<PlayerController>();
        


        Light2D[] lights = FindObjectsOfType<Light2D>();
        foreach (Light2D light in lights)
        {
            light.enabled = false;
        }

        GameObject obj = GameObject.Find("CameraPoint");
        if (obj != null)
        {
            // 将相机位置移动到物体位置
            Camera.main.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, Camera.main.transform.position.z);
        }
    }
    void Start()
    {
        
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
        //擦弹数显示
        grazeText.text = "graze " + playerController.grazeCount/2;

        if (isGameOver && Input.GetKeyDown(KeyCode.Z))
        {
            Restart();
            isGameOver = false;
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
            AudioListener.pause = true; 
            Time.timeScale = 0;
            
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
    public void Restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //无敌时间显示
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
