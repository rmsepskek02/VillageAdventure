using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo
{
    #region TODO
    /* 
    Load 했을때 상인관련 data UI에 저장된 data 값 표출하기
    새 게임 시작하는 경우 상인관련 data 초기화
    Load 하는 경우 해당 data에 맞게 상인관련 data 저장 값에 맞게 Load
    
    - 상인 작업
    해당위치는 접근불가능하게 타일 조정??
    관련 UI 작업 및 컨텐츠 필요
    ex) 캐릭터 이동속도 상승, 재화획득률 상승, 타이머 느리게 가기, 워리어 판매, 워리어 공격력 증가 등등

    data
    대표이미지
    재화소모 1~3개 수치, 이미지
    설명(레벨)
    코드값
    분류
    - 버프 / 워리어 / 아이템

    UI 스크립트 따로 하나 필요

    - 오브젝트 추가 작업
    - 게임 난이도 설정

    메뉴작업
    option - 해상도
    exit - 게임 종료
     
    각 씬별 노래 재생
    데드씬 - 뜻대로 되지 않는
    몬스터씬 - 
    상점씬 - 마을 상점

    /// 버그
    리소스 오브젝트 생성시 플레이어가 생성장소에 있으면 밀려남, 밀려나면서 타일 콜라이더에 갇히는 현상이 있음
    오브젝트 건설시 트리거 관련 버그 있음 트리거 벗어나면서 다시 겹치는 경우 청사진임
    타켓포지션에 도착했을때 에러뜨는 부분 확인

    그리드 그려지는 시기 수정 
    */
    #endregion


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


