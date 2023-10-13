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
            //SceneManager.LoadScene((int)type, LoadSceneMode.Additive);
            
            // �� ���� ���� �� 1�� �Ŀ� �����ų ����� �ִٸ� ����
            StartCoroutine(WaitForComplete());
        
            IEnumerator WaitForComplete()
            {
                yield return new WaitForSeconds(0.05f);
                complete?.Invoke();
            }
            /// ���� �Ϻ��ϰ� �ε�ǰ� �������� �����ϰԲ�???
        }
    }
}