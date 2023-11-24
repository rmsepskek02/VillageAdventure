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
        InGameManager inGameManager;

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

                // ���������� ���� ���� ���� ���������� ���� ������ ���� �������� �ʵ忡 ����
                GameManager.Instance.prevStage = GameManager.Instance.currentScene;
                // ������ ������Ƿ� ����� ���������� ���� ������
                GameManager.Instance.LoadScene(bindStage, PortalProgress);

                void PortalProgress()
                {
                    // ���� �����Ͽ����Ƿ� ��Ż���� ������ ���� �ҷ���
                    inGameManager.InitPortals();
                    // ���� �������� ������ ������
                    var prevStage = GameManager.Instance.prevStage;
                    // �� ���� �Ϸ� ��, ��Ƶ� ���� ���������� ������ �̿��Ͽ� ���� ���������� ����� ��Ż �˻�
                    var bindPortal = inGameManager.portals.Where(_ => _.bindStage == prevStage).SingleOrDefault();
                    // BuildTrigger ������Ʈ�� ã�� isCollision�� false�� ����
                    BuildTrigger buildTrigger = collision.transform.GetChild(1).gameObject.GetComponent<BuildTrigger>();
                    buildTrigger.isCollision = false;
                    // �ش� ��Ż�� ĳ���͸� ��ġ��Ŵ
                    collision.transform.position = bindPortal.transform.GetChild(0).position;

                    GameManager.Instance.SetObject();
                }
            }
        }
        
    }
}