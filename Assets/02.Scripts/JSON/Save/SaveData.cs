using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;




public class SaveData : MonoBehaviour
{

    //public Inventory inventory;
    //[System.Serializable]
    //public class PlayerData
    //{
    //    public ItemSlot inventory;
    //    public Vector3 curPosition = controller.transform.position;

    //}

    //PlayerData playerData;

    //[ContextMenu("To Json Data")]
    //void SaveDataToJson()
    //{
    //    string data = JsonUtility.ToJson(playerData);
    //    string path = Path.Combine(Application.dataPath, "02.Scripts/JSON/PlayerSaveData/playerData.json");
    //    File.WriteAllText(path, data);
    //    Debug.Log("슬롯" + playerData.inventory);
    //    Debug.Log("위치" + playerData.curPosition);

    //}

    //[ContextMenu("From Json Data")]
    //void LoadDataFromJson()
    //{
    //    // 데이터를 불러올 경로 지정
    //    string path = Path.Combine(Application.dataPath, "02.Scripts/JSON/PlayerSaveData/playerData.json");
    //    // 파일의 텍스트를 string으로 저장
    //    string data = File.ReadAllText(path);
    //    // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
    //    playerData = JsonUtility.FromJson<PlayerData>(data);

    //}

    //private void Awake()
    //{
    //    controller = GetComponent<PlayerController>();
    //    inventory = GetComponent<Inventory>();
    //}



    //private void Start()
    //{
    //    // 플레이어 위치 로드
    //    LoadPlayerPosition();
    //}

    //private void OnDestroy()
    //{
    //    // 게임 종료 시 플레이어 위치 저장
    //    SavePlayerPosition();
    //}

    //플레이어 위치 저장위치


    //public Inventory inventory;

    //public GameObject container;
    //private void Awake()
    //{
    //    inventory = GetComponent<Inventory>();
    //}

    //[System.Serializable]
    //private class PlayerCameraPositionData
    //{
    //    public float x;
    //    public float y;
    //    public float z;
    //    public float w;
    //}
    //public void SavePlayerCameraPosition()
    //{
    //    Quaternion playerCameraPosition = container.transform.rotation;

    //    PlayerCameraPositionData cameraPositionData = new PlayerCameraPositionData
    //    {
    //        x = playerCameraPosition.x,
    //        y = playerCameraPosition.y,
    //        z = playerCameraPosition.z,
    //        w = playerCameraPosition.w
    //    };

    //    string json = JsonUtility.ToJson(cameraPositionData);
    //    File.WriteAllText(cameraSavePath, json);

    //    Debug.Log("카메라 저장위치" + playerCameraPosition);
    //}
    //public void LoadPlayerCameraPosition()
    //{
    //    if (File.Exists(cameraSavePath))
    //    {
    //        string json = File.ReadAllText(cameraSavePath);
    //        PlayerCameraPositionData positionCameraData = JsonUtility.FromJson<PlayerCameraPositionData>(json);

    //        container.transform.rotation = new Quaternion(positionCameraData.x, positionCameraData.y, positionCameraData.z, positionCameraData.w);

    //        Debug.Log("카메라 불러온 위치" +
    //        positionCameraData.x +
    //        positionCameraData.y +
    //        positionCameraData.z +
    //        positionCameraData.w);
    //    }
    //    else
    //    {
    //        Debug.Log("저장 된 거 없음");
    //    }
    //}



    //[System.Serializable]
    //private class InventoryData
    //{
    //    public ItemSlot[] slots;
    //}

    //public void SaveInventory()
    //{
    //    InventoryData inventoryData = new InventoryData
    //    {
    //        slots = new ItemSlot[inventory.slots.Length]
    //    };

    //    for (int i = 0; i < inventory.slots.Length; i++)
    //    {
    //        // ItemSlot 객체를 새로 생성하여 데이터 복사
    //        inventoryData.slots[i] = new ItemSlot
    //        {
    //            item = inventory.slots[i].item,
    //            quantity = inventory.slots[i].quantity
    //        };
    //    }

    //    string json = JsonUtility.ToJson(inventoryData);
    //    File.WriteAllText(InventorySavePath, json);

    //    inventory.UpdateUI();
    //}

    //public void LoadInventory()
    //{
    //    if (File.Exists(InventorySavePath))
    //    {
    //        string json = File.ReadAllText(InventorySavePath);
    //        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

    //        // 로드한 데이터를 슬롯에 복사
    //        for (int i = 0; i < inventory.slots.Length && i < inventoryData.slots.Length; i++)
    //        {
    //            // ItemSlot 객체를 새로 생성하여 데이터 복사
    //            inventory.slots[i] = new ItemSlot
    //            {
    //                item = inventoryData.slots[i].item,
    //                quantity = inventoryData.slots[i].quantity
    //            };
    //        }

    //        // UI 업데이트
    //        inventory.UpdateUI();

    //        Debug.Log("인벤토리 저장 내용: " + inventory.slots[0]);
    //    }
    //    else
    //    {
    //        Debug.Log("저장된 인벤토리 없음.");
    //    }
    //}

    public GameObject thisButton; //이번 체크포인트
    public GameObject nextButton; //다음 체크 포인트

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") //플레이어랑 붙딛히면 
        {
            SoundManager.instance.PlayInteractionEffect();
            thisButton.SetActive(false); //이번 체크포인트 끄고
            SavePlayerPosition();        //플레이어 위치 저장

            if (nextButton != null)
            {
                nextButton.SetActive(true);  //다음 체크포인트 키기
            }
        }
    }

    private string savePath = "Assets/02.Scripts/JSON/PlayerSaveData/playerData.json";//저장경로

    public PlayerController controller;

    [System.Serializable]
    private class PlayerPositionData //플레이어 좌표값
    {
        public float x;
        public float y;
        public float z;
    }

    [ContextMenu("To Json Data")]
    private void SavePlayerPosition()
    {
        Vector3 playerPosition = controller.transform.position; //좌표값 받아오기

        PlayerPositionData positionData = new PlayerPositionData
        {
            x = playerPosition.x,
            y = playerPosition.y,
            z = playerPosition.z
        };

        string json = JsonUtility.ToJson(positionData);
        File.WriteAllText(savePath, json);

        Debug.Log("저장위치" + playerPosition);
    }

    [ContextMenu("From Json Data")]
    // 추가로 로드할 때 사용할 메서드
    public void LoadPlayerPosition()//저장된 세이브 불러오기
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerPositionData positionData = JsonUtility.FromJson<PlayerPositionData>(json);

            // 저장된 위치로 플레이어 이동
            controller.transform.position = new Vector3(positionData.x, positionData.y, positionData.z);

            Debug.Log("불러온 위치" + controller.transform.position);
    
        }
        else
        {
            SetPosition();
            LoadPlayerPosition();
        }
    }

    [ContextMenu("Set Basic Json Data")]
    public void SetPosition()
    {
        Vector3 playerPosition = new Vector3(150f, 0.7f, -71f);

        PlayerPositionData positionData = new PlayerPositionData
        {
            x = playerPosition.x,
            y = playerPosition.y,
            z = playerPosition.z
        };

        string json = JsonUtility.ToJson(positionData);
        File.WriteAllText(savePath, json);

        Debug.Log("저장위치" + playerPosition);
    }
}
