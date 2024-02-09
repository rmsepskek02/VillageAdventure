using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Object;
using VillageAdventure.DB;
using VillageAdventure.Util;
using System.Linq;
using UnityEngine.UI;

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
        public GameObject mineHolder;
        public GameObject treeHolder;
        public GameObject buildObj;
        public GameObject homeObj;
        public GameObject fieldObj;
        public GameObject spawn;
        public GameObject spawningPool;
        public GameObject monster;
        public GameObject normalSlime;
        public GameObject nonePlayer;
        public GameObject warrior;
        public GameObject testAWS;
        public Text guideText;
        public int sdIndex;
        public int sdTypeIndex;
        public int warriorCountInLayer;
        public int warriorCount;
        public List<int> warriorIndexInLayer;

        #region UI_inGame
        public float playerHP { get; set; } = 100f;
        public bool isDead = false;
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
            testAWS = GameObject.Find("TestAWS");
            UI_inGame = GameObject.Find("UI_inGame");
            startPoint = GameObject.Find("StartPoint");
            GeneratorCharactor();
            player = charactors[0].gameObject;
            BuildObject = player.transform.GetChild(1).gameObject;
            holder = GameObject.Find("Holder");
            mineHolder = holder.transform.Find("MineHolder").gameObject;
            treeHolder = holder.transform.Find("TreeHolder").gameObject;
            buildObj = GameObject.Find("BuildObject");
            homeObj = buildObj.transform.Find("HomeObject").gameObject;
            fieldObj = buildObj.transform.Find("FieldObject").gameObject;
            spawn = GameObject.Find("Spawn");
            spawningPool = spawn.transform.Find("SpawningPool").gameObject;
            monster = GameObject.Find("Monster");
            normalSlime = monster.transform.Find("SlimeNormal").gameObject;
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
            CheckWarriorUI();
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
            {
                isDead = true;
                if(Time.timeScale != 0f)
                {
                    UI_inGame.transform.GetChild(2).gameObject.SetActive(false);
                    GameManager.Instance.PauseGame();
                }
            }
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
            if (isDead)
            {
                // 현재신의 오디오를 찾아서 클립 바꾸기
                // UI 띄우기
                UI_inGame.transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                UI_inGame.transform.GetChild(4).gameObject.SetActive(false);
            }
            if (GameManager.Instance.isPause == true)
            {
                GameManager.Instance.PauseGame();
                UI_inGame.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                GameManager.Instance.ReStartGame();
                UI_inGame.transform.GetChild(5).gameObject.SetActive(false);
            }
        }

        public void CheckWarriorUI()
        {
            UI_inGame.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            UI_inGame.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            UI_inGame.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < warriorIndexInLayer.Count; i++)
            {
                UI_inGame.transform.GetChild(0).transform.GetChild(warriorIndexInLayer[i]).gameObject.SetActive(true);
            }
            if (GameManager.Instance.currentScene != Enum.SceneType.Field)
            {
                UI_inGame.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                UI_inGame.transform.GetChild(0).gameObject.SetActive(true);
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

        // 게임 초기화
        /// <summary>
        /// 모든 오브젝트 초기화
        /// 재화 초기화
        /// 플레이어 청사진 관련 초기화
        /// 게임시간 초기화
        /// 젠 관련 초기화
        /// 플레이어 위치 처음으로 되돌리기
        /// 죽으면 키입력제거
        /// </summary>
        public void ResetGame()
        {
            playerHP = 100f;
            isDead = false;
            hour = 0;
            min = 0;
            time = 0;
            mine = 10;
            tree = 10;
            fish = 10;
            food = 10;
            score = 0;

            // 플레이어 초기화
            /// 청사진, 위치
            player.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = null;
            player.transform.position = new Vector3(0, 0, 0);

            // 젠 관련 초기화
            spawningPool.GetComponent<SpawningPool>().lastMonsterSpawnTime = 0;

            // mine 초기화
            mineHolder.SetActive(false);
            for (int i = 0; i < mineHolder.transform.childCount; i++)
            {
                GameObject childObject = mineHolder.transform.GetChild(i).gameObject;
                childObject.SetActive(false); // 자식 오브젝트 비활성화
            }
            // forest 초기화
            treeHolder.SetActive(false);
            for (int i = 0; i < treeHolder.transform.childCount; i++)
            {
                GameObject childObject = treeHolder.transform.GetChild(i).gameObject;
                childObject.SetActive(false); // 자식 오브젝트 비활성화
            }
            // home object 초기화
            for (int i = 0; i < homeObj.transform.childCount; i++) 
            {
                GameObject childObject = homeObj.transform.GetChild(i).gameObject;
                Destroy(childObject);
            }
            homeObj.SetActive(true);
            // fielld object 초기화
            for (int i = 0; i < fieldObj.transform.childCount; i++)
            {
                GameObject childObject = fieldObj.transform.GetChild(i).gameObject;
                Destroy(childObject);
            }
            fieldObj.SetActive(true);
            // normal slime 초기화
            spawningPool.SetActive(false);
            normalSlime.SetActive(false);
            for (int i = 0; i < normalSlime.transform.childCount; i++)
            {
                GameObject childObject = normalSlime.transform.GetChild(i).gameObject;
                Destroy(childObject);
            }
            // warrior 초기화
            warrior.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                GameObject childObject = warrior.transform.GetChild(i).gameObject;
                Destroy(childObject);
                GameObject emptyObject = new GameObject($"EmptyObject{gameObject.transform.GetSiblingIndex()}");
                emptyObject.transform.SetParent(warrior.transform);
            }
        }
        private bool isCoroutineRunning = false;
        public void SetGuideUI(string text, bool isActive)
        {
            guideText.text = text;
            guideText.gameObject.SetActive(isActive);
            if (isActive) 
                StartCoroutine(DeActive());
        }
        public IEnumerator DeActive()
        {
            if (!isCoroutineRunning)
            {
                isCoroutineRunning = true;
                yield return new WaitForSeconds(3.0f);
                guideText.gameObject.SetActive(false);
                isCoroutineRunning = false;
            }
        }
    }
}