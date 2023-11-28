using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure;
namespace VillageAdventure.UI
{ 
    public class UIDead : MonoBehaviour
    {
        public Button save;
        public Button menu;
        public Image image;
        public Text text;
        public Text placeholderText;
        public Text inputText;
        public GameObject buttonHolder;
        public GameObject inputHolder;

        void Start()
        {
            save.onClick.AddListener(OnClickSave);
            menu.onClick.AddListener(OnClickMenu);
        }

        private void OnClickSave()
        {
            Debug.Log("SAVE!!");
            if (inputText.text.Length == 0)
            {
                text.text = "Enter Nickname !!";
                return;
            }
            DataManager.Instance.SaveGameData(inputText.text);
            InGameManager.Instance.ResetGame();
            GameManager.Instance.LoadScene(Enum.SceneType.Title, null);
            inputText.text = "";
        }
        private void OnClickMenu()
        {
            GameManager.Instance.ReStartGame();
            InGameManager.Instance.ResetGame();
            GameManager.Instance.LoadScene(Enum.SceneType.Title, null);
        }

        void Update()
        {
        
        }
    }
}
