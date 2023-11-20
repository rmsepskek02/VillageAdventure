using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure.Controller;
using VillageAdventure.Object;
using VillageAdventure.Util;

namespace VillageAdventure
{
    public class PlayerController : Singleton<PlayerController>
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
            // �÷��̾� ĳ������ ���̶�Ű ���� �θ� �÷��̾� ��Ʈ�ѷ��� ����
            PlayerCharactor.transform.SetParent(transform);
            // �θ� �����Ͽ����Ƿ�, ĳ������ ��ġ�� �θ� �������� 0,0,0 ��ġ�� ��ġ
            PlayerCharactor.transform.localPosition = Vector3.zero;

            // ĳ���� ������ ���� �Է� ��Ʈ�ѷ� ��ü�� ����
            inputcontroller = new InputController();
            // ����� Ű �� Ű�� �ش��ϴ� ��ɵ��� ���
            inputcontroller.AddAxis("Horizontal", OnMoveX);
            inputcontroller.AddAxis("Vertical", OnMoveY);
        }

        private void FixedUpdate()
        {
            // �÷��̾� ĳ������ �����Ͱ� ���õǱ� ������ �Է� �Ұ��� ����ó��
            if (PlayerCharactor.boActor == null || InGameManager.Instance.isDead == true)
                return;
            InputUpdate();
        }

        private void InputUpdate()
        {
            foreach (var input in inputcontroller.inputAxes)
                input.Value.GetAxisValue(Input.GetAxisRaw(input.Key));
            foreach (var input in inputcontroller.inputButtons)
            {
                // Ű�� ���� ���� 1ȸ
                if (Input.GetButtonDown(input.Key))
                    input.Value.OnDown();
                // Ű�� ����ؼ� ������ �ִ� ���
                else if (Input.GetButton(input.Key))
                    input.Value.OnPress();
                // Ű�� �����ٰ� ���� ���� 1ȸ
                else if (Input.GetButtonUp(input.Key))
                    input.Value.OnUp();
                // Ű�� ����ؼ� ���� �ִ� ��� (�ƹ��� ��ȣ �ۿ��� ���� ���)
                else
                    input.Value.OnNotPress();
            }
        }
        #region Input Implementation(�Է� ������)
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