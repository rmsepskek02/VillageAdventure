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

        [SerializeField]
        Vector2 _spawnPos;

        private InGameManager inGameManager;
        private float lastMonsterSpawnTime = 0f;
        private float monsterSpawnInterval = 10f;

        void Start()
        {
            inGameManager = InGameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            GeneratorCharactor();
        }

        public void GeneratorCharactor()
        {
            if (inGameManager.time - lastMonsterSpawnTime >= monsterSpawnInterval)
            {
                GameObject monster = GameObject.Find("Monster");
                Transform normalSlime = monster.transform.Find("SlimeNormal").gameObject.transform;
                var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.index == 2001).SingleOrDefault();
                var sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == 0).SingleOrDefault();
                var testMonster = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
                testMonster.Initialize(new BoMonster(sdMonster));
                //Vector2 randPos;
                //Vector2 randDir = Random.insideUnitCircle * Random.Range(0, 10);
                //randPos = _spawnPos + randDir;
                testMonster.transform.SetParent(normalSlime);
                testMonster.gameObject.transform.position = _spawnPos;
                lastMonsterSpawnTime = inGameManager.time;
            }
        }
    }

}
