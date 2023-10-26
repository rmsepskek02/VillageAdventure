using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Enum;

namespace VillageAdventure.Object
{
    public class Monster : Actor
    {
        string objName;
        public BoMonster boMonster;
        private float time = 0;
        private float lastMonsterMoveTime = 0f;
        private float monsterMoveInterval = 2f;
        public bool isMoving = true;
        public bool isAttack = false;
        private TriggerController trigger;

        public override void Initialize(BoActor boMonster)
        {
            base.Initialize(boMonster);
            this.boMonster = boMonster as BoMonster;
        }

        public override void Init()
        {
            objName = gameObject.name;
            objTagName = objName.Replace("(Clone)", "");
            trigger = transform.GetComponentInChildren<TriggerController>();
            CheckTrigger();
        }

        private void Update()
        {
            time += Time.deltaTime;

            if (time - lastMonsterMoveTime >= monsterMoveInterval)
            {
                if (!isAttack && isMoving)
                    SetMoveDir();
                lastMonsterMoveTime = time;
            }
        }
        public override void OnMove()
        {
            base.OnMove();
        }
        public override void OnMoveAnim() { }

        private void SetState(Collider2D collision = null)
        {
            if (isMoving)
            {
                State = MonsterState.State.Move;
            }
            if(!isMoving && !isAttack)
            {
                State = MonsterState.State.Idle;
            }
            if(collision != null)
            {
                if(collision.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    Debug.Log("DF");
                    return;
                }
                if(isAttack && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Debug.Log("AP");
                    return;
                }
                if(isAttack && collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
                {
                    Debug.Log("AM");
                    return;
                }
                if(collision.gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                {
                    Debug.Log("BB");
                    isAttack = true;
                    isMoving = false;
                    State = MonsterState.State.Alert;
                    boMonster.moveDirection = Vector2.zero;
                    rigid.velocity = new Vector2(0f, 0f);
                }
                else
                {
                    Debug.Log("OO");
                    isAttack = false;
                    isMoving = false;
                    SetMoveDir();
                }
            }
            else
            {
                isMoving = true;
                isAttack = false;
            }
        }

        private void SetFlipX()
        {
            if (boMonster.moveDirection != Vector2.zero)
            {
                
                if (boMonster.moveDirection.x < 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }
        }

        private void CheckTrigger()
        {
            trigger.Initialize(OnEnter, OnExit, OnStay);
            void OnEnter(Collider2D collision)
            {
                // 무언가 들어왔다
                isMoving = false;
                SetState(collision);
            }
            void OnExit(Collider2D collision)
            {
                // 무언가 없어졌다
                isMoving = true;
                if (collision.gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                    isAttack = false;
                State = MonsterState.State.Move;
            }
            void OnStay(Collider2D collision)
            {
                // 무언가 계속있다
            }
        }

        // 무작위 이동 Direction 설정 함수
        void SetMoveDir()
        {
            Vector2 preRandom = boMonster.moveDirection;
            int randomX = Random.Range(-1, 2);
            int randomY = Random.Range(-1, 2);
            Vector2 ranDir = new Vector2(randomX, randomY);
            boMonster.moveDirection = ranDir;

            if (preRandom == boMonster.moveDirection)
                SetMoveDir();

            SetFlipX();
            gameObject.transform.GetChild(0).transform.position = new Vector2(gameObject.transform.position.x + 
                boMonster.moveDirection.x/4, gameObject.transform.position.y + boMonster.moveDirection.y/4 - 0.3f);
            if (State == MonsterState.State.Move && ranDir == Vector2.zero)
                State = MonsterState.State.Idle;
            if (State == MonsterState.State.Idle || State == MonsterState.State.Move)
                SetState();
        }
    }
}
