using UnityEngine;
using UnityEngine.UI;

public class Merchandise : MonoBehaviour
{
    public ItemData[] itemData; // 아이템 데이터 배열
    public Image itemImage;
    public Text itemName;
    public Text description;
    public Text code;
    public Text category;

    void Start()
    {
        // 예제를 위해 초기 아이템 데이터 설정
        InitializeItemData();
        // 초기 아이템 표시
        ShowItemDetails(0);
    }

    void InitializeItemData()
    {
        // 아이템 데이터 초기화
        itemData = new ItemData[]
        {
            new ItemData("Health Potion", null, new int[] {1, 2, 3}, null, "체력을 회복합니다. 레벨에 따라 회복량이 증가합니다.", 1001, 1, 5, "포션"),
            new ItemData("Strength Potion", null, new int[] {2, 3, 4}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, 1, 5, "포션"),
            new ItemData("Iron Sword", null, new int[] {3, 4, 5}, null, "철제 검으로 적을 공격합니다. 레벨에 따라 공격력이 증가합니다.", 2001, 1, 5, "무기")
        };
    }

    // 상점에서 아이템을 선택했을 때 호출되는 메서드
    public void OnItemClick(int index)
    {
        ShowItemDetails(index);
    }

    // 선택한 아이템의 세부 정보를 표시하는 메서드
    void ShowItemDetails(int index)
    {
        ItemData currentItem = itemData[index];
        if (currentItem != null)
        {
            itemImage.sprite = currentItem.itemImage;
            itemName.text = currentItem.itemName;
            description.text = currentItem.description.Replace("레벨", "현재 레벨");
            code.text = "코드: " + currentItem.code.ToString();
            category.text = "분류: " + currentItem.category;
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