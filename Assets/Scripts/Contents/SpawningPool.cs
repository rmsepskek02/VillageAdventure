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
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //GeneratorCharactor();
        }

        public void GeneratorCharactor()
        {
            var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.index == 2000).SingleOrDefault();
            var sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == 0).SingleOrDefault();
            var testMonster = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
            Vector2 randPos;
            Vector2 randDir = Random.insideUnitCircle * Random.Range(0, 10);
            randPos = _spawnPos + randDir;
            testMonster.gameObject.transform.position = randPos;
        }
    }

}
