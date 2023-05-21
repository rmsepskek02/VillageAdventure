using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.StaticData;

namespace VillageAdventure.DB
{
    [Serializable]
    public class BoPlayer : BoActor
    {
        /// sdActor의 형태로 되어 있는 데이터를 접근이 편리하게 실제 캐릭터의 기획
        /// 데이터 형태로 캐스팅하여 담아둘 필드
        public SDPlayer sdPlayer;

        public BoPlayer(SDActor sdActor)
            : base(sdActor)
        {
            sdPlayer = sdActor as SDPlayer;
        }
    }
}