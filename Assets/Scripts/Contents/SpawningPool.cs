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
        public float lastMonsterSpawnTime = 0f;
        private float monsterSpawnInterval = 3f;
        public List<int> warriorIndexInLayer;
        GameObject nonePlayer;
        Transform warrior;
        int i = 0;

        void Start()
        {
            inGameManager = InGameManager.Instance;
            nonePlayer = GameObject.Find("NonePlayer");
            warrior = nonePlayer.transform.Find("Warrior").gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            GeneratorMonster();
            IndexWarriorWithLayer(warrior, "Warrior");
            GeneratorWarrior(warriorIndexInLayer);
        }

        public void GeneratorMonster()
        {
            if (i >= 5)
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
        private void GeneratorWarrior(List<int> warriorList)
        {
            if (inGameManager.warriorCount <= 2)
            {
                if (inGameManager.time - lastMonsterSpawnTime >= 3f)
                {
                    var sdWarrior = GameManager.SD.sdNonePlayer.Where(_ => _.index == 7000).SingleOrDefault();
                    var _warrior = Instantiate(Resources.Load<GameObject>(sdWarrior.resourcePath)).GetComponent<Warrior>();
                    _warrior.Initialize(new BoWarrior(sdWarrior));
                    _warrior.transform.position = Vector2.zero;
                    _warrior.transform.SetParent(warrior);
                    Destroy(warrior.GetChild(warriorList[0]).gameObject);
                    _warrior.transform.SetSiblingIndex(warriorList[0]);
                    inGameManager.charactors.Add(_warrior);
                    lastMonsterSpawnTime = inGameManager.time;
                    inGameManager.warriorCount++;
                }
            }
        }
        void IndexWarriorWithLayer(Transform tr, string layerName)
        {
            for (int i = 0; i < tr.childCount; i++)
            {
                Transform child = tr.GetChild(i);
                if (child.gameObject.layer != LayerMask.NameToLayer(layerName))
                {
                    if (!warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                        warriorIndexInLayer.Add(child.GetSiblingIndex());
                }
                else
                {
                    if (warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                        warriorIndexInLayer.Remove(child.GetSiblingIndex());
                }
            }
            warriorIndexInLayer.Sort();
        }
    }

}
