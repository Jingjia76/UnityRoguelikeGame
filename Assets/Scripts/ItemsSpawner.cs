using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [Header("生成物件")]
    public Vector2 minOffset;        // 最小的偏移量
    public Vector2 maxOffset;        // 最大的偏移量
    public int minItemCount = 1;     // 最少生成的物品數量
    public int maxItemCount = 5;     // 最多生成的物品數量

    [Header("填入要找生成物品的物件掛載物件")]
    public string FindAddObjScriptName;
    public Transform itemPos;
    //拿物件
    private GameObject FatherObj;
    private ItemController itemController; // 引用 ItemController
    //HashMap 用於生成不重複位置
    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();

    private void Awake() {        
        //拿到物件後 1.拿程式碼 2.拿位置
        FatherObj =  GameObject.Find(FindAddObjScriptName); 
        //另一種方式 FatherObj = transform.parent.parent.gameObject;
        itemController = FatherObj.GetComponent<ItemController>();
        // itemPos = FatherObj.transform.GetChild(0).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //生成的方法
    void GenerateItems()
    {
        string objectTag = gameObject.tag;

        //TODO:如果是最後一關
        if (objectTag == "EndRoom")
        {
            Debug.Log("到達最後一關");
            // 在原地生成離開出口
            GameObject selectedPrefab = itemController.itemPrefabs[0]; // 使用 ItemController 中的 itemPrefabs
            Vector3 spawnPosition = transform.position;
            InstantiateItem(selectedPrefab, spawnPosition,itemPos); 
        }
        else
        {
            // 生成隨機數量的物品
            int itemCount = Random.Range(minItemCount, maxItemCount + 1);

            for (int i = 0; i < itemCount; i++)
            {
                // 隨機選擇要生成的預製物件
                GameObject selectedPrefab = itemController.itemPrefabs[Random.Range(1, itemController.itemPrefabs.Length)]; // 使用 ItemController 中的 itemPrefabs

                // 隨機生成位置，直到找到一個未使用的位置
                Vector3 spawnPosition;
                do
                {
                    float offsetX = Random.Range(minOffset.x, maxOffset.x);
                    float offsetY = Random.Range(minOffset.y, maxOffset.y);
                    spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);
                } while (spawnedPositions.Contains(spawnPosition));

                // 將位置添加到已生成的位置集合中
                spawnedPositions.Add(spawnPosition);

                // 生成物品，不再賦值給任何變量
                InstantiateItem(selectedPrefab, spawnPosition,itemPos);
            }
        }
    }

    void InstantiateItem(GameObject prefab, Vector3 position,Transform itemPos)
    {
        GameObject item = Instantiate(prefab, position, Quaternion.identity,itemPos);
        // item.transform.SetParent(transform);
    }
}