using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerConditions;

public class SaveData : MonoBehaviour
{
    [Header("SaveData")]
    public PlayerData playerData;
    public Inventory inventoryData;
    public PlayerConditions conditions;

    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void SavePlayerDataToJson()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(playerData);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "02.Scripts/JSON/PlayerSaveData/playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
        Debug.Log("저장됨");

    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "02.Scripts/JSON/PlayerSaveData/playerData.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }


    [Serializable]
    public class PlayerData
    {
        public Condition curHealth;
        public Transform curPosition;
        public ItemSlot[] slotsData;

    }

    private void Start()
    {
        playerData.slotsData = inventoryData.slots;
        playerData.curHealth = conditions.health;
    }
}
