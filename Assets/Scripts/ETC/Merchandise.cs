using UnityEngine;
using UnityEngine.UI;

public class Merchandise : MonoBehaviour
{
    public ItemData[] itemData; // ������ ������ �迭
    public Image itemImage;
    public Text itemName;
    public Text description;
    public Text code;
    public Text category;

    void Start()
    {
        // ������ ���� �ʱ� ������ ������ ����
        InitializeItemData();
        // �ʱ� ������ ǥ��
        ShowItemDetails(0);
    }

    void InitializeItemData()
    {
        // ������ ������ �ʱ�ȭ
        itemData = new ItemData[]
        {
            new ItemData("Health Potion", null, new int[] {1, 2, 3}, null, "ü���� ȸ���մϴ�. ������ ���� ȸ������ �����մϴ�.", 1001, 1, 5, "����"),
            new ItemData("Strength Potion", null, new int[] {2, 3, 4}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, 1, 5, "����"),
            new ItemData("Iron Sword", null, new int[] {3, 4, 5}, null, "ö�� ������ ���� �����մϴ�. ������ ���� ���ݷ��� �����մϴ�.", 2001, 1, 5, "����")
        };
    }

    // �������� �������� �������� �� ȣ��Ǵ� �޼���
    public void OnItemClick(int index)
    {
        ShowItemDetails(index);
    }

    // ������ �������� ���� ������ ǥ���ϴ� �޼���
    void ShowItemDetails(int index)
    {
        ItemData currentItem = itemData[index];
        if (currentItem != null)
        {
            itemImage.sprite = currentItem.itemImage;
            itemName.text = currentItem.itemName;
            description.text = currentItem.description.Replace("����", "���� ����");
            code.text = "�ڵ�: " + currentItem.code.ToString();
            category.text = "�з�: " + currentItem.category;
        }
    }
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Sprite itemImage;
    public int[] currencyAmounts;
    public Sprite[] currencyImages;
    public string description;
    public int code;
    public int currentLv;
    public int max;
    public string category;

    public ItemData(string name, Sprite image, int[] amounts, Sprite[] images, string desc, int cd, int currnetLv, int maxLv, string cat)
    {
        itemName = name;
        itemImage = image;
        currencyAmounts = amounts;
        currencyImages = images;
        description = desc;
        code = cd;
        currentLv = currnetLv;
        max = maxLv;
        category = cat;
    }
}