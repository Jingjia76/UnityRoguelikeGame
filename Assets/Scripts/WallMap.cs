using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    //進入範圍在顯示
    GameObject mapSprite;

    private void OnEnable() {
        mapSprite = transform.parent.GetChild(0).gameObject;
    
        mapSprite.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //判斷是否為玩家
        if (other.CompareTag("Player"))
        {
            mapSprite.SetActive(true);
        }
    }
}
