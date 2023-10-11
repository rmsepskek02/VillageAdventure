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

        #region Buttons <=> Objs 이벤트 연결
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

        #region 키보드 조작
        // 키보드로 빌드 조작
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
            // bulidOfTypeUI 인 경우 
            if (typeOfBuildHolderObj.gameObject.activeSelf)
            {
                buildButtonHolderObj.SetActive(true);
                typeOfBuildHolderObj.SetActive(false);
            }
            // ScoreObject 인 경우
            if (typeOfBuildObj[i].gameObject.activeSelf)
            {
                typeOfBuildHolderObj.SetActive(true);
                typeOfBuildObj[i].SetActive(false);
                _selectedUI = 0;
            }
        }
        // ScoreObject 선택 
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
        // ScoreObject Number 결정
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

        #region 마우스 이벤트
        // Build Button 클릭
        private void OnClickBuild()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            buildButtonHolderObj.SetActive(false);
            typeOfBuildHolderObj.SetActive(true);
        }
        // Reset Button 클릭
        private void OnClickReset()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        // bulidOfTypeUI Button 클릭
        private void OnClickBuildObjectUI(GameObject buildButtonObjsUI, int index)
        {
            InGameManager.Instance.sdTypeIndex = index;
            buildButtonObjsUI.SetActive(true);
            typeOfBuildHolderObj.SetActive(false);
            _selectedUI = index;
        }

        // ScoreObject Button 클릭
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
        // Back Button 클릭
        private void OnClickBack()
        {
            InGameManager.Instance.BuildObject.GetComponent<SpriteRenderer>().sprite = null;
            // bulidOfTypeUI 인 경우 
            if (typeOfBuildHolderObj.gameObject.activeSelf)
            {
                buildButtonHolderObj.SetActive(true);
                typeOfBuildHolderObj.SetActive(false);
            }
            // ScoreObject 인 경우
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

        /// UI에서 최종적으로 선택한 오브젝트가 마우스 포인터를 따라서 청사진 UI가 표출과 
        /// 동시에 카메라의 밝기를 조금 어둡게, 게임은 정지
        /// 만약 선택 취소를 원한다면, ESC로 UIBuild의 초기 UI가 표출(이 상태에서 뒤로가기 기능?)
        /// 건설불가능 지역인 경우 적사진 UI가 표출, 건설불가능(건설불가능한 지역 선택할 경우 UI표출?)
        /// 청사진 UI 상태에서 좌클릭을 하면 해당 좌표로 캐릭터가 이동한 뒤
        /// 이동이 불가능한 구역이라면 건설불가능(UI표출?, 이동이 불가능한 구역인지 어떻게 판단?)
        /// 해당 오브젝트의 건설에 필요한 재화를 소모한 뒤 해당 좌표 상단에 Bar가 먼저 생성되고, 
        /// 재화가 부족하면 건설 불가능(UI표출?)
        /// Bar가 지워지면 오브젝트 생성
        /// 생성한 오브젝트에 따라서 score에 영향
        // 길찾기가 어려움 fail 나중에 해보자

        /// UI에서 최종적으로 선택한 오브젝트가 플레이어 앞에 청사진 sprite가 표출과 
        /// 동시에 카메라의 밝기를 조금 어둡게
        /// 만약 선택 취소를 원한다면, ESC로 UIBuild의 초기 UI가 표출(이 상태에서 뒤로가기 기능?)
        /// 건설불가능 지역인 경우 적사진 sprite가 표출, 건설불가능(건설불가능한 지역 선택할 경우 UI표출?)
        /// 청사진 UI 상태에서 좌클릭을 하면 해당 오브젝트의 건설에 필요한 재화를 소모한 뒤 해당 좌표 상단에 Bar가 먼저 생성되고, 
        /// 재화가 부족하면 건설 불가능(UI표출?)
        /// Bar가 지워지면 오브젝트 생성
        /// 생성한 오브젝트에 따라서 score에 영향
        // 건물 오브젝트의 데이터를 담아둘 DataBase가 필요
        // 플레이어 앞에 표출해줄 오브젝트 필요 (청사진용 객체)
        // 선택한 오브젝트의 sprite를 청사진용 객체 sprite로 전달
        // 조건에 따라color tint를 조절하여 청,적 표출
        // esc 키입력과 그에 따른 결과
        // enter 키입력 청사진용 객체의 좌표에 건물 관련 프리펩클론 생성
        // 건설불가능한 경우에 따른 UI 표출
    }
}