using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Object;

public class Warrior : Actor
{
    string objName;
    public BoWarrior boWarrior;
    private float time = 0;
    private float lastMonsterMoveTime = 0f;
    private float monsterMoveInterval = 2f;
    public bool isMoving = true;
    public bool isAttack = false;
    private TriggerController trigger;

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
    }

    public override void Init()
    {
        base.Init();
        objName = gameObject.name;
        objTagName = objName.Replace("(Clone)", "");
        trigger = transform.GetComponentInChildren<TriggerController>();
        CheckTrigger();
    }
    public override void OnMove()
    {
        base.OnMove();
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
    private void CheckTrigger()
    {
        trigger.Initialize(OnEnter, OnExit, OnStay);
        void OnEnter(Collider2D collision)
        {
            // 무언가 들어왔다
            SetState(collision);
        }
        void OnExit(Collider2D collision)
        {
            // 무언가 없어졌다
            isMoving = true;
            isAttack = false;
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
                boWarrior.moveDirection.x / 3, gameObject.transform.position.y + boWarrior.moveDirection.y / 3);
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
            if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                isAttack = true;
                isMoving = false;
            }
            else
            {
                isAttack = false;
                isMoving = false;
            }
            SetMoveDir();
        }
        else
        {
            isMoving = true;
            isAttack = false;
            boWarrior.moveDirection = Vector2.zero;
        }
    }
}
