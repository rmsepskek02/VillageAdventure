using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VillageAdventure.UI
{
    public class UIBuild: MonoBehaviour
    {
        #region Button Objs
        // Build Reset Button
        public Button buildButtonUI;
        public Button resetButtonUI;
        public GameObject buildButtonHolderObj;

        // Build Type Button Parent
        public GameObject typeOfBuildHolderObj;

        // Build Type Button & Objs
        public Button[] typeOfBuildUI;
        public GameObject[] typeOfBuildObj;

        // Home Tpye Button & Objs
        public Button[] homeButtons;
        public GameObject[] homeObjs;

        // Field Tpye Button & Objs
        public Button[] fieldButtons;
        public GameObject[] fieldObjs;

        // ETC.Home Tpye Button & Objs
        public Button[] commonButtons;
        public GameObject[] commonObjs;

        // ETC.FieldTpye Button & Objs
        public Button[] resourceButtons;
        public GameObject[] resourceObjs;
        #endregion

        private SpriteRenderer sprite;
        private Image image;

        private void Start()
        {
            buildButtonUI.onClick.AddListener(OnClickBuild);
            resetButtonUI.onClick.AddListener(OnClickReset);

            sprite = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();

            ConnectUIEvent(typeOfBuildUI, typeOfBuildObj, 0, true);
            ConnectUIEvent(homeButtons, homeObjs, 3000);
            ConnectUIEvent(fieldButtons, fieldObjs, 4000);
            ConnectUIEvent(commonButtons, commonObjs, 5000);
            ConnectUIEvent(resourceButtons, resourceObjs, 6000);
        }

        private void Update()
        {
            GetKeyUp();
        }

        #region Buttons <=> Objs �̺�Ʈ ����
        private void ConnectUIEvent(Button[] buttons, GameObject[] objs, int objType, bool typeUI = false)
        {
            for (int i = 0; i < buttons.Length; ++i)
            {
                int index = i;
                int objIndex = i + objType;
                buttons[i].onClick.AddListener(() =>
                {
                    if (index == (buttons.Length - 1))
                        OnClickBack();
                    else
                    {
                        if(typeUI == true)
                            OnClickBuildObjectUI(objs[index], index);
                        else
                            OnClickBuildObject(objs[index], objIndex);
                    }
                });
            }
        }
        #endregion

        #region Ű���� ����
        // Ű����� ���� ����
        int _selectedUI = 0;
        int _selectedObjNum = 0;
        GameObject[] _selectedObj = null;
        public void GetKeyUp()
        {
            // Build Button
            if (Input.GetKeyUp(KeyCode.B) && buildButtonHolderObj.gameObject.activeSelf)
            {
                InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
                buildButtonHolderObj.SetActive(false);
                typeOfBuildHolderObj.SetActive(true);
            }
            // Reset Button
            if (Input.GetKeyUp(KeyCode.R) && buildButtonHolderObj.gameObject.activeSelf)
                InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            if (typeOfBuildHolderObj.gameObject.activeSelf)
            {
                // Home Button
                if (Input.GetKeyUp(KeyCode.H))
                    OnClickBuildObjectUI(typeOfBuildObj[0], 0);
                // Field Button
                if (Input.GetKeyUp(KeyCode.F))
                    OnClickBuildObjectUI(typeOfBuildObj[1], 1);
                // ETC Home Button
                if (Input.GetKeyUp(KeyCode.C))
                    OnClickBuildObjectUI(typeOfBuildObj[2], 2);
                // ETC Field Button
                if (Input.GetKeyUp(KeyCode.S))
                    OnClickBuildObjectUI(typeOfBuildObj[3], 3);
            }
            // Back Button
            if (Input.GetKeyUp(KeyCode.R))
            {
                BackTypeUI(_selectedUI);
            }

            _selectedObjNum = SelectScoreObjectNum(_selectedUI);
            if (_selectedObjNum != 0)
            {
                if (_selectedUI == 0)
                    _selectedObj = homeObjs;
                if (_selectedUI == 1)
                    _selectedObj = fieldObjs;
                if (_selectedUI == 2)
                    _selectedObj = commonObjs;
                if (_selectedUI == 3)
                    _selectedObj = resourceObjs;
                SelectScoreObject(_selectedObj, _selectedUI);
            }
        }
        // Back Button
        private void BackTypeUI(int i)
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            // bulidOfTypeUI �� ��� 
            if (typeOfBuildHolderObj.gameObject.activeSelf)
            {
                buildButtonHolderObj.SetActive(true);
                typeOfBuildHolderObj.SetActive(false);
            }
            // ScoreObject �� ���
            if (typeOfBuildObj[i].gameObject.activeSelf)
            {
                typeOfBuildHolderObj.SetActive(true);
                typeOfBuildObj[i].SetActive(false);
                _selectedUI = 0;
            }
        }
        // ScoreObject ���� 
        private void SelectScoreObject(GameObject[] buildObj, int i)
        {
            _selectedObjNum = SelectScoreObjectNum(_selectedUI);
            int index = _selectedObjNum - 1;

            if (index < 0)
                index = 0;
            if (_selectedObjNum > buildObj.Length)
                return;

            if (typeOfBuildObj[i].gameObject.activeSelf)
            {
                if (buildObj[index].GetComponent<Image>() != null)
                {
                    InGameManager.Instance.sdIndex = index + (_selectedUI+3)*1000;
                    InGameManager.Instance.UIBuildActivated = true;
                    InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = buildObj[index].GetComponent<Image>().sprite;
                }
                buildButtonHolderObj.SetActive(true);
                typeOfBuildObj[i].SetActive(false);
                _selectedObjNum = 0;
            }
        }
        // ScoreObject Number ����
        private int SelectScoreObjectNum(int i)
        {
            if (!typeOfBuildObj[i].gameObject.activeSelf)
            {
                return _selectedObjNum;
            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
                _selectedObjNum = 1;
            if (Input.GetKeyUp(KeyCode.Alpha2))
                _selectedObjNum = 2;
            if (Input.GetKeyUp(KeyCode.Alpha3))
                _selectedObjNum = 3;
            if (Input.GetKeyUp(KeyCode.Alpha4))
                _selectedObjNum = 4;
            if (Input.GetKeyUp(KeyCode.Alpha5))
                _selectedObjNum = 5;
            if (Input.GetKeyUp(KeyCode.Alpha6))
                _selectedObjNum = 6;
            if (Input.GetKeyUp(KeyCode.Alpha7))
                _selectedObjNum = 7;
            if (Input.GetKeyUp(KeyCode.Alpha8))
                _selectedObjNum = 8;
            if (Input.GetKeyUp(KeyCode.Alpha9))
                _selectedObjNum = 9;

            return _selectedObjNum;
        }
        #endregion

        #region ���콺 �̺�Ʈ
        // Build Button Ŭ��
        private void OnClickBuild()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            buildButtonHolderObj.SetActive(false);
            typeOfBuildHolderObj.SetActive(true);
        }
        // Reset Button Ŭ��
        private void OnClickReset()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        // bulidOfTypeUI Button Ŭ��
        private void OnClickBuildObjectUI(GameObject buildButtonObjsUI, int index)
        {
            InGameManager.Instance.sdTypeIndex = index;
            buildButtonObjsUI.SetActive(true);
            typeOfBuildHolderObj.SetActive(false);
            _selectedUI = index;
        }

        // ScoreObject Button Ŭ��
        private void OnClickBuildObject(GameObject buildObj, int index)
        {
            if (buildObj.GetComponent<Image>() != null)
            {
                InGameManager.Instance.sdIndex = index;
                InGameManager.Instance.UIBuildActivated = true;
                InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = buildObj.GetComponent<Image>().sprite;
            }
            buildButtonHolderObj.SetActive(true);
            buildObj.transform.parent.gameObject.SetActive(false);
        }
        // Back Button Ŭ��
        private void OnClickBack()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            // bulidOfTypeUI �� ��� 
            if (typeOfBuildHolderObj.gameObject.activeSelf)
            {
                buildButtonHolderObj.SetActive(true);
                typeOfBuildHolderObj.SetActive(false);
            }
            // ScoreObject �� ���
            if (typeOfBuildObj[_selectedUI].gameObject.activeSelf)
            {
                typeOfBuildHolderObj.SetActive(true);
                typeOfBuildObj[_selectedUI].SetActive(false);
                _selectedUI = 0;
            }
            //buildObj.transform.parent.gameObject.SetActive(false);
            //buildButtonHolderObj.SetActive(true);
        }
        #endregion

        /// UI���� ���������� ������ ������Ʈ�� ���콺 �����͸� ���� û���� UI�� ǥ��� 
        /// ���ÿ� ī�޶��� ��⸦ ���� ��Ӱ�, ������ ����
        /// ���� ���� ��Ҹ� ���Ѵٸ�, ESC�� UIBuild�� �ʱ� UI�� ǥ��(�� ���¿��� �ڷΰ��� ���?)
        /// �Ǽ��Ұ��� ������ ��� ������ UI�� ǥ��, �Ǽ��Ұ���(�Ǽ��Ұ����� ���� ������ ��� UIǥ��?)
        /// û���� UI ���¿��� ��Ŭ���� �ϸ� �ش� ��ǥ�� ĳ���Ͱ� �̵��� ��
        /// �̵��� �Ұ����� �����̶�� �Ǽ��Ұ���(UIǥ��?, �̵��� �Ұ����� �������� ��� �Ǵ�?)
        /// �ش� ������Ʈ�� �Ǽ��� �ʿ��� ��ȭ�� �Ҹ��� �� �ش� ��ǥ ��ܿ� Bar�� ���� �����ǰ�, 
        /// ��ȭ�� �����ϸ� �Ǽ� �Ұ���(UIǥ��?)
        /// Bar�� �������� ������Ʈ ����
        /// ������ ������Ʈ�� ���� score�� ����
        // ��ã�Ⱑ ����� fail ���߿� �غ���

        /// UI���� ���������� ������ ������Ʈ�� �÷��̾� �տ� û���� sprite�� ǥ��� 
        /// ���ÿ� ī�޶��� ��⸦ ���� ��Ӱ�
        /// ���� ���� ��Ҹ� ���Ѵٸ�, ESC�� UIBuild�� �ʱ� UI�� ǥ��(�� ���¿��� �ڷΰ��� ���?)
        /// �Ǽ��Ұ��� ������ ��� ������ sprite�� ǥ��, �Ǽ��Ұ���(�Ǽ��Ұ����� ���� ������ ��� UIǥ��?)
        /// û���� UI ���¿��� ��Ŭ���� �ϸ� �ش� ������Ʈ�� �Ǽ��� �ʿ��� ��ȭ�� �Ҹ��� �� �ش� ��ǥ ��ܿ� Bar�� ���� �����ǰ�, 
        /// ��ȭ�� �����ϸ� �Ǽ� �Ұ���(UIǥ��?)
        /// Bar�� �������� ������Ʈ ����
        /// ������ ������Ʈ�� ���� score�� ����
        // �ǹ� ������Ʈ�� �����͸� ��Ƶ� DataBase�� �ʿ�
        // �÷��̾� �տ� ǥ������ ������Ʈ �ʿ� (û������ ��ü)
        // ������ ������Ʈ�� sprite�� û������ ��ü sprite�� ����
        // ���ǿ� ����color tint�� �����Ͽ� û,�� ǥ��
        // esc Ű�Է°� �׿� ���� ���
        // enter Ű�Է� û������ ��ü�� ��ǥ�� �ǹ� ���� ������Ŭ�� ����
        // �Ǽ��Ұ����� ��쿡 ���� UI ǥ��
    }
}