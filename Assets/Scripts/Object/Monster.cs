using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Enum;

namespace VillageAdventure.Object
{
    public class Monster : Actor
    {
        string _objName;
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
            _objName = gameObject.name;
            objTagName = _objName.Replace("(Clone)", "");
            trigger = transform.GetComponentInChildren<TriggerController>();
            CheckTrigger();
        }

        void Update()
        {
            if (!isAttack)
                SetMoveDir();
            if(isMoving)
                MoveMonster();
        }
        public override void OnMove()
        {
            base.OnMove();
        }

        private void SetState(Collider2D collision = null)
        {
            if(isMoving)
            {
                State = ActorState.State.Move;
                SetFlipX();
            }
            if(!isMoving && !isAttack)
            {
                State = ActorState.State.Idle;
            }
            if(collision != null)
            {
                if(collision.name.Replace("(Clone)", "") == "Well")
                {
                    isAttack = true;
                    State = ActorState.State.Attack;
                }
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

        private void MoveMonster()
        {
            transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position +
                    boMonster.moveDirection.normalized * Time.deltaTime * boActor.moveSpeed, 1.0f);
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
                isAttack = false;
                State = ActorState.State.Move;
            }
            void OnStay(Collider2D collision)
            {
                // 무언가 계속있다
                isMoving = false;
                SetState(collision);
            }
        }

        // 무작위 이동 Direction 설정 함수
        void SetMoveDir()
        {
            time += Time.deltaTime;

            if (time - lastMonsterMoveTime >= monsterMoveInterval)
            {
                int randomX = Random.Range(-1, 2);
                int randomY = Random.Range(-1, 2);
                Vector2 ranDir = new Vector2(randomX, randomY);
                boMonster.moveDirection = ranDir;
                lastMonsterMoveTime = time;
                gameObject.transform.GetChild(0).transform.position = new Vector2(gameObject.transform.position.x + 
                    boMonster.moveDirection.x/4, gameObject.transform.position.y + boMonster.moveDirection.y/4 - 0.3f);
                if (State == ActorState.State.Move && ranDir == Vector2.zero)
                {
                    State = ActorState.State.Idle;
                }
                if (State == ActorState.State.Idle || State == ActorState.State.Move)
                    SetState();
            }
        }
    }
}
