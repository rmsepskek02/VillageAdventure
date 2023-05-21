using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.Enum
{
    public enum SceneType
    {
        Title, House, Field, Mine, FishingZone, Forest
    }
    public class ActorState
    {
        public enum State { Idle, Move, Attack, Hit, Dead, Hurt, Alert }
    }

/*    public static class Inventory
    {
        public const int mine = 0;
        public const int tree = 0;
        public const int fish = 0;
        public const int food = 0;
    }*/
}