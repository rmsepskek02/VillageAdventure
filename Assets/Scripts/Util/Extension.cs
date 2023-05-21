using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Object;

namespace VillageAdventure.Util
{
    public static class Extension 
    {
        public static void Update<T>(this List<T> list) where T : Obj
        {
            // (int i = list.Count-1; i>=0; i--)
            for (int i = 0; i < list.Count; i++)
            {
                // 객체의 참조가 존재하는지 확인
                if (list[i])
                {
                    // 존재한다면 객체의 업데이트 실행
                    list[i].Execute();
                }
                // 존재하지 않는다면, 게임 도중 특정 조건으로 인해 객체가 제거된 경우
                else
                {
                    // 제거된 객체이므로 리스트에서 해당 참조를 제거
                    list.RemoveAt(i);
                    i--;
                }

                // 주의해야할 점
                // 반복문 안에서 리스트의 원소를 제거할 때 주의할점
                /// 1. 반복문의 증감식을 ++로 사용할 경우, 원소를 제거 후 i 값을 -- 함
                /// 2. 반복문의 증감식을 --로 사용하고, 초기식을 마지막 인덱스로 지정하여
                ///    뒤에 원소부터 앞의 원소순으로 반복
            }
        }
    }
}