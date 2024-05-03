using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //獲得門位置的GameObject
    public GameObject doorLeft,doorRight,doorUp,doorDown;
    //是否顯示(門)
    public bool roomLeft,roomRight,roomUp,roomDown;


    //判斷上下左右是否有門，有就生成
    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
