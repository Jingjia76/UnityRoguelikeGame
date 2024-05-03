using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    //獲得門位置的GameObject
    public GameObject doorLeft,doorRight,doorUp,doorDown;
    //是否顯示(門)
    public bool roomLeft,roomRight,roomUp,roomDown;
    public Text stepToStartText; 
    public int stepToStart;

    //知道房間有幾個門是打開的
    public int doorNumber;

    //判斷上下左右是否有門，有就生成
    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    public void UpdateRoom(float xOffset,float yOffset)
    {
        //如何獲得初始房間和現在房間的網格距離 / 如果外面距離要改這裡也要改 /用(float)是因為12.3 為Dou 
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / (float)yOffset));
    
        stepToStartText.text = stepToStart.ToString();
        if (roomUp)doorNumber ++;
        if (roomDown)doorNumber ++;
        if (roomLeft)doorNumber ++;
        if (roomRight)doorNumber ++;

    }
}
