using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Enum;

namespace VillageAdventure.Object
{
    public class Monster : Actor
    {
        string _objName;

        public override void Init()
        {
            _objName = gameObject.name;
        }

        void Update()
        {
            SetState();
        }

        // 슬라임 애니메이션 조작
        private void SetState()
        {
            if (Input.GetKeyUp(KeyCode.A))
                State = ActorState.State.Alert;
            if (Input.GetKeyUp(KeyCode.S))
                State = ActorState.State.Attack;
            if (Input.GetKeyUp(KeyCode.D))
                State = ActorState.State.Dead;
            if (Input.GetKeyUp(KeyCode.Z))
                State = ActorState.State.Idle;
            if (Input.GetKeyUp(KeyCode.X))
                State = ActorState.State.Hurt;
            if (Input.GetKeyUp(KeyCode.C))
                State = ActorState.State.Move;

            objTagName = _objName;
        }
    }
}
