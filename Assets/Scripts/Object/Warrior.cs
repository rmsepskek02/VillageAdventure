using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Object;

public class Warrior : Actor
{
    string objName;
    public BoWarrior boWarrior;
    private float time = 0f;
    private float timeAi = 0f;
    private float lastMonsterMoveTime = 0f;
    private float monsterMoveInterval = 2f;
    public bool isMoving = true;
    public bool isAttack = false;
    public bool isEnterObj= false;
    public bool hasMonster = false;
    public bool isArrive = false;
    private TriggerController moveTrigger;
    private TriggerController monsterTrigger;
    Vector2[] path;
    int targetIndex;
    private Transform target;

    public override void Initialize(BoActor boWarrior)
    {
        base.Initialize(boWarrior);
        this.boWarrior = boWarrior as BoWarrior;
    }

    public override void Execute()
    {
        base.Execute();
    }
    public override void OnMoveAnim()
    {
        base.OnMoveAnim();
        if(isAttack)
            anim.SetBool("isAttack", isAttack);
        if(transform.GetChild(0).position.x > transform.position.x)
            anim.SetInteger("attackState", 2);
        else if(transform.GetChild(0).position.x < transform.position.x)
            anim.SetInteger("attackState", 3);
        else if (transform.GetChild(0).position.y > transform.position.y)
            anim.SetInteger("attackState", 1);
        else if (transform.GetChild(0).position.y < transform.position.y)
            anim.SetInteger("attackState", 0);
    }

    public override void Init()
    {
        base.Init();
        objName = gameObject.name;
        objTagName = objName.Replace("(Clone)", "");
        moveTrigger = transform.GetChild(0).GetComponent<TriggerController>();
        CheckTrigger();
    }
    public override void OnMove()
    {
        base.OnMove();
    }
    private void Update()
    {
        hasMonster = GameObject.FindGameObjectWithTag("Monster");
        time += Time.deltaTime;
        if (time - lastMonsterMoveTime >= monsterMoveInterval)
        {
            if (!isAttack && isMoving)
            {
                if (!hasMonster)
                    SetMoveDir();
                else
                {
                    MoveToMonster();
                }
            }
            lastMonsterMoveTime = time;
        }
    }
    private void CheckTrigger()
    {
        moveTrigger.Initialize(OnEnter, OnExit, OnStay);
        void OnEnter(Collider2D collision)
        {
            // 무언가 들어왔다
            if(hasMonster)
            {
                if (!collision.CompareTag("Monster"))
                {
                    //SetState(collision);
                    isEnterObj = true;
                }
                if (collision.CompareTag("Monster"))
                {
                    isArrive = true;
                    isAttack = true;
                    isMoving = false;
                    boWarrior.moveDirection.x = 0;
                    boWarrior.moveDirection.y = 0;
                    //OnMoveAnim();
                    StopCoroutine("FollowPath");
                }
            }
            else
            {
                isEnterObj = true;
                SetState(collision);
            }
        }
        void OnExit(Collider2D collision)
        {
            // 무언가 없어졌다
            isMoving = true;
            isAttack = false;
            isEnterObj = false;
            isArrive = false;
        }
        void OnStay(Collider2D collision)
        {
            // 무언가 계속있다
        }
    }


    // 무작위 이동 Direction 설정 함수
    private void SetMoveDir()
    {
        Vector2 preRandom = boWarrior.moveDirection;
        boWarrior.moveDirection = Vector2.zero;
        int random = Random.Range(-1, 2);
        string[] array = { "x", "y"};
        int randomIndex = Random.Range(0, array.Length);
        string randomElement = array[randomIndex];

        if (randomElement == "x")
            boWarrior.moveDirection.x = random;
        else if (randomElement == "y")
            boWarrior.moveDirection.y = random;

        if (preRandom == boWarrior.moveDirection)
            SetMoveDir();

        gameObject.transform.GetChild(0).transform.position = new Vector2(gameObject.transform.position.x +
                boWarrior.moveDirection.x / 2, gameObject.transform.position.y + boWarrior.moveDirection.y / 2);
    }
    private void MoveToMonster()
    {
        target = GameObject.FindGameObjectWithTag("Monster").transform;
        PathRequestManager.RequestPath(transform.position, (Vector2)target.transform.position, OnPathFound);
    }

    private void SetState(Collider2D collision = null)
    {
        if (collision != null)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Default")
              || collision.gameObject.layer == LayerMask.NameToLayer("Warrior"))
            {
                return;
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                isAttack = true;
                isMoving = false;
            }
            else
            {
                isAttack = false;
                isMoving = false;
                SetMoveDir();
            }
        }
        else
        {
            isMoving = true;
            isAttack = false;
            boWarrior.moveDirection = Vector2.zero;
        }
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    public void OnPathFound(Vector2[] newPath, bool pathSuccessful, bool hasMonster)
    {
        if (pathSuccessful && hasMonster)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    
    IEnumerator FollowPath()
    {
        if (!hasMonster)
        {
            yield return null;
        }
        Vector2 currentWaypoint = path[0];
        while (hasMonster)
        {
            if (isArrive)
            {
                Debug.Log($"isArrive = {isArrive}");
                //targetIndex++;
                //if (targetIndex >= path.Length)
                //{
                //}
                //currentWaypoint = path[targetIndex];
                //yield break;
            }
            Vector2 moveDirAi = new Vector2((target.transform.position.x - transform.position.x), 
                (target.transform.position.y - transform.position.y)).normalized;

            timeAi += Time.deltaTime;
            if(timeAi >= 1.0f)
            {
                if (isEnterObj)
                {
                    if (boWarrior.moveDirection.x != 0)
                    {
                        boWarrior.moveDirection.x = 0;
                        if (moveDirAi.y > 0)
                            boWarrior.moveDirection.y = 1;
                        else if (moveDirAi.y < 0)
                            boWarrior.moveDirection.y = -1;
                    }
                    else if (boWarrior.moveDirection.y != 0)
                    {
                        boWarrior.moveDirection.y = 0;
                        if (moveDirAi.x > 0)
                            boWarrior.moveDirection.x = 1;
                        else if (moveDirAi.x < 0)
                            boWarrior.moveDirection.x = -1;
                    }
                }
                else
                {
                    if (Mathf.Abs(moveDirAi.x) >= Mathf.Abs(moveDirAi.y))
                    {
                        boWarrior.moveDirection.y = 0;
                        if (moveDirAi.x > 0)
                            boWarrior.moveDirection.x = 1;
                        else if (moveDirAi.x < 0)
                            boWarrior.moveDirection.x = -1;
                    }
                    else if (Mathf.Abs(moveDirAi.x) < Mathf.Abs(moveDirAi.y))
                    {
                        boWarrior.moveDirection.x = 0;
                        if (moveDirAi.y > 0)
                            boWarrior.moveDirection.y = 1;
                        else if (moveDirAi.y < 0)
                            boWarrior.moveDirection.y = -1;
                    }
                }
                gameObject.transform.GetChild(0).transform.position = new Vector2(gameObject.transform.position.x +
                    boWarrior.moveDirection.x / 3, gameObject.transform.position.y + boWarrior.moveDirection.y / 3);
                timeAi = 0f;
                //transform.position = Vector2.MoveTowards((Vector2)transform.position, currentWaypoint, Time.deltaTime);
            }

            yield return null;

        }
    }
}
