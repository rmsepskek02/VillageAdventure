using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.DB;

namespace VillageAdventure.Object
{
    public class ScoreObject : Obj
    {
        public BoScoreObject boScoreObject;
        protected SpriteRenderer sr;
        protected Collider2D coll;

        protected virtual void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
        }
        public override void Init()
        {

        }
        public virtual void Initialize(BoScoreObject boScoreObject)
        {
            this.boScoreObject = boScoreObject;
            SetStats();
        }
        public virtual void SetStats()
        {
            boScoreObject.hp = boScoreObject.sdObject.hp;
            boScoreObject.score = boScoreObject.sdObject.score;
        }
        private void Update()
        {
            if (boScoreObject.hp < boScoreObject.sdObject.hp)
            {
                sr.color = Color.gray;
            }
            if (boScoreObject.hp <= 0)
            {
                InGameManager.Instance.score -= boScoreObject.score;
                Destroy(gameObject);
            }
        }
    }
}
