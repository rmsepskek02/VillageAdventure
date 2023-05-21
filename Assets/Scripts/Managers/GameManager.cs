using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VillageAdventure.Enum;
using VillageAdventure.Util;
using VillageAdventure.StaticData;
using System;

namespace VillageAdventure
{
    public class GameManager : Singleton<GameManager>
    {
        public SceneType prevStage;
        public SceneType currentScene;

        [SerializeField]
        private StaticDataModule sd = new StaticDataModule();
        public static StaticDataModule SD { get { return Instance.sd; } }

        public void LoadScene(SceneType type, Action complete = null)
        {
            currentScene = type;
            SceneManager.LoadScene((int)type);
        
            // 씬 변경 시작 후 1초 후에 실행시킬 기능이 있다면 실행
            StartCoroutine(WaitForComplete());
        
            IEnumerator WaitForComplete()
            {
                yield return new WaitForSeconds(0.05f);
        
                complete?.Invoke();
            }
            /// 씬이 완벽하게 로드되고 씬변경이 가능하게끔???
        }
    }
}