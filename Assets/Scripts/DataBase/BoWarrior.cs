using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.StaticData;

namespace VillageAdventure.DB
{
    [Serializable]
    public class BoWarrior: BoActor
    {
        public SDNonePlayer sdWarrior;

        public BoWarrior(SDActor sdActor)
            : base(sdActor)
        {
            sdWarrior = sdActor as SDNonePlayer;
        }
    }
}