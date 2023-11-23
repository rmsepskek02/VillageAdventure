using UnityEngine;
using VillageAdventure.Util;
using System.IO;
using UnityEngine.UI;

namespace VillageAdventure
{
    public class DataManager : Singleton<DataManager>
    {
        // 불러오기
        public void LoadGameData(string fileName)
        {
            SaveData saveData = new SaveData();
            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";

            // 저장된 게임이 있다면
            if (File.Exists(filePath))
            {
                // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
                string FromJsonData = File.ReadAllText(filePath);
                saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
                Debug.Log("불러오기 완료");
                InGameManager.Instance.mine = saveData.mine;
                InGameManager.Instance.tree = saveData.tree;
            }
        }

        // 저장하기
        public void SaveGameData(string fileName)
        {
            SaveData saveData = new SaveData();

            saveData.mine = InGameManager.Instance.mine;
            saveData.tree= InGameManager.Instance.tree;

            // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
            string ToJsonData = JsonUtility.ToJson(saveData, true);
            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";
            // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
            File.WriteAllText(filePath, ToJsonData);
        }

        // Savefiles 이름 불러오기
        public string[] GetSaveFiles()
        {
            string path = Application.persistentDataPath;
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }
}