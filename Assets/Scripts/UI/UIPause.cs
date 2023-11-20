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
            leaveYes.onClick.AddListener(OnClickYes);
            leaveNo.onClick.AddListener(OnClickNo);
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
            text.text = "Complete Save !!";
        }
        private void OnClickMenu()
        {
            InGameManager.Instance.UI_inGame.transform.GetChild(5).gameObject.transform.GetChild(4).gameObject.SetActive(true);
            isMenu = true;
        }
        private void OnClickExit()
        {
            InGameManager.Instance.UI_inGame.transform.GetChild(5).gameObject.transform.GetChild(4).gameObject.SetActive(true);
            isExit = true;
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
                InGameManager.Instance.UI_inGame.transform.GetChild(5).gameObject.SetActive(false);
                InGameManager.Instance.UI_inGame.transform.GetChild(5).gameObject.transform.GetChild(4).gameObject.SetActive(false);
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
            InGameManager.Instance.UI_inGame.transform.GetChild(5).gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }

        void Update()
        {

        }
    }
}
