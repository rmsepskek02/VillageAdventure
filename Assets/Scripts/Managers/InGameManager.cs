using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Object;
using VillageAdventure.DB;
using VillageAdventure.Util;
using System.Linq;

namespace VillageAdventure
{
    public class InGameManager : Singleton<InGameManager>
    {
        public bool UIBuildActivated = false;

        public List<Actor> charactors = new List<Actor>();
        public List<Actor> monsters = new List<Actor>();
        public List<Obj> objects = new List<Obj>();
        public List<Portal> portals = new List<Portal>();
        public GameObject player;
        public GameObject UI_inGame;
        public GameObject BuildObject;
        public GameObject startPoint;
        public GameObject holder;
        public GameObject buildObj;
        public GameObject spawn;
        public GameObject monster;
        public GameObject nonePlayer;
        public GameObject warrior;
        public int sdIndex;
        public int sdTypeIndex;
        public int warriorCountInLayer;
        public int warriorCount;
        public List<int> warriorIndexInLayer;

        #region UI_inGame
        public float playerHP { get; set; } = 100f;
        private bool isDead = false;
        public float hour = 0;
        public float min = 0;
        public float time = 0;
        public int mine = 0;
        public int tree = 0;
        public int fish = 0;
        public int food = 0;
        public int score = 0;
        #endregion

        private void Start()
        {
            UI_inGame = GameObject.Find("UI_inGame");
            startPoint = GameObject.Find("StartPoint");
            GeneratorCharactor();
            player = charactors[0].gameObject;
            BuildObject = player.transform.GetChild(1).gameObject;
            holder = GameObject.Find("Holder");
            buildObj= GameObject.Find("BuildObject");
            spawn= GameObject.Find("Spawn");
            monster = GameObject.Find("Monster");
            nonePlayer = GameObject.Find("NonePlayer");
            warrior = nonePlayer.transform.GetChild(0).gameObject;
            DontDestroyOnLoad(holder);
            DontDestroyOnLoad(buildObj);
            DontDestroyOnLoad(spawn);
            DontDestroyOnLoad(monster);
            DontDestroyOnLoad(nonePlayer);
        }

        private void Update()
        {
            ActiveUI();
            CalculateTime();
            ChangePlayerHP(Time.deltaTime * 0.2f);
            CheckWarrior();
            IndexWarriorWithLayer(warrior.transform, "Warrior");
            warriorCountInLayer = CountWarriorWithLayer(warrior.transform, "Warrior");
        }

        private void FixedUpdate()
        {
            charactors.Update();
            monsters.Update();
        }

        public void GeneratorCharactor()
        {
            // 씬에 플레이어 컨트롤러 객체를 찾음
            var playerController = FindObjectOfType<PlayerController>();
            var sdPlayer = GameManager.SD.sdPlayers.Where(_ => _.index == 1000).SingleOrDefault();
            var player = Instantiate(Resources.Load<GameObject>(sdPlayer.resourcePath)).GetComponent<Player>();
            player.Initialize(new BoPlayer(sdPlayer));
            playerController.Initialize(player);
            playerController.transform.position = startPoint.transform.position;
            // 생성한 캐릭터가 업데이트 될 수 있도록 전체 캐릭터 목록에 넣어줌
            charactors.Add(player);
        }

        public void InitPortals()
        {
            portals.Clear();
            var portalHolder = GameObject.Find("Portal").transform;

            for (int i = 0; i < portalHolder.childCount; ++i)
                portals.Add(portalHolder.GetChild(i).GetComponent<Portal>());
        }

        public void ChangePlayerHP(float value)
        {
            // 사망했다면 또는 타이틀 씬이면 변경 취소
            if (isDead == true || GameManager.Instance.currentScene == Enum.SceneType.Title)
                return;
            else
                playerHP = Mathf.Clamp(playerHP - value, 0, 100);

            // 사망 체크
            if (playerHP <= 0)
                isDead = true;
        }

        public void CalculateTime()
        {
            if (isDead == true || GameManager.Instance.currentScene == Enum.SceneType.Title)
                return;

            time += Time.deltaTime;
            if (time < 3600)
                min = time;
            else { hour++; min = 0; time = 0; }
        }

        public void ActiveUI()
        {
            if (GameManager.Instance.currentScene == Enum.SceneType.Title)
                UI_inGame.gameObject.SetActive(false);
            else
                UI_inGame.gameObject.SetActive(true);
        }

        public void CheckWarrior()
        {
            UI_inGame.transform.GetChild(0).gameObject.SetActive(false);
            UI_inGame.transform.GetChild(1).gameObject.SetActive(false);
            UI_inGame.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < warriorIndexInLayer.Count; i++)
            {
                UI_inGame.transform.GetChild(warriorIndexInLayer[i]).gameObject.SetActive(true);
            }
        }

        int CountWarriorWithLayer(Transform tr, string layerName)
        {
            int count = 0;
            for (int i = 0; i < tr.childCount; i++)
            {
                Transform child = tr.GetChild(i);
                if (child.gameObject.layer == LayerMask.NameToLayer(layerName))
                {
                    count++;
                }
                //count += CountWarriorWithLayer(child, layerName);
            }
            return count;
        }

        void IndexWarriorWithLayer(Transform tr, string layerName)
        {
            gameObject.transform.GetSiblingIndex();
            for (int i = 0; i < tr.childCount; i++)
            {
                Transform child = tr.GetChild(i);
                if (child.gameObject.layer == LayerMask.NameToLayer(layerName))
                {
                    if (!warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                        warriorIndexInLayer.Add(child.GetSiblingIndex());
                }
                else
                {
                    if (warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                        warriorIndexInLayer.Remove(child.GetSiblingIndex());
                }
            }
        }
    }
}