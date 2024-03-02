using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.Object;

namespace VillageAdventure
{
    public class SpawningPool : MonoBehaviour
    {
        public Vector2 monsterSpawnPos;

        private InGameManager igm;
        public float lastMonsterSpawnTime = 0f;
        private float monsterSpawnInterval = 10f;
        int i = 0;

        void Start()
        {
            igm = InGameManager.Instance;
        }

        void Update()
        {
            GeneratorMonster();
        }

        public void GeneratorMonster()
        {
            if (i >= 5)
            {
                return;
            }
            if (igm.time - lastMonsterSpawnTime >= monsterSpawnInterval)
            {
                GameObject monster = GameObject.Find("Monster");
                Transform normalSlime = monster.transform.Find("SlimeNormal").gameObject.transform;
                var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.index == 2001).SingleOrDefault();
                var testMonster = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
                testMonster.Initialize(new BoMonster(sdMonster));
                igm.monsters.Add(testMonster);
                testMonster.transform.SetParent(normalSlime);
                testMonster.gameObject.transform.position = monsterSpawnPos;
                lastMonsterSpawnTime = igm.time;
                i++;
                igm.SetGuideUI("Warning!! Monster appears", true);
            }
        }
    }
}
