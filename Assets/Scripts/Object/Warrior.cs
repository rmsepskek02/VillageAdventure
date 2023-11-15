using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Object;
using UnityEngine.UI;
using VillageAdventure;

public class Warrior : Actor
{
    string objName;
    public BoWarrior boWarrior;
    private float time = 0f;
    private float timeAi = 0f;
    private float lastMonsterMoveTime = 0f;
    private float monsterMoveInterval = 2f;
    public List<GameObject> monsterList = new List<GameObject>();
    public bool isMoving = true;
    public bool isAttack = false;
    public bool isEnterObj= false;
    public bool hasMonster = false;
    public bool isArrive = false;
    private GameObject UI_inGame;
    public Image HpBar;
    private AudioClip clip;
    private TriggerController moveTrigger;
    private TriggerController hitBox;
    private InGameManager inGameManager;
    Vector2[] path;
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
        if (isAttack == true)
            anim.SetBool("isAttack", true);
        else 
            anim.SetBool("isAttack", false);
        if (transform.GetChild(0).position.x > transform.position.x)
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
        inGameManager = InGameManager.Instance;
        objName = gameObject.name;
        objTagName = objName.Replace("(Clone)", "");
        UI_inGame = GameObject.Find("UI_inGame");
        HpBar = UI_inGame.transform.GetChild(0).transform.GetChild(gameObject.transform.GetSiblingIndex()).transform.GetChild(1).GetComponent<Image>();
        moveTrigger = transform.GetChild(0).GetComponent<TriggerController>();
        hitBox = transform.GetChild(1).GetComponent<TriggerController>();
        CheckTrigger();
        HitBoxTrigger();
        AddAudioClip();
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
        SetHp();
    }
    private void AddAudioClip()
    {
        audioClip.Add(Resources.Load<AudioClip>("Sound/Effect/MP_칼 휘두르는 소리 1"));
        audioClip.Add(Resources.Load<AudioClip>("Sound/Effect/MP_남자가 윽 하고 내는 신음"));
    }
        
    private void PlayAudioEffect()
    {
        if (isAttack)
            clip = audioClip[0];
        if (boWarrior.hp <= 0)
            clip = audioClip[1];

        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    // 이동방향 및 Object 감지 Trigger
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
    // Monster Hit Box Trigger
    private void HitBoxTrigger()
    {
        hitBox.Initialize(OnEnter, OnExit, OnStay);
        void OnEnter(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                monsterList.Add(collision.gameObject);
            }
            else
            {
                isMoving = true;
            }
        }
        void OnExit(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                monsterList.Remove(collision.gameObject);
                if(monsterList.Count == 0)
                {
                    isMoving = true;
                }
            }
        }
        void OnStay(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                
            }
        }
    }
    // Warrior's  Hit Damage
    private void HitMonster()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].GetComponent<Monster>().boMonster.hp -= boWarrior.power;
        }
    }
    // Warrior Hp 관련 
    private void SetHp()
    {
        HpBar.fillAmount = Mathf.Clamp01((boWarrior.hp / boActor.sdActor.hp));
        if (boWarrior.hp <= 0)
        {
            inGameManager.warriorCount--;
            GameObject emptyObject = new GameObject($"EmptyObject{gameObject.transform.GetSiblingIndex()}");
            emptyObject.transform.SetParent(gameObject.transform.parent.gameObject.transform);
            emptyObject.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());
            Destroy(gameObject);
            Debug.Log("WARRIOR DIEEEEEE");
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
        ChangeHitBoxPosition();
    }

    // child Object postion 변경
    private void ChangeHitBoxPosition()
    {
        // moveTrigger position 변경
        gameObject.transform.GetChild(0).transform.position = new Vector2(gameObject.transform.position.x +
                boWarrior.moveDirection.x / 3, gameObject.transform.position.y + boWarrior.moveDirection.y / 3);

        // hitBox position 변경
        if (boWarrior.moveDirection.x == 1)
            gameObject.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 270f);
        else if (boWarrior.moveDirection.x == -1)
            gameObject.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 90f);
        else if (boWarrior.moveDirection.y == 1)
            gameObject.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0f);
        else if (boWarrior.moveDirection.y == -1)
            gameObject.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 180f);
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

    #region PathFinding
    public void OnPathFound(Vector2[] newPath, bool pathSuccessful, bool hasMonster)
    {
        if (pathSuccessful && hasMonster)
        {
            path = newPath;
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
                ChangeHitBoxPosition();
                timeAi = 0f;
                //transform.position = Vector2.MoveTowards((Vector2)transform.position, currentWaypoint, Time.deltaTime);
            }

            yield return null;

        }
    }

    //public void OnDrawGizmos()
    //{
    //    if (path != null)
    //    {
    //        for (int i = targetIndex; i < path.Length; i++)
    //        {
    //            Gizmos.color = Color.black;
    //            Gizmos.DrawCube(path[i], Vector2.one);
    //
    //            if (i == targetIndex)
    //            {
    //                Gizmos.DrawLine(transform.position, path[i]);
    //            }
    //            else
    //            {
    //                Gizmos.DrawLine(path[i - 1], path[i]);
    //            }
    //        }
    //    }
    //}
    #endregion
}
