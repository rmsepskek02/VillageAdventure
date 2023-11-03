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
        /// �ΰ��� ������
        public float moveSpeed; 
        public Vector2 moveDirection;
        public int hp;
        public int power;
        /// SD ������
        public SDActor sdActor; 

        public BoActor(SDActor sdActor)
        {
            this.sdActor = sdActor;
        }
    }
}