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
        // 예를 들어, 스프라이트가 "Assets/Resources/Model/Pixelole's overword Tileset1.0 free/MasterSimple" 경로에 있다고 가정합니다.
        Sprite[] resourceSprites = Resources.LoadAll<Sprite>("Model/Pixelole's overword Tileset1.0 free/MasterSimple");
        Sprite[] warriorSprites = Resources.LoadAll<Sprite>("Model/Character with sword and shield/idle/idle down1");
        Debug.Log("Test == " + resourceSprites.Length);
        Sprite mineSprite = resourceSprites[70];
        Sprite treeSprite = resourceSprites[38];
        Sprite fishSprite = resourceSprites[94];
        Sprite warriorSprite = warriorSprites[0];
        ItemData[] itemData = new ItemData[]
        {
            new ItemData("Health Potion", null, new int[] {1, 2, 3}, new Sprite[] {mineSprite, treeSprite, fishSprite}, "체력을 회복합니다. 레벨에 따라 회복량이 증가합니다.", 1001, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Strength Potion", null, new int[] {}, null, "공격력을 일시적으로 강화합니다. 레벨에 따라 강화량이 증가합니다.", 1002, "포션"),
            new ItemData("Iron Sword", warriorSprite, new int[] {}, null, "철제 검으로 적을 공격합니다. 레벨에 따라 공격력이 증가합니다.", 2001, "무기")
            // 여기에 추가 아이템을 추가할 수 있습니다.
        };

        // 아이템 데이터를 프리팹에 표시합니다.
        for (int i = 0; i < itemData.Length; i += 2) // 2씩 증가하면서 두 개씩 묶어서 처리
        {
            CreateMD(itemData[i], itemData[i + 1]); // 두 개의 아이템 데이터를 전달
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
    // 배열 외 오브젝트 삭제
    // image에 sprite 크기가 작음
    // 버튼 이벤트 구현
    // description에 int 변수를 포함하여 lv.1 > 2 > 3 > 4 와 같이 상위단계로 올라가도록 구현
    private void CreateMD(ItemData item1, ItemData item2)
    {
        // 프리팹 생성
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
