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
        public float stayTime = 0;
        private float lastMonsterMoveTime = 0f;
        private float monsterMoveInterval = 2f;
        public List<GameObject> enemyList = new List<GameObject>();
        public bool isMoving = true;
        public bool isAttack = false;
        private TriggerController trigger;
        private TriggerController hitBox;
        private AudioClip clip;

        public override void Initialize(BoActor boMonster)
        {
            base.Initialize(boMonster);
            this.boMonster = boMonster as BoMonster;
        }

        public override void Init()
        {
            objName = gameObject.name;
            objTagName = objName.Replace("(Clone)", "");
            trigger = transform.GetChild(0).GetComponent<TriggerController>();
            hitBox = transform.GetChild(1).GetComponent<TriggerController>();
            CheckTrigger();
            HitBoxTrigger();
            AddAudioClip();
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
            if (boMonster.hp < 0)
            {
                Debug.Log("MONSETER DIEEEEEE");
                State = MonsterState.State.Dead;
            }
        }
        public override void OnMove()
        {
            base.OnMove();
        }
        public override void OnMoveAnim() { }

        public void DeadMonster()
        {
            Debug.Log("ANIM DEAD");
            Destroy(gameObject);
        }
        private void AddAudioClip()
        {
            audioClip.Add(Resources.Load<AudioClip>("Sound/Effect/MP_슬라임 공격"));
            audioClip.Add(Resources.Load<AudioClip>("Sound/Effect/MP_슬라임 죽음"));
        }
        private void PlayAudioEffect()
        {
            if (isAttack)
                clip = audioClip[0];
            if (boMonster.hp <= 0)
                clip = audioClip[1];

            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        private void SetState(Collider2D collision = null)
        {
            if(isMoving)
                State = MonsterState.State.Move;
            if(!isMoving && !isAttack)
                State = MonsterState.State.Idle;
            // 충돌체가 있는 경우
            if (collision != null)
            {
                // 공격하고 있지 않은 경우
                if (!isAttack)
                {
                    // 공격해야할 대상인 경우
                    if (collision.gameObject.layer == LayerMask.NameToLayer("BuildObject")
                        || collision.gameObject.layer == LayerMask.NameToLayer("Warrior"))
                    {
                        isAttack = true;
                        isMoving = false;
                        State = MonsterState.State.Alert;
                        boMonster.moveDirection = Vector2.zero;
                        rigid.velocity = new Vector2(0f, 0f);
                        
                    }
                    // 공격할 대상이 아닌 경우
                    else
                    {
                        // 맵이거나 Trigger obj 인 경우
                        if (collision.gameObject.layer == LayerMask.NameToLayer("Tilemap")
                            || collision.gameObject.layer == LayerMask.NameToLayer("Trigger"))
                        {
                            isMoving = true;
                            SetMoveDir();
                        }
                        // 맵이 아닌 경우
                        else
                        {
                            isMoving = false;
                            boMonster.moveDirection = Vector2.zero;
                            rigid.velocity = new Vector2(0f, 0f);
                            State = MonsterState.State.Idle;
                        }
                    }
                }
                // 공격하고 있는 경우
                else if (isAttack){ }
            }
            // 충돌체가 없는 경우
            else if (collision == null) { }
        }

        private void SetFlipX()
        {
            if (boMonster.moveDirection != Vector2.zero)
            {
                if (boMonster.moveDirection.x < 0)
                    sr.flipX = true;
                else
                    sr.flipX = false;
            }
        }
        private void CheckTrigger()
        {
            trigger.Initialize(OnEnter, OnExit, OnStay);
            void OnEnter(Collider2D collision)
            {
                // 무언가 들어왔다
                if (collision.gameObject.layer != LayerMask.NameToLayer("Trigger"))
                {
                    isMoving = false;
                    SetState(collision);
                }
            }
            void OnExit(Collider2D collision)
            {
                // 무언가 없어졌다
                // 공격하고 있지 않은 경우
                if (!isAttack)
                {
                    State = MonsterState.State.Move;
                    isMoving = true;
                }
                // 공격하고 있는 경우
                else if (isAttack)
                {
                    if (collision.gameObject.layer == LayerMask.NameToLayer("Warrior")
                        || collision.gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                    {
                        isAttack = false;
                        State = MonsterState.State.Idle;
                        isMoving = true;
                        stayTime = 0;
                    }
                    else if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
                    {
                        
                    }
                }
            }
            
            void OnStay(Collider2D collision)
            {
                // 무언가 계속있다
                stayTime += Time.deltaTime;
                if (stayTime >= 0.1f)
                {
                    if (!isAttack)
                    {
                        if (collision.gameObject.layer == LayerMask.NameToLayer("Tilemap")
                            || collision.gameObject.layer == LayerMask.NameToLayer("Player")
                            || collision.gameObject.layer == LayerMask.NameToLayer("Monster")
                            || collision.gameObject.layer == LayerMask.NameToLayer("Trigger"))
                        {
                            isMoving = true;
                            SetMoveDir();
                            stayTime = 0;
                        }
                    }
                }
            }
        }

        private void HitBoxTrigger()
        {
            hitBox.Initialize(OnEnter, OnExit, OnStay);
            void OnEnter(Collider2D collision)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Warrior")
                    || collision.gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                {
                    enemyList.Add(collision.gameObject);
                }
                SetState(collision);
            }
            void OnExit(Collider2D collision)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Warrior")
                    || collision.gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                {
                    Debug.Log("EXIT Warrior");
                    enemyList.Remove(collision.gameObject);
                    if (enemyList.Count == 0)
                    {
                        isAttack = false;
                        State = MonsterState.State.Idle;
                        isMoving = true;
                        stayTime = 0;
                    }
                }
            }
            void OnStay(Collider2D collision)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Warrior"))
                {

                }
            }
        }
        // Monster's Hit Damage
        private void HitObject()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if(enemyList[i].gameObject.layer == LayerMask.NameToLayer("Warrior"))
                    enemyList[i].GetComponent<Warrior>().boWarrior.hp -= boMonster.power;
                else if (enemyList[i].gameObject.layer == LayerMask.NameToLayer("BuildObject"))
                    enemyList[i].GetComponent<ScoreObject>().boScoreObject.hp -= boMonster.power;
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
                boMonster.moveDirection.x/5, gameObject.transform.position.y + boMonster.moveDirection.y/5 - 0.3f);

            if (State == MonsterState.State.Move && ranDir == Vector2.zero)
                State = MonsterState.State.Idle;
            //if (State == MonsterState.State.Idle || State == MonsterState.State.Move)
            SetState();
        }
    }
}
