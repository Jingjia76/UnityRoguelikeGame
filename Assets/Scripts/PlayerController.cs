using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //獲得角色上資訊
    Rigidbody2D rb;
    Animator anim;

    //移動
    Vector2 movement;
    public float speed;

    private PlayerData playerData; // 玩家資料腳本的引用

    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>(); // 取得玩家資料腳本的實例

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //移動的值
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //左右水平翻轉
        if (movement.x != 0) transform.localScale = new Vector3(movement.x,1,1);
        
        SwitchAnim();
        
    }
    private void FixedUpdate() {
        //這裡時間使用的方式是用fixed不是用DeltaTime
        rb.MovePosition(rb.position + movement*speed*Time.fixedDeltaTime);
    }

    //切換動畫
    void SwitchAnim()
    {
        anim.SetFloat("speed",movement.magnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //判斷是否為玩家
        if (other.CompareTag("EndRoom"))
        {
            Debug.Log("抵達終點");
        }
        //判斷是否為金幣
        if (other.CompareTag("GoldCoin"))
        {
            playerData.AddScore(1); //增加的方法
            Debug.Log("獲得金幣");
            //刪除金幣
            Destroy(other.gameObject,0.5f);
        }
        //判斷是否找到最後的寶物
        if (other.CompareTag("EndTreasure"))
        {
            //執行離開或是顯示東西
            Debug.Log("碰到離開物件");
            // 強制轉成關閉
            // playerData.rawMapBool = false;
            // playerData.rawImage.SetActive(rawMapBool);
            playerData.SeeMap_D();

            Time.timeScale = 0;
        }
        
    }


}
