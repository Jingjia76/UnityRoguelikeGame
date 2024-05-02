using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction{
        up,
        down,
        left,
        right
    };
    public Direction direction;

    [Header("房間訊息")]
    //獲得房間模型
    public GameObject roomPrefab;
    //生成房間數量
    public int roomNumber;
    //顏色有關區別
    public Color startColor,endColor;

    [Header("位置控制")]
    //Point
    public Transform generatorPoint;
    //位移變量
    public float xOffset;
    public float yOffset;

    //生成列表保存產生的關卡
    public List<GameObject> room = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //生成房間
        for(int i = 0; i < roomNumber; i++) {
            //生成，且不旋轉
            room.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity));
            //改變位置
            ChangePointPos();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //改變位置
    public void ChangePointPos()
    {
        //利用枚舉+switch製作位移
        direction = (Direction)Random.Range(0,4);
        // Debug.Log(":"+direction);

        switch(direction)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0,yOffset,0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0,-yOffset,0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(xOffset,0,0);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(-xOffset,0,0);
                break;
            default:
                Debug.Log("錯誤");
                break;
        }
    }
}
