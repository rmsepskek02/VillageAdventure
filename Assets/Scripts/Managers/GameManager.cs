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

        public bool isPause;

        [SerializeField]
        private StaticDataModule sd = new StaticDataModule();
        public static StaticDataModule SD { get { return Instance.sd; } }

        // Scene Load
        public void LoadScene(SceneType type, Action complete = null)
        {
            currentScene = type;
            SceneManager.LoadScene((int)type);
            //SceneManager.LoadScene((int)type, LoadSceneMode.Additive);
            
            // 씬 변경 시작 후 1초 후에 실행시킬 기능이 있다면 실행
            StartCoroutine(WaitForComplete());
        
            IEnumerator WaitForComplete()
            {
                yield return new WaitForSeconds(0.05f);
                complete?.Invoke();
            }
            /// 씬이 완벽하게 로드되고 씬변경이 가능하게끔???
        }
        // Game Pause & Restart
        public void TogglePause()
        {
            if (!isPause)
            {
                isPause = true;
                PauseGame();
            }
            else
            {
                isPause = false;
                ReStartGame();
            }
        }

        // Game Pause
        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        // Game Restart
        public void ReStartGame()
        {
            Time.timeScale = 1f;
        }

        // Scene 에 따라 Object 활성화
        public void SetObject()
        {
            if (currentScene == SceneType.Mine)
            {
                InGameManager.Instance.homeObj.SetActive(false);
                InGameManager.Instance.fieldObj.SetActive(false);
                InGameManager.Instance.mineHolder.SetActive(true);
                InGameManager.Instance.treeHolder.SetActive(false);
                InGameManager.Instance.spawningPool.SetActive(false);
                InGameManager.Instance.normalSlime.SetActive(false);
                InGameManager.Instance.warrior.SetActive(false);
            }
            else if (currentScene == SceneType.Forest)
            {
                InGameManager.Instance.homeObj.SetActive(false);
                InGameManager.Instance.fieldObj.SetActive(false);
                InGameManager.Instance.mineHolder.SetActive(false);
                InGameManager.Instance.treeHolder.SetActive(true);
                InGameManager.Instance.spawningPool.SetActive(false);
                InGameManager.Instance.normalSlime.SetActive(false);
                InGameManager.Instance.warrior.SetActive(false);
            }
            else if (currentScene == SceneType.Field)
            {
                InGameManager.Instance.homeObj.SetActive(false);
                InGameManager.Instance.fieldObj.SetActive(true);
                InGameManager.Instance.mineHolder.SetActive(false);
                InGameManager.Instance.treeHolder.SetActive(false);
                InGameManager.Instance.spawningPool.SetActive(true);
                InGameManager.Instance.normalSlime.SetActive(true);
                InGameManager.Instance.warrior.SetActive(true);
            }
            else if (currentScene == SceneType.House)
            {
                InGameManager.Instance.homeObj.SetActive(true);
                InGameManager.Instance.fieldObj.SetActive(false);
                InGameManager.Instance.mineHolder.SetActive(false);
                InGameManager.Instance.treeHolder.SetActive(false);
                InGameManager.Instance.spawningPool.SetActive(false);
                InGameManager.Instance.normalSlime.SetActive(false);
                InGameManager.Instance.warrior.SetActive(false);
            }
            else if (currentScene == SceneType.FishingZone)
            {
                InGameManager.Instance.homeObj.SetActive(false);
                InGameManager.Instance.fieldObj.SetActive(false);
                InGameManager.Instance.mineHolder.SetActive(false);
                InGameManager.Instance.treeHolder.SetActive(false);
                InGameManager.Instance.spawningPool.SetActive(false);
                InGameManager.Instance.normalSlime.SetActive(false);
                InGameManager.Instance.warrior.SetActive(false);
            }
        }
    }
}