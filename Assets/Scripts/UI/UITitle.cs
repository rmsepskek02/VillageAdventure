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
        private string _buttonText;
        private string awsResponse;

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

            files = DataManager.Instance.GetSaveFiles();
            for (var i = 0; i < files.Length; i++)
            {
                CreateButton(i);
            }

            AWSRank awsRank = gameObject.transform.parent.gameObject.AddComponent<AWSRank>();
            awsRank.SetRank("RankLambda", "GET", "", 0, (responseBody) =>
            {
                // 응답을 사용하는 코드를 여기에 작성합니다.
                if (responseBody != null)
                {
                    // 응답이 성공적으로 받아졌을 때 처리하는 내용
                    awsResponse = responseBody;
                    Debug.Log("Response received: " + responseBody);
                    // JSON 문자열을 RankResponse 객체로 역직렬화합니다.
                    RankResponse response = JsonUtility.FromJson<RankResponse>(responseBody);

                    // rank 배열에 접근하여 필요한 정보를 추출합니다.
                    foreach (RankItem item in response.rank)
                    {
                        string name = item.name;
                        int score = item.score;

                        // 추출한 정보를 사용하여 필요한 작업을 수행합니다.
                        CreateButtonFromRank(item);
                    }
                }
                else
                {
                    // 응답이 실패했을 때 처리하는 내용
                    Debug.Log("Failed to receive response.");
                }
            });
        }

        private void CreateButton(int i)
        {
            // 새로운 버튼 오브젝트 생성
            GameObject newButton = Instantiate(buttonPrefab);

            // 버튼의 부모 설정
            newButton.transform.SetParent(loadVertical.gameObject.transform, false);

            // 버튼의 텍스트 설정
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = Path.GetFileName(files[i]).Replace(".json", "");

            // 버튼에 클릭 이벤트 추가
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
                buttonComponent.onClick.AddListener(() => OnClickSaveFiles(buttonText.text));

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();

            // 버튼의 크기를 텍스트에 맞게 조절
            Vector2 textSize = buttonText.preferredWidth != buttonText.rectTransform.rect.width ?
                               new Vector2(buttonText.preferredWidth, rectTransform.sizeDelta.y) :
                               new Vector2(buttonText.rectTransform.rect.width, rectTransform.sizeDelta.y);
            rectTransform.sizeDelta = textSize;
        }

        private void CreateButtonFromRank(RankItem item)
        {
            // 버튼 오브젝트 생성
            GameObject newButton = Instantiate(buttonPrefab);
            // 부모 설정 등 버튼 속성 설정
            newButton.transform.SetParent(rankVertical.gameObject.transform, false);
            newButton.GetComponent<Button>().interactable = false;

            // RankItem의 정보를 사용하여 버튼 텍스트 설정
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = item.name + " - " + item.score;

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            // 버튼의 크기를 텍스트에 맞게 조절
            Vector2 textSize = buttonText.preferredWidth != buttonText.rectTransform.rect.width ?
                               new Vector2(buttonText.preferredWidth, rectTransform.sizeDelta.y) :
                               new Vector2(buttonText.rectTransform.rect.width, rectTransform.sizeDelta.y);
            rectTransform.sizeDelta = textSize;
        }

        // 버튼 클릭 시 실행될 메서드
        void OnClickSaveFiles(string buttonText)
        {
            Debug.Log("Button Clicked: " + buttonText);
            _buttonText = buttonText;
            selectUI.SetActive(true);
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
    }
}