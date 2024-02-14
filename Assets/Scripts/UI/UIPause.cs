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
        public Button option;
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

        public GameObject optionHolder;
        public Button optionCancelButton;
        public GameObject optionVolumeHolder;
        public Text optionVolumeText;
        public GameObject optionToggleHolder;
        public Toggle optionOnToggle;
        public Toggle optionOffToggle;
        public Slider optionSlider;

        private bool isMenu;
        private bool isExit;
        private bool isMute;

        void Start()
        {
            save.onClick.AddListener(OnClickSave);
            menu.onClick.AddListener(OnClickMenu);
            option.onClick.AddListener(OnClickOption);
            exit.onClick.AddListener(OnClickExit);
            back.onClick.AddListener(OnClickBack);
            leaveYes.onClick.AddListener(OnClickYes);
            leaveNo.onClick.AddListener(OnClickNo);
            cancelButton.onClick.AddListener(OnClickCancel);
            optionCancelButton.onClick.AddListener(OnClickOptionCancelButton);
            optionSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // 초기 볼륨 설정
            optionSlider.onValueChanged.AddListener(OnVolumeChanged);
            optionOnToggle.onValueChanged.AddListener(OnToggleValueChanged);
            optionOffToggle.onValueChanged.AddListener(OffToggleValueChanged);
            if (PlayerPrefs.GetInt("IsMute") == 1)
            {
                optionOnToggle.isOn = true;
                optionOffToggle.isOn = false;
                optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            }
            else if (PlayerPrefs.GetInt("IsMute") == 0)
            {
                optionOnToggle.isOn = false;
                optionOffToggle.isOn = true;
                optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
        }

        private void OnClickSave()
        {
            Debug.Log("SAVE!!");
            if (inputText.text.Length == 0)
            {
                text.text = "Enter Filename !!";
                return;
            }
            // 데이터 save
            DataManager.Instance.SaveGameData(inputText.text);
            text.text = "Complete Save !!";
            inputText.text = "";
        }
        private void OnClickMenu()
        {
            leaveUI.SetActive(true);
            isMenu = true;
        }
        private void OnClickOption()
        {
            optionHolder.gameObject.SetActive(true);
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

        // 볼륨 조절 이벤트 핸들러
        public void OnVolumeChanged(float volume)
        {
            SoundManager.instance.SetVolume(volume);
            PlayerPrefs.SetFloat("Volume", volume); // 사용자가 설정한 볼륨을 저장
            optionOnToggle.isOn = true;
            optionOffToggle.isOn = false;
            optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            isMute = false;
            SoundManager.instance.SetVolume(optionSlider.value);
        }

        // Toggle의 상태가 변경될 때 호출되는 메서드
        void OnToggleValueChanged(bool isOn)
        {
            if (isOn)
            {
                optionOffToggle.isOn = false;
                optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
                optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                isMute = false;
                SoundManager.instance.SetVolume(optionSlider.value);
            }
            else
            {
                optionOffToggle.isOn = true;
                optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                isMute = true;
                SoundManager.instance.SetVolume(0);
            }
            PlayerPrefs.SetInt("IsMute", isMute ? 0 : 1);
        }
        // Toggle의 상태가 변경될 때 호출되는 메서드
        void OffToggleValueChanged(bool isOff)
        {
            if (isOff)
            {
                optionOnToggle.isOn = false;
                optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                isMute = true;
                SoundManager.instance.SetVolume(0);
            }
            else
            {
                optionOnToggle.isOn = true;
                optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
                optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                isMute = false;
                SoundManager.instance.SetVolume(optionSlider.value);
            }
            PlayerPrefs.SetInt("IsMute", isMute ? 0 : 1);
        }
        private void OnClickOptionCancelButton()
        {
            optionCancelButton.transform.parent.gameObject.SetActive(false);
        }
        void Update()
        {

        }
    }
}
