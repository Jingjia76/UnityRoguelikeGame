using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    //攝影機的移動速度
    public float speed;
    public Transform target;


    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) 
            //
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.position.x,target.position.y,transform.position.z),speed*Time.deltaTime);
    
    }

    //更新目標的方法
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
