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
        public float moveSpeed; 
        public Vector2 moveDirection;
        public int hp;
        public int power;
        /// SD 데이터
        public SDActor sdActor; 

        public BoActor(SDActor sdActor)
        {
            this.sdActor = sdActor;
        }
    }
}