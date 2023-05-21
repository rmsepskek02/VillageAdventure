using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VillageAdventure.UI
{
    public class UITitle : MonoBehaviour
    {
        public Button start;
        public Button load;
        public Button option;
        public Button exit;

        public GameObject menuHolder;

        private void Start()
        {
            start.onClick.AddListener(OnClickstart);
            load.onClick.AddListener(OnClickload);
            option.onClick.AddListener(OnClickoption);
            exit.onClick.AddListener(OnClickexit);
        }

        private void OnClickstart()
        {
            GameManager.Instance.LoadScene(Enum.SceneType.House, null);
        }
        private void OnClickload()
        {

        }
        private void OnClickoption()
        {

        }
        private void OnClickexit()
        {

        }
    }
}