using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure;
namespace VillageAdventure.UI
{
    public class UIPause: MonoBehaviour
    {
        public Button save;
        public Button menu;
        public Button exit;
        public Button back;
        public Button cancelButton;
        public Image image;
        public Text text;
        public Text placeholderText;
        public Text inputText;
        public GameObject buttonHolder;
        public GameObject inputHolder;
        public GameObject leaveUI;
        public GameObject leaveButtonHolder;
        public Button leaveYes;
        public Button leaveNo;
        public Image leaveImage;
        public Text leaveText;

        private bool isMenu;
        private bool isExit;


        void Start()
        {
            save.onClick.AddListener(OnClickSave);
            menu.onClick.AddListener(OnClickMenu);
            exit.onClick.AddListener(OnClickExit);
            back.onClick.AddListener(OnClickBack);
            leaveYes.onClick.AddListener(OnClickYes);
            leaveNo.onClick.AddListener(OnClickNo);
            cancelButton.onClick.AddListener(OnClickCancel);
        }

        private void OnClickSave()
        {
            Debug.Log("SAVE!!");
            if (inputText.text.Length == 0)
            {
                text.text = "Enter Filename !!";
                return;
            }
            // µ•¿Ã≈Õ save
            DataManager.Instance.SaveGameData(inputText.text);
            text.text = "Complete Save !!";
            inputText.text = "";
        }
        private void OnClickMenu()
        {
            leaveUI.SetActive(true);
            isMenu = true;
        }
        private void OnClickExit()
        {
            leaveUI.SetActive(true);
            isExit = true;
        }
        private void OnClickBack()
        {
            GameManager.Instance.TogglePause();
            Debug.Log($"BACK = {gameObject.name}");
        }
        private void OnClickCancel()
        {
            Debug.Log($"BACK = {gameObject.name}");
            GameManager.Instance.TogglePause();
        }
        private void OnClickYes()
        {
            if (isMenu)
            {
                isMenu = false;
                GameManager.Instance.isPause = false;
                GameManager.Instance.LoadScene(Enum.SceneType.Title, null);
                GameManager.Instance.ReStartGame();
                InGameManager.Instance.ResetGame();
                gameObject.SetActive(false);
                leaveUI.SetActive(false);
            }
            else if (isExit)
            {
                Application.Quit();
            }
        }
        private void OnClickNo()
        {
            isMenu = false;
            isExit = false;
            GameManager.Instance.isPause = false;
            leaveUI.SetActive(false);
        }

        void Update()
        {

        }
    }
}
