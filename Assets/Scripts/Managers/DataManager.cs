using UnityEngine;
using VillageAdventure.Util;
using System.IO;
using UnityEngine.UI;
using VillageAdventure.Object;
using System.Collections.Generic;
using System.Linq;
using VillageAdventure.StaticData;
using VillageAdventure.DB;

namespace VillageAdventure
{
    public class DataManager : Singleton<DataManager>
    {
        private List<int> _mineNum = new List<int>();
        private List<int> _treeNum = new List<int>();
        private HomeObjectList homeObjData;
        private FieldObjectList fieldObjData;
        private MonsterList monsterData;
        private WarriorList warriorList;

        // 불러오기
        public void LoadGameData(string fileName)
        {
            InGameManager igManager = InGameManager.Instance;
            SaveData saveData = new SaveData();

            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";

            // 저장된 게임이 있다면
            if (File.Exists(filePath))
            {
                Debug.Log("불러오기 완료");
                // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
                string FromJsonData = File.ReadAllText(filePath);
                saveData = JsonUtility.FromJson<SaveData>(FromJsonData);

                igManager.playerHP = saveData.playerHP;
                igManager.isDead = saveData.isDead;
                igManager.hour = saveData.hour;
                igManager.min = saveData.min;
                igManager.time = saveData.time;
                igManager.mine = saveData.mine;
                igManager.tree = saveData.tree;
                igManager.fish = saveData.fish;
                igManager.food = saveData.food;
                igManager.score = saveData.score;

                UIMerchant.warriorAttackLevel = saveData.warriorAttackLevel;
                UIMerchant.playerMovespeedLevel = saveData.playerMovespeedLevel;
                UIMerchant.miningLevel = saveData.miningLevel;
                UIMerchant.loggingLevel = saveData.loggingLevel;
                UIMerchant.fishingLevel = saveData.fishingLevel;
                UIMerchant[] uiMerchant = Resources.FindObjectsOfTypeAll<UIMerchant>();
                uiMerchant[0].InitializeItemData();
                SetSkillData();

                // Resource 활성화 Num
                _mineNum = saveData.mineNum;
                igManager.mineHolder.GetComponent<Resource>().resourceList = new List<int>(_mineNum);
                _treeNum = saveData.treeNum;
                igManager.treeHolder.GetComponent<Resource>().resourceList = new List<int>(_treeNum);
                // Player Position
                igManager.player.transform.position = saveData.transform;

                // homeObj List
                for (var i = 0; i < saveData.homeObjectList.Count; i++)
                {
                    SDObject sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.nickName == saveData.homeObjectList[i].nickName).SingleOrDefault();
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath)).GetComponent<ScoreObject>();
                    sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    sdObjectClone.boScoreObject.hp = saveData.homeObjectList[i].hp;
                    Vector3 sdObjectPosition = saveData.homeObjectList[i].transform;
                    sdObjectClone.transform.position = sdObjectPosition;
                    sdObjectClone.transform.SetParent(igManager.homeObj.transform);
                }
                // fieldObj List
                for (var i = 0; i < saveData.fieldObjectList.Count; i++)
                {
                    SDObject sdObject = GameManager.SD.sdFieldObjects.Where(_ => _.nickName == saveData.fieldObjectList[i].nickName).SingleOrDefault();
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath)).GetComponent<ScoreObject>();
                    sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    sdObjectClone.boScoreObject.hp = saveData.fieldObjectList[i].hp;
                    Vector3 sdObjectPosition = saveData.fieldObjectList[i].transform;
                    sdObjectClone.transform.position = sdObjectPosition;
                    sdObjectClone.transform.SetParent(igManager.fieldObj.transform);
                }
                // monster List
                for (var i = 0; i < saveData.monsterList.Count; i++)
                {
                    SDActor sdMonster = GameManager.SD.sdMonsters.Where(_ => _.nickName == saveData.monsterList[i].nickName).SingleOrDefault();
                    var sdMonsterClone = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
                    sdMonsterClone.Initialize(new BoMonster(sdMonster));
                    igManager.monsters.Add(sdMonsterClone);
                    sdMonsterClone.boMonster.hp = saveData.monsterList[i].hp;
                    Vector3 sdMonsterPosition = saveData.monsterList[i].transform;
                    sdMonsterClone.transform.position = sdMonsterPosition;
                    sdMonsterClone.transform.SetParent(igManager.normalSlime.transform);
                }
                // warrior List
                for (var i = 0; i < saveData.warriorList.Count; i++)
                {
                    Destroy(igManager.warrior.transform.GetChild(i).gameObject);
                }
                for (var i = 0; i < saveData.warriorList.Count; i++)
                {
                    SDActor sdWarrior = GameManager.SD.sdNonePlayer.Where(_ => _.nickName == saveData.warriorList[i].nickName).SingleOrDefault();
                    var sdWarriorClone = Instantiate(Resources.Load<GameObject>(sdWarrior.resourcePath)).GetComponent<Warrior>();
                    sdWarriorClone.Initialize(new BoWarrior(sdWarrior));
                    igManager.charactors.Add(sdWarriorClone);
                    sdWarriorClone.boWarrior.hp = saveData.warriorList[i].hp;
                    Vector3 sdWarriorClonePosition = saveData.warriorList[i].transform;
                    sdWarriorClone.transform.position = sdWarriorClonePosition;
                    sdWarriorClone.transform.SetParent(igManager.warrior.transform);
                    sdWarriorClone.transform.SetSiblingIndex(saveData.warriorList[i].index);
                }

                // 마지막 monster Spawn Time
                igManager.spawningPool.GetComponent<SpawningPool>().lastMonsterSpawnTime = saveData.lastMonsterSpawnTime;

                // 현재 Scene
                GameManager.Instance.LoadScene(saveData.currentScene, null);
                GameManager.Instance.currentScene = saveData.currentScene;
                GameManager.Instance.SetObject();
            }
        }

        // 저장하기
        public void SaveGameData(string fileName)
        {
            InGameManager igManager = InGameManager.Instance;
            SaveData saveData = new SaveData();

            saveData.playerHP = igManager.playerHP;
            saveData.isDead = igManager.isDead;
            saveData.hour = igManager.hour;
            saveData.min = igManager.min;
            saveData.time = igManager.time;
            saveData.mine = igManager.mine;
            saveData.tree = igManager.tree;
            saveData.fish = igManager.fish;
            saveData.food = igManager.food;
            saveData.score = igManager.score;

            saveData.warriorAttackLevel = UIMerchant.warriorAttackLevel;
            saveData.playerMovespeedLevel = UIMerchant.playerMovespeedLevel;
            saveData.miningLevel = UIMerchant.miningLevel;
            saveData.loggingLevel = UIMerchant.loggingLevel;
            saveData.fishingLevel = UIMerchant.fishingLevel;

            // Resource 활성화 Num
            _mineNum = igManager.mineHolder.GetComponent<Resource>().resourceList;
            saveData.mineNum = new List<int>(_mineNum);
            _treeNum = igManager.mineHolder.GetComponent<Resource>().resourceList;
            saveData.treeNum = new List<int>(_treeNum);
            // Player Position
            saveData.transform = igManager.player.transform.position;
            // 현재 Scene
            saveData.currentScene = GameManager.Instance.currentScene;
            
            // homeObj List
            for(var i = 0; i < igManager.homeObj.transform.childCount; i++)
            {
                homeObjData = new HomeObjectList();
                homeObjData.hp = igManager.homeObj.transform.GetChild(i).GetComponent<ScoreObject>().boScoreObject.hp;
                homeObjData.transform = igManager.homeObj.transform.GetChild(i).transform.position;
                homeObjData.nickName = igManager.homeObj.transform.GetChild(i).tag;
                homeObjData.parent = igManager.homeObj.name;
                saveData.homeObjectList.Add(homeObjData);
            }
            // fieldObj List
            for (var i = 0; i < igManager.fieldObj.transform.childCount; i++)
            {
                fieldObjData = new FieldObjectList();
                fieldObjData.hp = igManager.fieldObj.transform.GetChild(i).GetComponent<ScoreObject>().boScoreObject.hp;
                fieldObjData.transform = igManager.fieldObj.transform.GetChild(i).transform.position;
                fieldObjData.nickName = igManager.fieldObj.transform.GetChild(i).tag;
                fieldObjData.parent = igManager.fieldObj.name;
                saveData.fieldObjectList.Add(fieldObjData);
            }
            // monster normalSlime List
            for (var i = 0; i < igManager.normalSlime.transform.childCount; i++)
            {
                monsterData = new MonsterList();
                monsterData.hp = igManager.normalSlime.transform.GetChild(i).GetComponent<Monster>().boMonster.hp;
                monsterData.transform = igManager.normalSlime.transform.GetChild(i).transform.position;
                monsterData.nickName = igManager.normalSlime.transform.GetChild(i).tag;
                monsterData.parent = igManager.normalSlime.name;
                saveData.monsterList.Add(monsterData);
            }

            // warrior List
            for (var i = 0; i < igManager.warrior.transform.childCount; i++)
            {
                if(igManager.warrior.transform.GetChild(i).gameObject.tag == "Warrior")
                {
                    warriorList = new WarriorList();
                    warriorList.hp = igManager.warrior.transform.GetChild(i).GetComponent<Warrior>().boActor.hp;
                    warriorList.transform = igManager.warrior.transform.GetChild(i).transform.position;
                    warriorList.nickName = igManager.warrior.transform.GetChild(i).tag;
                    warriorList.parent = igManager.warrior.name;
                    warriorList.index = i;
                    saveData.warriorList.Add(warriorList);
                }
            }
            // 마지막 monster Spawn Time
            saveData.lastMonsterSpawnTime = igManager.spawningPool.GetComponent<SpawningPool>().lastMonsterSpawnTime ;

            // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
            string ToJsonData = JsonUtility.ToJson(saveData, true);
            string filePath = Application.persistentDataPath + "/" + $"{fileName}.json";
            Debug.Log(filePath);
            // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
            File.WriteAllText(filePath, ToJsonData);
        }

        // Savefiles 이름 불러오기
        public string[] GetSaveFiles()
        {
            string path = Application.persistentDataPath;
            string[] files = Directory.GetFiles(path);

            // 파일들의 정보를 가져와서 최신 순서대로 정렬
            FileInfo[] fileInfos = files.Select(f => new FileInfo(f)).OrderByDescending(f => f.LastWriteTime).ToArray();

            // 정렬된 파일들을 다시 파일 경로 배열에 할당
            files = fileInfos.Select(f => f.FullName).ToArray();

            return files;
        }

        // Savefile 삭제하기
        public void DeleteSaveFile(string fileName)
        {
            Debug.Log($"filename = {fileName}");
            string _fileName = fileName + ".json";
            string path = Path.Combine(Application.persistentDataPath, _fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("파일 삭제됨: " + _fileName);
            }
            else
            {
                Debug.Log("파일을 찾을 수 없음: " + _fileName);
            }
            File.Delete(path);
        }

        // MD를 통한 Data Set
        public void SetSkillData()
        {
            Warrior[] warriors = Resources.FindObjectsOfTypeAll<Warrior>();
            foreach (Warrior warrior in warriors)
            {
                warrior.boWarrior.power += 20 * (UIMerchant.warriorAttackLevel - 1);
            }
            Player player = FindObjectOfType<Player>();
            player.boPlayer.moveSpeed += 3 * (UIMerchant.playerMovespeedLevel - 1);
        }
        // MD를 통한 Data Init
        public void InitSkillData()
        {
            Warrior[] warriors = Resources.FindObjectsOfTypeAll<Warrior>();
            foreach (Warrior warrior in warriors)
            {
                warrior.boWarrior.power -= 20 * (UIMerchant.warriorAttackLevel - 1);
            }
            Player player = FindObjectOfType<Player>();
            player.boPlayer.moveSpeed -= 3 * (UIMerchant.playerMovespeedLevel - 1);
        }
        // MD data 초기화
        public void InitData()
        {
            InitSkillData();
            UIMerchant.warriorAttackLevel = 1;
            UIMerchant.playerMovespeedLevel = 1;
            UIMerchant.miningLevel = 1;
            UIMerchant.loggingLevel = 1;
            UIMerchant.fishingLevel = 1;
            UIMerchant[] uiMerchant = Resources.FindObjectsOfTypeAll<UIMerchant>();
            uiMerchant[0].InitializeItemData();
            Player player = FindObjectOfType<Player>();
            SetSkillData();
        }
    }
}