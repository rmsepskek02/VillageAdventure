using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.StaticData;

namespace VillageAdventure.DB
{
    [Serializable]
    public class BoActor 
    {
        /// 인게임 데이터
        public float moveSpeed; // 인게임 스피드
        public Vector2 moveDirection;
        public SDActor sdActor; // 원래 스피드

        public BoActor(SDActor sdActor)
        {
            this.sdActor = sdActor;
        }
    }
}