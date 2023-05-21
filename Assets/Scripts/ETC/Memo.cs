using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo
{
    // 슬라임 생성하기 
    // sdMonster List에 추가가 안되어있음 왜?

    // 스포닝 풀로 옮기기
    //		var sdMonsters = GameManager.SD.sdMonsters.Where(_ => _.index == 3000).SingleOrDefault();
    //		var testMonster= Instantiate(Resources.Load<GameObject>(sdMonsters.resourcePath));
    //		int index = testMonster.name.IndexOf("(Clone)");
    //		if(index > 0)
    //			testMonster.name = testMonster.name.Substring(0, index);

    /// 몰?루
    // foreach (var input in inputcontroller.inputAxes) 에러 뜸 PlayerController 43
    // SceneManager에서 씬관리?? GM에 있는거 날리고 인스턴스만 만드는애로

    /// 플레이어
    /*    /// 플레이어 체력
        // 음식으로 회복*/

    // GameOver 구현 = isDead 활용해서 게임 멈추고, UI 띄우기
    /// 플레이어 기능
    /// 건물 생성과 철거
    /*    // 건설할때 캐릭터의 앞에 생성
        // UI 선택 후 특정 구역을 선택하여 건설
        // 건설하려는데 다른 오브젝트가 겹치면 건설불가*/
    // 철거하는 기능

    /// 게임 스코어
    // UI, 건물에 따라 변경, GameVictory 구현

    /// 리소스
    /*    /// 음식 
        // 애니메이션 활용
        // 하나의 오브젝트로 쓸거임 (Bar를 제외하고)
        // 애니메이션으로 Sprite 컨트롤
        // Food Object의 Tag를 변경하여 접근제한
        // Bar는 mine이랑 같은구조*/

    // 특정 오브젝트에서 일정 시간마다 생성
    // 상호작용으로 섭취
    // 음식과 목재가 있어야 음식 오브젝트를 추가로 생성가능
    // 게임 시작과 동시에 해당 오브젝트 소량 제공
    // UI

    /*    /// 낚시
        // 음식과 동일
        // 차이점
        // 낚시 오브젝트에 접근 후 일정시간마다 상호작용시 재화 획득하는 구조
        // 오브젝트를 생성 불가능
        // UI
        // 획득한 재화는 불과 상호작용하는 경우에 체력 회복*/

    /*/// 불
    // UI 
    // 생성할때 마인 소모
    // 점화할때 목재 소모
    // 건물처럼 건설하는 방식*/

    /// 건물
    // 게임 스코어에 영향
    // 목재 및 광석을 소모
    // UI
    // 생성가능한 영역 설정???
    /// 집 밖
    // 슬라임에 의해 파괴 가능
    /// 집 안
    // 슬라임에 의해 파괴 불가
    // 플레이어의 능력치에 영향???

    /// 상인
    // 중립 오브젝트로 특정조건에 따라 맵에 등장, AI를 부여???
    // 광물을 사용하여 거래
    // 상호작용하면 UI 표출
    // 용병 구매
    // 플레이어의 능력 상승???

    /// 슬라임
    // 적 오브젝트로 특정조건에 따라 맵에 등장
    // 건물을 파괴 및 용병을 공격하는 AI 오브젝트

    /// 용병
    // 슬라임을 공격하는 AI 오브젝트
    // 오브젝트 수에 따라 일정시간마다 음식 소모
    // 음식이 없을 경우 일정시간 후 삭제??? 

    /*    /// UI
        // 우측상단 게임현황
        // 건설 전 UI 표출*/

    /// 게임환경
    /// 세이브
    // 현재 게임 세이브
    /// 옵션
    // 각종 옵션기능
    /// 일시중지
    // 일시중지기능
    /// Help
    // 도움말 기능 UI로 게임 전반적인 부분 설명

    /// 씬 데이터 유지
    // BoData 처럼 객체마다 각각 데이터를 저장하고 객체가 파괴되어도 저장했던 데이터를 불러와서 덮어쓰는 방법

    /// 버그
    // 리소스 오브젝트 생성시 플레이어가 생성장소에 있으면 밀려남, 밀려나면서 타일 콜라이더에 갇히는 현상이 있음

    // 시간 딜레이 코드
    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(0.3f);
    }
}


// UIBuild 에서 선택함에 따라 GameManager에서 SD를 다른 값을 받아옴
// Player에서 이걸 참조해서 적용함

/// UI Home을 누르면 HomeScoreObject만 나열
/// Home을 눌렀으니 HomeObject만 생성이 가능 = sdTypeIndex에 따라 GameManager의 sd 값을 다르게 받아와 Player가 사용
/// 어디든 만들 수 있는건 양쪽 SD List에 추가하면 가능할듯?
/// 


