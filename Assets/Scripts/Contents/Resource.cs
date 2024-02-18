using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.Object
{

    public class Resource : Obj
    {
        /// 0~9 ���� �� ���� (�ڽĿ�����Ʈ ������ �޾Ƽ� �ִ밪�� ���� ���ϳ�????????)

        /// ���� �Ѿ�� ó���� �����ϰ������� ��� ���� ���ϳ�????????

        // ����Ʈ �ۼ�
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
        // ���� ����
        public void RandomSetActive(float min, float max)
        {
            ActiveObject();
            if (gameObject.name == "TreeHolder" || gameObject.name == "MineHolder")
            {
                // ����Ʈ�� ������ 15���� ��ž
                if (resourceList.Count == 15)
                {
                    return;
                }
                // �ð� ������ ����
                time += Time.deltaTime;
                // �ð� üũ
                if (time >= 2f)
                {
                    // ������ ����
                    int currentNumber = Mathf.RoundToInt(Random.Range(min, max));
                    // ����Ʈ�� �������� ���ٸ�
                    if (!resourceList.Contains(currentNumber))
                    {
                        //// �ڽ��� �ڽĿ�����Ʈ�� ��Ȱ��ȭ
                        //gameObject.transform.GetChild(currentNumber).transform.GetChild(0).gameObject.SetActive(false);
                        //// �������� ���� �ڽĿ�����Ʈ Ȱ��ȭ
                        //gameObject.transform.GetChild(currentNumber).gameObject.SetActive(true);
                        // ������ ����Ʈ�� �߰�
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
