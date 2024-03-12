using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VillageAdventure.UI
{
    public class UITitle : MonoBehaviour
    {
        public Button start;
        public Button load;
        public Button rank;
        public Button option;
        public Button help;
        public Button exit;

        public GameObject menuHolder;
        public GameObject loadHolder;
        public GameObject rankHolder;
        public GameObject optionHolder;
        public GameObject helpHolder;

        public Image loadImage;
        public Image loadDataImage;
        public Text loadText;
        public Button loadCancelButton;
        public Text loadX;
        public GameObject loadScrollView;
        public GameObject loadVertical;
        public GameObject selectUI;
        public Image selectImage;
        public Button selectCancelButton;
        public Text selectText;
        public GameObject selectButtonHolder;
        public Button selectLoad;
        public Button selectDelete;

        public Image rankImage;
        public Image rankDataImage;
        public Text rankText;
        public GameObject rankScrollView;
        public GameObject rankVertical;
        public Button rankCancelButton;
        public Text rankX;

        public Image optionImage;
        public GameObject optionTextHolder;
        public GameObject optionVolumeHolder;
        public Text optionVolumeText;
        public GameObject optionToggleHolder;
        public Toggle optionOnToggle;
        public Toggle optionOffToggle;
        public Slider optionSlider;
        public Text optionSizeText;
        public Button optionCancelButton;
        public Text optionX;

        public Image helpImage;
        public Text helpText;
        public Button helpCancelButton;
        public Text helpX;
        public GameObject buttonPrefab;
        string[] files;
        private string _buttonText;
        private string awsResponse;
        private bool isMute;

        [System.Serializable]
        public class RankItem
        {
            public string name;
            public int score;
        }

        [System.Serializable]
        public class RankResponse
        {
            public RankItem[] rank;
        }

        private void Start()
        {
            start.onClick.AddListener(OnClickStart);
            load.onClick.AddListener(OnClickLoad);
            rank.onClick.AddListener(OnClickRank);
            option.onClick.AddListener(OnClickOption);
            help.onClick.AddListener(OnClickHelp);
            exit.onClick.AddListener(OnClickExit);
            loadCancelButton.onClick.AddListener(OnClickLoadCancelButton);
            selectCancelButton.onClick.AddListener(OnClickSelectCancelButton);
            rankCancelButton.onClick.AddListener(OnClickRankCancelButton);
            optionCancelButton.onClick.AddListener(OnClickOptionCancelButton);
            helpCancelButton.onClick.AddListener(OnClickHelpCancelButton);
            selectLoad.onClick.AddListener(OnClickLoadSelectLoad);
            selectDelete.onClick.AddListener(OnClickLoadSelectDelete);
            optionSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // �ʱ� ���� ����
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


            files = DataManager.Instance.GetSaveFiles();
            for (var i = 0; i < files.Length; i++)
            {
                CreateButton(i);
            }

            AWSRank awsRank = gameObject.transform.parent.gameObject.AddComponent<AWSRank>();
            awsRank.SetRank("RankLambda", "GET", "", 0, (responseBody) =>
            {
                // ������ ����ϴ� �ڵ带 ���⿡ �ۼ��մϴ�.
                if (responseBody != null)
                {
                    // ������ ���������� �޾����� �� ó���ϴ� ����
                    awsResponse = responseBody;
                    //Debug.Log("Response received: " + responseBody);
                    // JSON ���ڿ��� RankResponse ��ü�� ������ȭ�մϴ�.
                    RankResponse response = JsonUtility.FromJson<RankResponse>(responseBody);

                    // rank �迭�� �����Ͽ� �ʿ��� ������ �����մϴ�.
                    foreach (RankItem item in response.rank)
                    {
                        string name = item.name;
                        int score = item.score;

                        // ������ ������ ����Ͽ� �ʿ��� �۾��� �����մϴ�.
                        CreateButtonFromRank(item);
                    }
                }
                else
                {
                    // ������ �������� �� ó���ϴ� ����
                    Debug.Log("Failed to receive response.");
                }
            });
        }

        private void CreateButton(int i)
        {
            // ���ο� ��ư ������Ʈ ����
            GameObject newButton = Instantiate(buttonPrefab);

            // ��ư�� �θ� ����
            newButton.transform.SetParent(loadVertical.gameObject.transform, false);

            // ��ư�� �ؽ�Ʈ ����
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = Path.GetFileName(files[i]).Replace(".json", "");

            // ��ư�� Ŭ�� �̺�Ʈ �߰�
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
                buttonComponent.onClick.AddListener(() => OnClickSaveFiles(buttonText.text));

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();

            // ��ư�� ũ�⸦ �ؽ�Ʈ�� �°� ����
            Vector2 textSize = buttonText.preferredWidth != buttonText.rectTransform.rect.width ?
                               new Vector2(buttonText.preferredWidth, rectTransform.sizeDelta.y) :
                               new Vector2(buttonText.rectTransform.rect.width, rectTransform.sizeDelta.y);
            rectTransform.sizeDelta = textSize;
        }

        private void CreateButtonFromRank(RankItem item)
        {
            // ��ư ������Ʈ ����
            GameObject newButton = Instantiate(buttonPrefab);
            // �θ� ���� �� ��ư �Ӽ� ����
            newButton.transform.SetParent(rankVertical.gameObject.transform, false);
            newButton.GetComponent<Button>().interactable = false;

            // RankItem�� ������ ����Ͽ� ��ư �ؽ�Ʈ ����
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = item.name + " - " + item.score;

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            // ��ư�� ũ�⸦ �ؽ�Ʈ�� �°� ����
            Vector2 textSize = buttonText.preferredWidth != buttonText.rectTransform.rect.width ?
                               new Vector2(buttonText.preferredWidth, rectTransform.sizeDelta.y) :
                               new Vector2(buttonText.rectTransform.rect.width, rectTransform.sizeDelta.y);
            rectTransform.sizeDelta = textSize;
        }

        // ��ư Ŭ�� �� ����� �޼���
        void OnClickSaveFiles(string buttonText)
        {
            Debug.Log("Button Clicked: " + buttonText);
            _buttonText = buttonText;
            selectUI.SetActive(true);
        }
        private void OnClickStart()
        {
            GameManager.Instance.LoadScene(Enum.SceneType.House, null);
            SoundManager.instance.PlayBGM(1);
            DataManager.Instance.InitData();
            //InGameManager.Instance.ResetGame();
        }
        private void OnClickLoad()
        {
            loadHolder.gameObject.SetActive(true);
        }
        private void OnClickRank()
        {
            rankHolder.gameObject.SetActive(true);
        }
        private void OnClickOption()
        {
            optionHolder.gameObject.SetActive(true);
        }
        private void OnClickHelp()
        {
            helpHolder.gameObject.SetActive(true);
        }
        private void OnClickExit()
        {
            Application.Quit();
        }
        private void OnClickLoadCancelButton()
        {
            loadCancelButton.transform.parent.gameObject.SetActive(false);
        }
        private void OnClickSelectCancelButton()
        {
            selectCancelButton.transform.parent.gameObject.SetActive(false);
        }
        private void OnClickLoadSelectLoad()
        {
            DataManager.Instance.LoadGameData(_buttonText);
        }
        private void OnClickLoadSelectDelete()
        {
            DataManager.Instance.DeleteSaveFile(_buttonText);
            selectCancelButton.transform.parent.gameObject.SetActive(false);
            Transform parentTransform = loadScrollView.transform.GetChild(0).transform.GetChild(0).transform;
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
            files = null;
            files = DataManager.Instance.GetSaveFiles();
            for (var i = 0; i < files.Length; i++)
            {
                CreateButton(i);
            }
        }
        private void OnClickRankCancelButton()
        {
            rankCancelButton.transform.parent.gameObject.SetActive(false);
        }
        private void OnClickOptionCancelButton()
        {
            optionCancelButton.transform.parent.gameObject.SetActive(false);
        }
        private void OnClickHelpCancelButton()
        {
            helpCancelButton.transform.parent.gameObject.SetActive(false);
        }

        // ���� ���� �̺�Ʈ �ڵ鷯
        public void OnVolumeChanged(float volume)
        {
            SoundManager.instance.SetVolume(volume);
            PlayerPrefs.SetFloat("Volume", volume); // ����ڰ� ������ ������ ����
            optionOnToggle.isOn = true;
            optionOffToggle.isOn = false;
            optionOnToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            optionOffToggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            optionSlider.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            isMute = false;
            SoundManager.instance.SetVolume(optionSlider.value);
        }

        // Toggle�� ���°� ����� �� ȣ��Ǵ� �޼���
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
        // Toggle�� ���°� ����� �� ȣ��Ǵ� �޼���
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
    }
}