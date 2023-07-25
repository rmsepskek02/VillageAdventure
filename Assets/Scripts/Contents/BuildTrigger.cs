using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VillageAdventure.Enum;

namespace VillageAdventure.Object
{
    class BuildTrigger : TriggerController
    {
        private TriggerController trigger;
        private SpriteRenderer sprite;
        private SceneType currentScene;
        private bool isCollision;
        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.material.color = Color.green;
            ControlOfCollision();
        }
        private void Update()
        {
            ControlOfScene();
        }

        // 충돌로 Build 가능 여부 구분
        private void ControlOfCollision()
        {
            trigger = GetComponent<TriggerController>();
            trigger.Initialize(IsEnterObj, IsExitobj, null);
            void IsEnterObj(Collider2D collision)
            {
                isCollision = true;
                sprite.material.color = Color.red;
            }
            void IsExitobj(Collider2D collision)
            {
                isCollision = false;
                sprite.material.color = Color.green;
            }
        }

        // Scene으로 Build 가능 여부 구분
        private void ControlOfScene()
        {
            if (isCollision)
            {
                sprite.material.color = Color.red;
                return;
            }
            currentScene = GameManager.Instance.currentScene;
            switch (currentScene)
            {
                case SceneType.House:
                    if (InGameManager.Instance.sdTypeIndex == 0 ||
                        InGameManager.Instance.sdTypeIndex == 2)
                        sprite.material.color = Color.green;
                    else
                        sprite.material.color = Color.red;
                    break;
                case SceneType.Field:
                    if (InGameManager.Instance.sdTypeIndex == 1 ||
                        InGameManager.Instance.sdTypeIndex == 3)
                        sprite.material.color = Color.green;
                    else
                        sprite.material.color = Color.red;
                    break;
                case SceneType.Mine:
                    sprite.material.color = Color.red;
                    break;
                case SceneType.Forest:
                    sprite.material.color = Color.red;
                    break;
                case SceneType.FishingZone:
                    sprite.material.color = Color.red;
                    break;
            }
        }
    }
}
