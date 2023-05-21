using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VillageAdventure.Object
{
    class FirePot : Obj
    {
        float delayTime = 0;
        float fireTime = 0;
        Animator anim = null;
        
        public override void Init()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Firing();
        }

        private void Firing()
        {
            // 화덕에 불이 붙는 과정과 불이 꺼지는 과정
            if (anim.GetBool("isFire") == true)
            {
                delayTime += Time.deltaTime;
                // 화덕 flame 이 켜지는 경우와 딜레이
                if(delayTime>= 1f)
                {
                    // 화덕 flame 애니메이션 ON
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    // 화덕 flame 애니메이션 딜레이 시간 초기화
                    delayTime = 0;
                }
                /// 불이 켜진 상태에서 상호작용 중 불이 꺼지면 불이 꺼진 상태에서의 상호작용 결과가 나타남
                // 상호작용으로 Bar가 활성화하지 않는 경우에만 불이 꺼지는 딜레이 카운트를 실행
                /// 근데 이러면 상호작용으로 Bar가 활성화되어 있는 만큼 불이 꺼지는 딜레이가 늦어짐 ????????????????????
                if (gameObject.transform.GetChild(0).gameObject.activeSelf == false)
                {
                    fireTime += Time.deltaTime;
                    // 화덕 flame 이 꺼지는 경우와 딜레이
                    if (fireTime >= 5f)
                    {
                        // 화덕 flame 애니메이션 OFF
                        gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        // 화덕 애니메이션 OFF
                        anim.SetBool("isFire", false);
                        // 화덕 현재 상태 변경 (태그사용)
                        gameObject.tag = "UnFire";
                        // 불이 꺼지는 시간까지 시간 초기화
                        fireTime = 0;
                    }
                }
            }
        }
    }
}
