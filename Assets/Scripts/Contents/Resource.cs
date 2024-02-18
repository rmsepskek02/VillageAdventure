using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.Object
{

    public class Resource : Obj
    {
        /// 0~9 랜덤 수 추출 (자식오브젝트 개수를 받아서 최대값을 지정 못하나????????)

        /// 씬이 넘어가도 처음에 형성하고나서부터 계속 유지 못하나????????

        // 리스트 작성
        public List<int> resourceList = new List<int>();
        float time = 0;
        float foodTime = 0;
        // Food
        Animator anim = null;

        public override void Init()
        {
            anim = GetComponent<Animator>();
        }
        public override void Execute()
        {
            base.Execute();
        }
        private void Update()
        {
            RandomSetActive(-0.5f, 14.5f);
            FoodObject();
        }
        // 랜덤 생성
        public void RandomSetActive(float min, float max)
        {
            ActiveObject();
            if (gameObject.name == "TreeHolder" || gameObject.name == "MineHolder")
            {
                // 리스트의 개수가 15개면 스탑
                if (resourceList.Count == 15)
                {
                    return;
                }
                // 시간 딜레이 형성
                time += Time.deltaTime;
                // 시간 체크
                if (time >= 2f)
                {
                    // 랜덤수 추출
                    int currentNumber = Mathf.RoundToInt(Random.Range(min, max));
                    // 리스트에 랜덤수가 없다면
                    if (!resourceList.Contains(currentNumber))
                    {
                        //// 자식의 자식오브젝트를 비활성화
                        //gameObject.transform.GetChild(currentNumber).transform.GetChild(0).gameObject.SetActive(false);
                        //// 랜덤수에 따라 자식오브젝트 활성화
                        //gameObject.transform.GetChild(currentNumber).gameObject.SetActive(true);
                        // 랜덤수 리스트에 추가
                        resourceList.Add(currentNumber);
                        //Debug.Log(currentNumber);

                        ActiveObject();
                    }
                    time = 0;
                }
            }
        }

        private void ActiveObject()
        {
            for (int i = 0; i < resourceList.Count; ++i)
            {
                var ResourceObj = gameObject.transform.GetChild(resourceList[i]);

                if (!ResourceObj.gameObject.activeSelf)
                {
                    ResourceObj.transform.GetChild(0).gameObject.SetActive(false);
                    ResourceObj.gameObject.SetActive(true);
                }
            }
        }
        private void FoodObject()
        {
            if (gameObject.tag == "NotFood")
                foodTime += Time.deltaTime;
            if (foodTime >= 3f)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                anim.SetBool("isComplete", true);
                gameObject.tag = "Food";
                foodTime = 0;
            }
        }
        private void Fishing()
        {

        }
    }
}
