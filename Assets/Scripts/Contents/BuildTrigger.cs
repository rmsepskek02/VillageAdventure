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
        public bool isCollision;
        public List<GameObject> collisionObject;
        private bool destroy;
        Sprite emptySprite;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.material.color = Color.green;
            ControlOfCollision();
            
            // 빈 사각형을 생성합니다.
            Texture2D texture = new Texture2D(100, 100);
            texture.Apply();

            // 빈 사각형 스프라이트를 생성하여 SpriteRenderer에 설정합니다.
            emptySprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        private void Update()
        {
            ControlOfScene();
            destroy = gameObject.transform.parent.gameObject.GetComponent<Player>().destroy;
            if (destroy == true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
            }
        }

        // 충돌로 Build 가능 여부 구분
        private void ControlOfCollision()
        {
            trigger = GetComponent<TriggerController>();
            trigger.Initialize(IsEnterObj, IsExitobj, IsStayobj);
            void IsEnterObj(Collider2D collision)
            {
                isCollision = true;
                sprite.material.color = Color.red;
                if (destroy)
                {
                    collisionObject.Clear();
                    if (collision.gameObject.layer != LayerMask.NameToLayer("Tilemap"))
                    {
                        collision.GetComponent<SpriteRenderer>().color = Color.black;
                        CheckList(collision.gameObject);
                    }
                }
            }
            void IsExitobj(Collider2D collision)
            {
                isCollision = false;
                sprite.material.color = Color.green;
                if (destroy)
                {
                    if (collision.gameObject.layer != LayerMask.NameToLayer("Tilemap"))
                    { 
                        collision.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        collisionObject.Clear();
                    }
                }
            }
            void IsStayobj(Collider2D collision)
            {
                if (destroy)
                {
                    if(collision.gameObject.layer != LayerMask.NameToLayer("Tilemap"))
                    {
                        collision.GetComponent<SpriteRenderer>().color = Color.black;
                        CheckList(collision.gameObject);
                    }
                }
            }
        }
        private void CheckList(GameObject obj)
        {
            if (!collisionObject.Contains(obj))
            {
                collisionObject.Add(obj);
            }
        }
        
        // Scene으로 Build 가능 여부 구분
        private void ControlOfScene()
        {
            currentScene = GameManager.Instance.currentScene;
            switch (currentScene)
            {
                case SceneType.House:
                    if (InGameManager.Instance.sdTypeIndex == 0 ||
                        InGameManager.Instance.sdTypeIndex == 2)
                    {
                        sprite.material.color = Color.green;
                    }
                    else
                    {
                        sprite.material.color = Color.red;
                    }
                    break;
                case SceneType.Field:
                    if (InGameManager.Instance.sdTypeIndex == 1 ||
                        InGameManager.Instance.sdTypeIndex == 2 ||
                        InGameManager.Instance.sdTypeIndex == 3)
                    {
                        sprite.material.color = Color.green;
                    }
                    else
                    {
                        sprite.material.color = Color.red;
                    }
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
            if (isCollision)
            {
                sprite.material.color = Color.red;
            }
            if(destroy == true)
            {
                sprite.material.color = new Color(0f, 0f, 0.7f, 0.5f);
            }
        }
    }
}
