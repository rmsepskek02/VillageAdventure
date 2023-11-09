using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.StaticData;
using System;

namespace VillageAdventure.DB
{
    [Serializable]
    public class BoScoreObject
    {
        public float hp;
        public int score;

        public SDObject sdObject;
        public BoScoreObject(SDObject sdObject)
        {
            this.sdObject = sdObject;
        }
    }
}