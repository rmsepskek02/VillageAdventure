using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure.Object;

namespace VillageAdventure
{
    public class UIController : MonoBehaviour
    {
        public RectTransform currentGauge;
        public RectTransform lifeBar;

        public Text time;
        public Text mine;
        public Text tree;
        public Text food;
        public Text fish;
        public Text score;
        public GameObject playerHP;


        private void Start()
        {

        }
        private void Update()
        {
            DisplayTime();
            ObjectCount();
            ScoreCount();
            CalculateLifeGauge();
        }

        private void DisplayTime()
        {
            time.text = $"Time [{InGameManager.Instance.hour} : {Mathf.Floor(InGameManager.Instance.min/60)}]";
        }

        private void ObjectCount()
        {
            mine.text = $": {InGameManager.Instance.mine}";
            tree.text = $": {InGameManager.Instance.tree}";
            food.text = $": {InGameManager.Instance.food}";
            fish.text = $": {InGameManager.Instance.fish}";
        }

        private void ScoreCount()
        {
            score.text = $"Score : {InGameManager.Instance.score}";
        }

        private void CalculateLifeGauge()
        {
            // 현재 체력을 나타내는 캐릭터 이미지의 현재 위치를 받아옴
            Vector2 position = currentGauge.anchoredPosition;
            // 좌측을 0%, 우측을 100%로 보이도록 캐릭터 이미지의 x 위치를 변경
            /// 플레이어의 배고픔 값이 100을 넘지 않도록 (배고픔 값 범위 0~100)
            position.x = lifeBar.sizeDelta.x * (InGameManager.Instance.playerHP / 100) - 150f;
            // 변경한 위치를 적용
            currentGauge.anchoredPosition = position;
        }
    }
}