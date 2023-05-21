using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.StaticData;

namespace VillageAdventure.DB
{
    public class BoMonster : BoActor
    {
        public SDMonster sdMonster;

        public BoMonster(SDActor sdActor)
            : base(sdActor)
        {
            sdMonster = sdActor as SDMonster;
        }
    }
}