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
        private string awsResponse;

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
            PutRank();
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
        private void PutRank()
        {
            AWSRank awsRank = gameObject.transform.parent.gameObject.AddComponent<AWSRank>();
            awsRank.SetRank("RankLambda", "PUT", inputText.text, InGameManager.Instance.score, (responseBody) =>
            {
                // 응답을 사용하는 코드를 여기에 작성합니다.
                if (responseBody != null)
                {
                    // 응답이 성공적으로 받아졌을 때 처리하는 내용
                    Debug.Log("Success Put Rank.");
                }
                else
                {
                    // 응답이 실패했을 때 처리하는 내용
                    Debug.Log("Failed to receive response.");
                }
            });
        }
        void Update()
        {
        
        }
    }
}
