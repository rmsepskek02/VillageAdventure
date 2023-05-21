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
            GeneratorCharactor();
        }

        public void GeneratorCharactor()
        {
            //TEST
            //Debug.Log(GameManager.SD.sdMonsters.Where(_ => _.index == 2000).SingleOrDefault().index);
            //var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.nickname == "SlimeNormal").SingleOrDefault();
            //var sdMonster = GameManager.SD.sdMonsters.Where(_ => _.index == 0).SingleOrDefault();
            //var sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == 0).SingleOrDefault();
            //Debug.Log(sdMonster.resourcePath);
            //Debug.Log(sdMonster);
            //Debug.Log(sdMonster1.resourcePath);
            //Debug.Log(GameManager.SD.sdMonsters);
            //Debug.Log(GameManager.SD.sdHomeObjects.Count);
            //Debug.Log(sdMonster);
            //Debug.Log(sdObject);
            //var testMonster = Instantiate(Resources.Load<GameObject>(sdMonster.resourcePath)).GetComponent<Monster>();
            //var test= Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
            //testMonster.Initialize(new BoMonster(sdMonster));
            //test.Initialize(new BoMonster(sdMonster));
            //playerController.Initialize(testMonster);
            //Vector2 randPos;
            //Vector2 randDir = Random.insideUnitCircle * Random.Range(0, 10);
            //randPos = _spawnPos + randDir;
            //testMonster.gameObject.transform.position = randPos;

            //InGameManager.Instance.charactors.Add(testMonster);
            //Debug.Log(InGameManager.Instance.sdIndex);
            //var sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();
            //var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
            //Vector2 randPos;
            //Vector2 randDir = Random.insideUnitCircle * Random.Range(0, 10);
            //randPos = _spawnPos + randDir;
            //test.transform.position = randPos;
        }
    }

}
