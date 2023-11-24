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
        public GameObject vertical1;
        public GameObject scrollView;

        public Image loadImage;
        public Image loadDataImage;
        public Text loadText;
        public Button loadCancelButton;
        public Text loadX;

        public Image rankImage;
        public Image rankDataImage;
        public Text rankText;
        public Button rankCancelButton;
        public Text rankX;

        public Image optionImage;
        public GameObject optionTextHolder;
        public Text optionVolumeText;
        public Text optionSizeText;
        public Button optionCancelButton;
        public Text optionX;

        public Image helpImage;
        public Text helpText;
        public Button helpCancelButton;
        public Text helpX;
        public GameObject buttonPrefab;
        string[] files;
        private void Start()
        {
            start.onClick.AddListener(OnClickStart);
            load.onClick.AddListener(OnClickLoad);
            rank.onClick.AddListener(OnClickRank);
            option.onClick.AddListener(OnClickOption);
            help.onClick.AddListener(OnClickHelp);
            exit.onClick.AddListener(OnClickExit);
            loadCancelButton.onClick.AddListener(OnClickLoadCancelButton);
            rankCancelButton.onClick.AddListener(OnClickRankCancelButton);
            optionCancelButton.onClick.AddListener(OnClickOptionCancelButton);
            helpCancelButton.onClick.AddListener(OnClickHelpCancelButton);

            files = DataManager.Instance.GetSaveFiles();
            for (var i = 0; i < files.Length; i++)
            {
                CreateButton(i);
            }
        }

        private void CreateButton(int i)
        {
            // ���ο� ��ư ������Ʈ ����
            GameObject newButton = Instantiate(buttonPrefab);

            // ��ư�� �θ� ����
            newButton.transform.SetParent(vertical1.gameObject.transform, false);

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
        // ��ư Ŭ�� �� ����� �޼���
        void OnClickSaveFiles(string buttonText)
        {
            Debug.Log("Button Clicked: " + buttonText);
            DataManager.Instance.LoadGameData(buttonText);
        }
        private void OnClickStart()
        {
            GameManager.Instance.LoadScene(Enum.SceneType.House, null);
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
    }
}