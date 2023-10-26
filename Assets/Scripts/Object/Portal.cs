using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Util;

namespace VillageAdventure.Object
{
    using SceneType = Enum.SceneType;
    public class Portal : Obj
    {
        // 해당 포털이 연결된 스테이지에 대한 데이터를 들고 있음
        public SceneType bindStage;

        private TriggerController portalTrigger;

        public override void Init()
        {
            UsingPortal();
        }
        private void UsingPortal()
        {
            portalTrigger = GetComponent<TriggerController>();
            var currentScene = GameManager.Instance.currentScene;
            portalTrigger.Initialize(OnEnterPortal, null, null);

            void OnEnterPortal(Collider2D collision)
            {
                if (!collision.CompareTag("Player"))
                    return;
                //else
                    //collision.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = null;

                var gameManager = GameManager.Instance;
                var inGameManager = InGameManager.Instance;

                // 스테이지를 변경 전에 현재 스테이지에 대한 정보를 이전 스테이지 필드에 담음
                gameManager.prevStage = gameManager.currentScene;
                // 정보를 담았으므로 연결된 스테이지로 씬을 변경함
                gameManager.LoadScene(bindStage, PortalProgress);

                void PortalProgress()
                {
                    // 씬을 변경하였으므로 포탈들의 정보를 새로 불러옴
                    inGameManager.InitPortals();
                    // 이전 스테이지 정보를 가져옴
                    var prevStage = gameManager.prevStage;
                    // 씬 변경 완료 후, 담아둔 이전 스테이지의 정보를 이용하여 이전 스테이지와 연결된 포탈 검사
                    var bindPortal = inGameManager.portals.Where(_ => _.bindStage == prevStage).SingleOrDefault();
                    // BuildTrigger 컴포넌트를 찾아 isCollision를 false로 설정
                    BuildTrigger buildTrigger = collision.transform.GetChild(1).gameObject.GetComponent<BuildTrigger>();
                    buildTrigger.isCollision = false;
                    // 해당 포탈로 캐릭터를 배치시킴
                    collision.transform.position = bindPortal.transform.GetChild(0).position;

                    GameObject buildObj = GameObject.Find("BuildObject");
                    GameObject homeObj = buildObj.transform.Find("HomeObject").gameObject;
                    GameObject fieldObj= buildObj.transform.Find("FieldObject").gameObject;
                    GameObject holder = GameObject.Find("Holder");
                    GameObject mineHolder = holder.transform.Find("MineHolder").gameObject;
                    GameObject treeHolder = holder.transform.Find("TreeHolder").gameObject;
                    GameObject spawn = GameObject.Find("Spawn");
                    GameObject spawningPool = spawn.transform.Find("SpawningPool").gameObject;
                    GameObject monster = GameObject.Find("Monster");
                    GameObject normalSlime = monster.transform.Find("SlimeNormal").gameObject;
                    GameObject nonePlayer = GameObject.Find("NonePlayer");
                    GameObject warrior = nonePlayer.transform.Find("Warrior").gameObject;
                    if (gameManager.currentScene == SceneType.Mine)
                    {
                        homeObj.SetActive(false);
                        fieldObj.SetActive(false);
                        mineHolder.SetActive(true);
                        treeHolder.SetActive(false);
                        spawningPool.SetActive(false);
                        normalSlime.SetActive(false);
                        warrior.SetActive(false);
                    }
                    else if (gameManager.currentScene == SceneType.Forest)
                    {
                        homeObj.SetActive(false);
                        fieldObj.SetActive(false);
                        mineHolder.SetActive(false);
                        treeHolder.SetActive(true);
                        spawningPool.SetActive(false);
                        normalSlime.SetActive(false);
                        warrior.SetActive(false);
                    }
                    else if (gameManager.currentScene == SceneType.Field)
                    {
                        homeObj.SetActive(false);
                        fieldObj.SetActive(true);
                        mineHolder.SetActive(false);
                        treeHolder.SetActive(false);
                        spawningPool.SetActive(true);
                        normalSlime.SetActive(true);
                        warrior.SetActive(true);
                    }
                    else if (gameManager.currentScene == SceneType.House)
                    {
                        homeObj.SetActive(true);
                        fieldObj.SetActive(false);
                        mineHolder.SetActive(false);
                        treeHolder.SetActive(false);
                        spawningPool.SetActive(false);
                        normalSlime.SetActive(false);
                        warrior.SetActive(false);
                    }
                    else if (gameManager.currentScene == SceneType.FishingZone)
                    {
                        homeObj.SetActive(false);
                        fieldObj.SetActive(false);
                        mineHolder.SetActive(false);
                        treeHolder.SetActive(false);
                        spawningPool.SetActive(false);
                        normalSlime.SetActive(false);
                        warrior.SetActive(false);
                    }
                }
            }
        }
    }
}