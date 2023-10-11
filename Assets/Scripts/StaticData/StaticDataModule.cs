using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.StaticData
{
    [Serializable]
    public class StaticDataModule
    {
        public List<SDPlayer> sdPlayers;
        public List<SDMonster> sdMonsters;
        public List<SDHomeObject> sdHomeObjects;
        public List<SDFieldObject> sdFieldObjects;
        public List<SDCommonObject> sdCommonObjects;
        public List<SDResourceObject> sdResourceObjects;
    }
}