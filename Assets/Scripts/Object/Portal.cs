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

                // 스테이지를 변경 전에 현재 스테이지에 대한 정보를 이전 스테이지 필드에 담음
                GameManager.Instance.prevStage = GameManager.Instance.currentScene;
                // 정보를 담았으므로 연결된 스테이지로 씬을 변경함
                GameManager.Instance.LoadScene(bindStage, PortalProgress);

                void PortalProgress()
                {
                    // 씬을 변경하였으므로 포탈들의 정보를 새로 불러옴
                    inGameManager.InitPortals();
                    // 이전 스테이지 정보를 가져옴
                    var prevStage = GameManager.Instance.prevStage;
                    // 씬 변경 완료 후, 담아둔 이전 스테이지의 정보를 이용하여 이전 스테이지와 연결된 포탈 검사
                    var bindPortal = inGameManager.portals.Where(_ => _.bindStage == prevStage).SingleOrDefault();
                    // BuildTrigger 컴포넌트를 찾아 isCollision를 false로 설정
                    BuildTrigger buildTrigger = collision.transform.GetChild(1).gameObject.GetComponent<BuildTrigger>();
                    buildTrigger.isCollision = false;
                    // 해당 포탈로 캐릭터를 배치시킴
                    collision.transform.position = bindPortal.transform.GetChild(0).position;

                    GameManager.Instance.SetObject();
                }
            }
        }
        
    }
}