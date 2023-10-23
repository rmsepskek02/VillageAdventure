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
        private float _boMoveSpeed = 0;
        private float lastMonsterMoveTime = 0f;
        private float monsterMoveInterval = 2f;
        private bool isMoving = true;
        Vector2 _boMoveDir = Vector2.zero;

        public override void Initialize(BoActor boMonster)
        {
            base.Initialize(boMonster);
            this.boMonster = boMonster as BoMonster;
        }

        public override void Init()
        {
            _objName = gameObject.name;
            objTagName = _objName.Replace("(Clone)", "");
            _boMoveDir = new Vector2(boMonster.moveDirection.x, boMonster.moveDirection.y);
            _boMoveSpeed = boActor.moveSpeed;
        }

        void Update()
        {
            Debug.Log($"===== {isMoving}");
            if (isMoving)
                MoveMonster();
        }
        public override void OnMove()
        {
            base.OnMove();
        }
        // 슬라임 애니메이션 조작
        private void SetState(Vector2 moveDir)
        {
            if (Input.GetKeyUp(KeyCode.A))
                State = ActorState.State.Move;
            if (Input.GetKeyUp(KeyCode.S))
                State = ActorState.State.Attack;
            if (Input.GetKeyUp(KeyCode.D))
                State = ActorState.State.Dead;
            if (moveDir == Vector2.zero)
            {
                State = ActorState.State.Idle;
            }
            if (Input.GetKeyUp(KeyCode.X))
                State = ActorState.State.Hurt;
            if (moveDir != Vector2.zero)
            {
                State = ActorState.State.Move;
                if (moveDir.x < 0)
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
            time += Time.deltaTime;

            if (time - lastMonsterMoveTime >= monsterMoveInterval)
            {
                int randomX = Random.Range(-1, 2);
                int randomY = Random.Range(-1, 2);
                Vector2 ranDir = new Vector2(randomX, randomY);
                boMonster.moveDirection = ranDir;
                _boMoveDir = new Vector2(boMonster.moveDirection.x, boMonster.moveDirection.y);
                lastMonsterMoveTime = time;
                SetState(_boMoveDir);
            }
            // 이동 방향이 유효한지 확인하고, 충돌 시 다시 설정
            if (IsPositionValid(_boMoveDir) != null)
            {
                if (IsPositionValid(_boMoveDir).name.Replace("(Clone)", "") == "Well")
                {
                    Debug.Log("WELL COLLISION");
                    State = ActorState.State.Attack;
                    isMoving = false;
                    return;
                }
                else
                {
                    Vector2 zeroDirection = Vector2.zero;
                    _boMoveDir = (zeroDirection - (Vector2)gameObject.transform.position);
                }
            }
            else if(IsPositionValid(_boMoveDir) == null)
            {
                // 이동 가능한 위치로 이동
                transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + _boMoveDir.normalized * Time.deltaTime * _boMoveSpeed, 1.0f);
            }
        }

        Collider2D IsPositionValid(Vector2 position)
        {
            int layerMask = ~LayerMask.GetMask("Monster");
            // 이동 위치에서 Raycast로 Collider 충돌을 확인
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.2f, position, 1f, layerMask);
            // 필요에 따라 다른 검사를 추가할 수 있음
            Debug.DrawRay((Vector2)transform.position + Vector2.down * 0.2f, position.normalized * 1f, Color.red);
            Debug.Log(hit.collider);

            return hit.collider; // Collider가 없으면 이동 가능한 위치
        }

        public void testFunc()
        {
            Debug.Log("TEST");
            isMoving = true;
            State = ActorState.State.Idle;
        }
    }
}
