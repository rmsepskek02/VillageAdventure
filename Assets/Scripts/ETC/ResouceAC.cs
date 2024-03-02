using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VillageAdventure.Object
{
    class ResouceAC : MonoBehaviour
    {
        Animator anim = null;

        private void Start()
        {
            anim = transform.root.GetComponent<Animator>();
        }
        public void BeActive_AnimEvent()
        {
            /// 모든 리소스에 일반화 작업??????
            /// 리소스의 구조가 규칙이 생겨버림
            /// Holder - 리소스 - Baroutline - Bar 구조
            // 이 오브젝트가 Mine 이라면
            if (gameObject.transform.parent.transform.parent.CompareTag("Mine"))
            {
                // 인벤토리에 랜덤으로 재화 획득
                InGameManager.Instance.mine += (int)UnityEngine.Random.Range(1, 5) * UIMerchant.miningLevel;
                ChildNumber(0);
            }
            // 이 오브젝트가 Tree 이라면
            else if (gameObject.transform.parent.transform.parent.CompareTag("Tree"))
            {
                // 인벤토리에 랜덤으로 재화 획득
                InGameManager.Instance.tree += (int)UnityEngine.Random.Range(1, 5) * UIMerchant.loggingLevel;
                ChildNumber(1);
            }
            // 이 오브젝트가 Food 라면
            else if (gameObject.transform.parent.transform.parent.CompareTag("Food"))
            {
                // 음식 재화 획득
                InGameManager.Instance.food++;
                // 음식 상호작용 결과
                InGameManager.Instance.playerHP += 5f;
                // 음식의 상태 변경
                gameObject.transform.parent.transform.parent.gameObject.tag = "NotFood";
                // 음식의 애니메이션 상태 변경
                gameObject.transform.parent.transform.parent.GetComponent<Animator>().SetBool("isComplete", false);
                // Bar 비활성화
                transform.parent.gameObject.SetActive(false);
            }
            // 화덕에 불이 꺼진 경우, 이 오브젝트가 UnFire 라면
            else if(gameObject.transform.parent.transform.parent.CompareTag("UnFire"))
            {
                // 화덕 애니메이션 ON
                gameObject.transform.parent.transform.parent.GetComponent<Animator>().SetBool("isFire", true);
                // 화덕 상태를 Fire로 변경
                gameObject.transform.parent.transform.parent.gameObject.tag = "Fire";
                // 목재 사용
                InGameManager.Instance.tree--;
                // Bar 비활성화
                transform.parent.gameObject.SetActive(false);
            }
            // 화덕에 불이 켜진 경우, 이 오브젝트가 Fire 라면
            else if (gameObject.transform.parent.transform.parent.CompareTag("Fire"))
            {
                // 재화 사용
                InGameManager.Instance.fish--;
                // 재화 사용의 결과
                InGameManager.Instance.playerHP += 30f;
                // Bar 비활성화
                transform.parent.gameObject.SetActive(false);
            }
            // 낚시 중인 경우, 이 오브젝트가 Fishing 이라면
            else if (gameObject.transform.root.CompareTag("Fishing"))
            {
                // 낚시 재화 획득
                InGameManager.Instance.fish += (int)UnityEngine.Random.Range(1, 5) * UIMerchant.fishingLevel;
                // Bar 비활성화
                transform.parent.gameObject.SetActive(false);
            }
        }

        // 이 오브젝트가 최상위 부모의 N번째 오브젝트인지 검사 후 리스트에서 N을 제거
        void ChildNumber(int i)
        {
            var resourceList = gameObject.transform.root.GetChild(i).GetComponent<Resource>().resourceList;
            
            // 이 오브젝트가 최상위 부모의 N번째 오브젝트인지 검사
            for (int childNumber = 0; childNumber < resourceList.Count; childNumber++)
            {
                // 이 오브젝트가 N번째 오브젝트인지 검사 
                if (gameObject.transform.parent.transform.parent == gameObject.transform.root.GetChild(i).GetChild(resourceList[childNumber]))
                {
                    // 리스트에서 N을 제거 후 멈춤
                    resourceList.Remove(resourceList[childNumber]);
                    break;
                }
            }
            transform.parent.transform.parent.gameObject.SetActive(false);
            /// 후순위 자식오브젝트 일수록 리스트 제거가 안이루어지는거 같음????????????
            // 랜덤생성하는 과정에서 자체적으로 한번 검사하는 걸로 해결
        }

    }
}
