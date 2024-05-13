using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [Header("用來調出資料顯示的物件")]
    public Text scoreText;
    public GameObject rawImage;        // 地圖顯示
    private bool rawMapBool = false;   // 地圖顯示控制
 
    private int goldCoin = 0; // 玩家分數

    public Text timerText;               // 計時器
    private float currentTime = 0f; 
    private bool isTimerRunning = false; // 計時狀態

    public GameObject endLook;         // 死亡顯示面板
    public List<Text> endLookText = new List<Text>();
    
    // 玩家分數的属性
    public int GoldCoin
    {
        //這裡應該繼承之前累積的分數 若要跨廠場景物件還在，那他就不能被刪除!! 之後要改
        get { return goldCoin; }
        set { goldCoin = value; } // 這裡使用 private set 可以保證資料只能在腳本內部修改
    }

    // Start is called before the first frame update
    void Start()
    {
        isTimerRunning = true;
        endLook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimerCount();
    }

    //增加分數
    public void AddScore(int value)
    {
        Debug.Log("增加分數");
        GoldCoin += value;

        //將顯示的數值一值改變
        scoreText.text = string.Format("* {0,-5:#,###}", GoldCoin);
    }

    public void SeeMap()
    {
        Debug.Log(rawMapBool ? "打開Map" : "關閉Map");
        rawMapBool = !rawMapBool;
        rawImage.SetActive(rawMapBool);
    }

    public void SeeMap_D()
    {
        rawMapBool = false;
        rawImage.SetActive(rawMapBool);
        endLook.SetActive(true);
        ChangData();
    }

    void ChangData()
    {
        endLookText[0].text = timerText.text;
        endLookText[1].text = GoldCoin.ToString();
        endLookText[2].text = "0";

    }

    void TimerCount()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;

            // 将秒数转换为分钟和秒钟
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            // 更新 Text 对象显示的文本内容，格式为 MM:SS
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

}
