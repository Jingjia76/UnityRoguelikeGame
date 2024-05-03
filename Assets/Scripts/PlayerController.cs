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

    // Start is called before the first frame update
    void Start()
    {
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
}
