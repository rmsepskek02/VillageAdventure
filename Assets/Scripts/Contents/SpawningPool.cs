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
        public Vector2 warriorSpawnPos;

        private InGameManager inGameManager;
        private float lastMonsterSpawnTime = 0f;
        private float monsterSpawnInterval = 10f;
        private int warriors = 0;
        int i = 0;

        void Start()
        {
            inGameManager = InGameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            GeneratorMonster();
            GeneratorWarrior();
        }

        public void GeneratorMonster()
        {
            if (i >= 1)
            {
                return;
            }
            if (inGameManager.time - lastMonsterSpawnTime >= monsterSpawnInterval)
            {
                GameObject monster = GameObject.Find("Monster");
                Transform normalSlime = monster.transform.Find("SlimeNormal").gameObject.transform;
                var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.index == 2001).SingleOrDefault();
                var testMonster = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
                testMonster.Initialize(new BoMonster(sdMonster));
                inGameManager.monsters.Add(testMonster);
                testMonster.transform.SetParent(normalSlime);
                testMonster.gameObject.transform.position = monsterSpawnPos;
                lastMonsterSpawnTime = inGameManager.time;
                i++;
            }
        }
        private void GeneratorWarrior()
        {
            if(warriors <= 2)
            {
                if (inGameManager.time - lastMonsterSpawnTime >= monsterSpawnInterval)
                {
                    var sdWarrior = GameManager.SD.sdNonePlayer.Where(_ => _.index == 7000).SingleOrDefault();
                    var warrior = Instantiate(Resources.Load<GameObject>(sdWarrior.resourcePath)).GetComponent<Warrior>();
                    warrior.Initialize(new BoWarrior(sdWarrior));
                    warrior.transform.position = Vector2.zero;
                    GameObject nonePlayer = GameObject.Find("NonePlayer");
                    Transform _warrior = nonePlayer.transform.Find("Warrior").gameObject.transform;
                    warrior.transform.SetParent(_warrior);
                    inGameManager.charactors.Add(warrior);
                    lastMonsterSpawnTime = inGameManager.time;
                    warriors++;
                }
            }
        }
    }

}
