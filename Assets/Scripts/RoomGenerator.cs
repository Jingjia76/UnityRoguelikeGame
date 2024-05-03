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
    //最後的房間
    private GameObject endRoom;
    


    [Header("位置控制")]
    //Point
    public Transform generatorPoint;
    //位移變量
    public float xOffset;
    public float yOffset;
    //顯示要檢測的標籤
    public LayerMask roomLayer;

    //生成列表保存產生的關卡
    public List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        //生成房間
        for(int i = 0; i < roomNumber; i++) {
            //生成，且不旋轉
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>());
            //改變位置
            ChangePointPos();
        }

        //TODO:檢測房間_設定終點最遠房間距離
        //初始房間顏色改變
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;    

        //預設為初始房間，這樣在比較時越比越大，離初始房間越遠
        endRoom = rooms[0].gameObject;
        //檢測每個房間
        foreach (var room in rooms)
        {
            // //比較目標點和原始點距離 sqrMagnitude是一種比較方式 ()
            // if(room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude) 
            // {
            //     endRoom = room.gameObjet;
            // }
            SetupRoom(room,room.transform.position);
        }


        //最終房間顏色改變
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO:改變位置
    public void ChangePointPos()
    {
        do{
            //利用枚舉+switch製作位移
            direction = (Direction)Random.Range(0,4);

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
        }while(Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
        //後執行
    }

    //TODO:判斷上下左右有沒有房間並附值
    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        //判斷上下左右是否有其他房間
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0,yOffset,0),0.2f,roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0,-yOffset,0),0.2f,roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset,0,0),0.2f,roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset,0,0),0.2f,roomLayer);
    }
}
