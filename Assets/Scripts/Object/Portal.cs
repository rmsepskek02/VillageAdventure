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
        // �ش� ������ ����� ���������� ���� �����͸� ��� ����
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

                // ���������� ���� ���� ���� ���������� ���� ������ ���� �������� �ʵ忡 ����
                gameManager.prevStage = gameManager.currentScene;
                // ������ ������Ƿ� ����� ���������� ���� ������
                gameManager.LoadScene(bindStage, PortalProgress);

                void PortalProgress()
                {
                    // ���� �����Ͽ����Ƿ� ��Ż���� ������ ���� �ҷ���
                    inGameManager.InitPortals();
                    // ���� �������� ������ ������
                    var prevStage = gameManager.prevStage;
                    // �� ���� �Ϸ� ��, ��Ƶ� ���� ���������� ������ �̿��Ͽ� ���� ���������� ����� ��Ż �˻�
                    var bindPortal = inGameManager.portals.Where(_ => _.bindStage == prevStage).SingleOrDefault();
                    // BuildTrigger ������Ʈ�� ã�� isCollision�� false�� ����
                    BuildTrigger buildTrigger = collision.transform.GetChild(1).gameObject.GetComponent<BuildTrigger>();
                    buildTrigger.isCollision = false;
                    // �ش� ��Ż�� ĳ���͸� ��ġ��Ŵ
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