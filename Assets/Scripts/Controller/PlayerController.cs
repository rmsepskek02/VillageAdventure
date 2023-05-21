using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Controller;
using VillageAdventure.Object;

namespace VillageAdventure
{
    public class PlayerController : MonoBehaviour
    {
        private InputController inputcontroller;
        public Player PlayerCharactor { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        public void Initialize(Player player)
        {
            PlayerCharactor = player;
            // 플레이어 캐릭터의 하이라키 상의 부모를 플레이어 컨트롤러로 생성
            PlayerCharactor.transform.SetParent(transform);
            // 부모를 변경하였으므로, 캐릭터의 위치를 부모를 기준으로 0,0,0 위치에 배치
            PlayerCharactor.transform.localPosition = Vector3.zero;

            // 캐릭터 조작을 위해 입력 컨트롤러 객체를 생성
            inputcontroller = new InputController();
            // 사용할 키 및 키에 해당하는 기능들을 등록
            inputcontroller.AddAxis("Horizontal", OnMoveX);
            inputcontroller.AddAxis("Vertical", OnMoveY);
        }

        private void FixedUpdate()
        {
            // 플레이어 캐릭터의 데이터가 세팅되기 전에는 입력 불가능 예외처리
            if (PlayerCharactor.boActor == null)
                return;
            InputUpdate();
        }

        private void InputUpdate()
        {
            foreach (var input in inputcontroller.inputAxes)
                input.Value.GetAxisValue(Input.GetAxisRaw(input.Key));
            foreach (var input in inputcontroller.inputButtons)
            {
                // 키를 누른 순간 1회
                if (Input.GetButtonDown(input.Key))
                    input.Value.OnDown();
                // 키를 계속해서 누르고 있는 경우
                else if (Input.GetButton(input.Key))
                    input.Value.OnPress();
                // 키를 눌렀다가 떼는 순간 1회
                else if (Input.GetButtonUp(input.Key))
                    input.Value.OnUp();
                // 키를 계속해서 떼고 있는 경우 (아무런 상호 작용이 없는 경우)
                else
                    input.Value.OnNotPress();
            }
        }
        #region Input Implementation(입력 구현부)
        private void OnMoveX(float value)
        {
            if (PlayerCharactor.boPlayer.moveDirection.y != 0)
                return;
            PlayerCharactor.boPlayer.moveDirection.x = value;
        }
        private void OnMoveY(float value)
        {
            if (PlayerCharactor.boPlayer.moveDirection.x != 0)
                return;
            PlayerCharactor.boPlayer.moveDirection.y = value;
        }
        #endregion
    }
}