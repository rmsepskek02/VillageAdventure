using UnityEngine;
using VillageAdventure.Util;
using System.IO;
using UnityEngine.UI;

namespace VillageAdventure
{
    public class DataManager : Singleton<DataManager>
    {
        // �ҷ�����
        public void LoadGameData(string fileName)
        {
            SaveData saveData = new SaveData();
            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";

            // ����� ������ �ִٸ�
            if (File.Exists(filePath))
            {
                // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
                string FromJsonData = File.ReadAllText(filePath);
                saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
                Debug.Log("�ҷ����� �Ϸ�");
                InGameManager.Instance.mine = saveData.mine;
                InGameManager.Instance.tree = saveData.tree;
            }
        }

        // �����ϱ�
        public void SaveGameData(string fileName)
        {
            SaveData saveData = new SaveData();

            saveData.mine = InGameManager.Instance.mine;
            saveData.tree= InGameManager.Instance.tree;

            // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
            string ToJsonData = JsonUtility.ToJson(saveData, true);
            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";
            // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
            File.WriteAllText(filePath, ToJsonData);
        }

        // Savefiles �̸� �ҷ�����
        public string[] GetSaveFiles()
        {
            string path = Application.persistentDataPath;
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }
}