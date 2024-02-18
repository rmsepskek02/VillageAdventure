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

                // ���������� ���� ���� ���� ���������� ���� ������ ���� �������� �ʵ忡 ����
                GameManager.Instance.prevStage = GameManager.Instance.currentScene;
                // ������ ������Ƿ� ����� ���������� ���� ������
                GameManager.Instance.LoadScene(bindStage, PortalProgress);

                void PortalProgress()
                {
                    // ���� �����Ͽ����Ƿ� ��Ż���� ������ ���� �ҷ���
                    InGameManager.Instance.InitPortals();
                    // ���� �������� ������ ������
                    var prevStage = GameManager.Instance.prevStage;
                    // �� ���� �Ϸ� ��, ��Ƶ� ���� ���������� ������ �̿��Ͽ� ���� ���������� ����� ��Ż �˻�
                    var bindPortal = InGameManager.Instance.portals.Where(_ => _.bindStage == prevStage).SingleOrDefault();
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