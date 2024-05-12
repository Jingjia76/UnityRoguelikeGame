using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Transform Generate_Room_Door;
    //位移變量
    public float xOffset;
    public float yOffset;
    //顯示要檢測的標籤
    public LayerMask roomLayer;
    //最大房間的數值
    public int maxStep;

    //生成列表保存產生的關卡
    public List<Room> rooms = new List<Room>();
    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    //單獨入口房間
    List<GameObject> oneWayRooms = new List<GameObject>();

    //房間的牆~
    public WallType wallType;
    // Start is called before the first frame update
    void Start()
    {
        //生成房間
        for(int i = 0; i < roomNumber; i++) {
            //生成，且不旋轉
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity,Generate_Room_Door).GetComponent<Room>());
            //改變位置
            ChangePointPos();
        }

        //TODO:檢測房間_設定終點最遠房間距離
        //初始房間顏色改變
        // rooms[0].GetComponent<SpriteRenderer>().color = startColor;    

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

        FindEndRoom();

        //最終房間顏色改變
        // endRoom.GetComponent<SpriteRenderer>().color = endColor;
        //將最後一關改顏色
        endRoom.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.red;
        endRoom.tag = "EndRoom";
    }

    // Update is called once per frame
    void Update()
    {
        //案任意按鍵加載當前場景
        // if(Input.anyKeyDown)
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
        
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
    
        newRoom.UpdateRoom(xOffset,yOffset);

        //產生牆壁
        switch(newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleDown,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight,roomPosition,Quaternion.identity,Generate_Room_Door);
                break;
            case 2:
                if (newRoom.roomUp && newRoom.roomLeft)
                    Instantiate(wallType.doubleUL,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.doubleDL,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.doubleDR,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUD,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR,roomPosition,Quaternion.identity,Generate_Room_Door);           
                break;
            case 3:
                if (newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleURD,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomUp && newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(wallType.tripleULD,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomUp && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.tripleULR,roomPosition,Quaternion.identity,Generate_Room_Door);
                if (newRoom.roomDown && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.tripleDLR,roomPosition,Quaternion.identity,Generate_Room_Door);
                break;
            case 4:
                if (newRoom.roomUp && newRoom.roomDown && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.fourDoors,roomPosition,Quaternion.identity,Generate_Room_Door);
                break;
        }

    }

    //找到最遠的房間
    public void FindEndRoom()
    {
        //獲得最大數值
        for(int i = 0; i < rooms.Count; i++) {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;

        }
        //獲得最大和次大房間
        foreach (var room in rooms)
        {
            //第一種情況 最遠距離
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            //第二大狀況
            if (room.stepToStart == maxStep-1)
                lessFarRooms.Add(room.gameObject);

        }
        //獲得最遠單項門的房間
        for(int i = 0; i < farRooms.Count; i++) {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }                
        //獲得次遠距離單向門房間   
        for(int i = 0; i < lessFarRooms.Count; i++) {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }
        //兩個列表沒有單個門，可以做為出口
        if (oneWayRooms.Count != 0)
        {
            //一定為單向出口的房間
            endRoom = oneWayRooms[Random.Range(0,oneWayRooms.Count)];
        }else{
            //沒有單個出口的房間
            endRoom = farRooms[Random.Range(0,farRooms.Count)];
        }
    }
}
[System.Serializable]
public class WallType
{
    public GameObject singleUp,singleDown,singleLeft,singleRight,
                      doubleUL,doubleUR,doubleDL,doubleDR,doubleUD,doubleLR,
                      tripleURD,tripleULD,tripleULR,tripleDLR,
                      fourDoors;


}