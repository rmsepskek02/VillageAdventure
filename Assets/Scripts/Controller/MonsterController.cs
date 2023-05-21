using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Enum;


/// 삭제
public class MonsterController : ActorController
{
    string _objName;

    void Start()
    {
        _objName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    // 슬라임 애니메이션 조작
    private void SetState()
    {
        if (Input.GetKeyUp(KeyCode.A))
            State = ActorState.State.Dead;
        if (Input.GetKeyUp(KeyCode.S))
            State = ActorState.State.Alert;
        if (Input.GetKeyUp(KeyCode.D))
            State = ActorState.State.Hurt;

        objTagName = _objName;
    }

    public override void Init()
    {

    }
}
