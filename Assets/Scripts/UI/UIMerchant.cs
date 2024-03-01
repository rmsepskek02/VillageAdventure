using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure;

public class UIMerchant : MonoBehaviour
{
    public Button cancelButton;
    public GameObject merchandisePrefab;
    public GameObject mdVertical;

    void Start()
    {
        cancelButton.onClick.AddListener(OnClickCancel);
        InitializeItemData();
    }

    void InitializeItemData()
    {
        // ���� ���, ��������Ʈ�� "Assets/Resources/Model/Pixelole's overword Tileset1.0 free/MasterSimple" ��ο� �ִٰ� �����մϴ�.
        Sprite[] resourceSprites = Resources.LoadAll<Sprite>("Model/Pixelole's overword Tileset1.0 free/MasterSimple");
        Sprite[] warriorSprites = Resources.LoadAll<Sprite>("Model/Character with sword and shield/idle/idle down1");
        Debug.Log("Test == " + resourceSprites.Length);
        Sprite mineSprite = resourceSprites[70];
        Sprite treeSprite = resourceSprites[38];
        Sprite fishSprite = resourceSprites[94];
        Sprite warriorSprite = warriorSprites[0];
        ItemData[] itemData = new ItemData[]
        {
            new ItemData("Health Potion", null, new int[] {1, 2, 3}, new Sprite[] {mineSprite, treeSprite, fishSprite}, "ü���� ȸ���մϴ�. ������ ���� ȸ������ �����մϴ�.", 1001, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, "����"),
            new ItemData("Iron Sword", warriorSprite, new int[] {}, null, "ö�� ������ ���� �����մϴ�. ������ ���� ���ݷ��� �����մϴ�.", 2001, "����")
            // ���⿡ �߰� �������� �߰��� �� �ֽ��ϴ�.
        };

        // ������ �����͸� �����տ� ǥ���մϴ�.
        for (int i = 0; i < itemData.Length; i += 2) // 2�� �����ϸ鼭 �� ���� ��� ó��
        {
            CreateMD(itemData[i], itemData[i + 1]); // �� ���� ������ �����͸� ����
        }
    }

    void Update()
    {

    }

    private void OnClickCancel()
    {
        InGameManager.Instance.isMerchant = false;
        GameManager.Instance.ReStartGame();
    }
    // �迭 �� ������Ʈ ����
    // image�� sprite ũ�Ⱑ ����
    // ��ư �̺�Ʈ ����
    // description�� int ������ �����Ͽ� lv.1 > 2 > 3 > 4 �� ���� �����ܰ�� �ö󰡵��� ����
    private void CreateMD(ItemData item1, ItemData item2)
    {
        // ������ ����
        GameObject newMerchandise = Instantiate(merchandisePrefab);
        newMerchandise.transform.SetParent(mdVertical.transform, false);
        Transform newMDtrans1 = newMerchandise.transform.GetChild(0).gameObject.transform;
        Transform newMDtrans2 = newMerchandise.transform.GetChild(1).gameObject.transform;
        newMDtrans1.GetChild(0).gameObject.GetComponent<Image>().sprite = item1.itemImage;
        newMDtrans1.GetChild(2).gameObject.GetComponent<Text>().text = item1.description;
        for (int i = 0; i < item1.currencyAmounts.Length; i++) 
        {
            newMDtrans1.GetChild(1).gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = item1.currencyImages[i];
            newMDtrans1.GetChild(1).gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = $"{item1.currencyAmounts[i]}";
        }
        newMDtrans2.GetChild(0).gameObject.GetComponent<Image>().sprite = item2.itemImage;
        newMDtrans2.GetChild(2).gameObject.GetComponent<Text>().text = item2.description;
        for (int i = 0; i < item2.currencyAmounts.Length; i++)
        {
            newMDtrans2.GetChild(1).gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = item2.currencyImages[i];
            newMDtrans2.GetChild(1).gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = $"{item2.currencyAmounts[i]}";
        }
    }
}
