using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure;
using VillageAdventure.DB;
using VillageAdventure.Object;

public class UIMerchant : MonoBehaviour
{
    public Button cancelButton;
    public GameObject merchandisePrefab;
    public GameObject mdVertical;
    Sprite[] resourceSprites;
    Sprite[] warriorSprites;
    Sprite mineSprite;
    Sprite treeSprite;
    Sprite fishSprite;
    Sprite foodSprite;
    Sprite warriorSprite;
    public List<int> warriorIndexInLayer;
    GameObject nonePlayer;
    Transform warrior;

    public static int warriorAttackLevel = 1;
    public static int playerMovespeedLevel = 1;
    public static int miningLevel = 1;
    public static int loggingLevel = 1;
    public static int fishingLevel = 1;

    void Start()
    {
        cancelButton.onClick.AddListener(OnClickCancel);
        resourceSprites = Resources.LoadAll<Sprite>("Model/Pixelole's overword Tileset1.0 free/MasterSimple");
        warriorSprites = Resources.LoadAll<Sprite>("Model/Character with sword and shield/idle/idle down1");
        mineSprite = resourceSprites[70];
        treeSprite = resourceSprites[38];
        fishSprite = resourceSprites[94];
        foodSprite = resourceSprites[72];
        warriorSprite = warriorSprites[0];
        nonePlayer = GameObject.Find("NonePlayer");
        warrior = nonePlayer.transform.Find("Warrior").gameObject.transform;
        InitializeItemData();
    }

    public void InitializeItemData()
    {
        foreach (Transform child in mdVertical.transform)
        {
            Destroy(child.gameObject);
        }
        // 예를 들어, 스프라이트가 "Assets/Resources/Model/Pixelole's overword Tileset1.0 free/MasterSimple" 경로에 있다고 가정합니다.
        
        ItemData[] itemData = new ItemData[]
        {
            // 1000 번대 - Player 관련
            // 2000 번대 - Warrior 관련
            // 3000 번대 - 기타 관련
            new ItemData("Upgrade Player Speed", null, new int[] {6, 6, 6}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"The speed of the player's movement increases. <color=red>Lv.{playerMovespeedLevel}</color> (Max <color=blue>5</color>)", 1001, playerMovespeedLevel, 5,"Skill"),
            new ItemData("Upgrade Mining", null, new int[] {1}, new Sprite[] {mineSprite}, $"It increases the efficiency of mining <color=red>Lv.{miningLevel}</color> (Max <color=blue>5</color>)", 1002, miningLevel, 5, "Skill"),
            new ItemData("Upgrade logging", null, new int[] {1}, new Sprite[] {treeSprite}, $"It increases the efficiency of logging <color=red>Lv.{loggingLevel}</color> (Max <color=blue>5</color>)", 1003, loggingLevel, 5, "Skill"),
            new ItemData("Upgrade fishing", null, new int[] {1}, new Sprite[] {fishSprite}, $"It increases the efficiency of fishing <color=red>Lv.{fishingLevel}</color> (Max <color=blue>5</color>)", 1004, fishingLevel, 5, "Skill"),
            new ItemData("Buy Warrior", warriorSprite, new int[] {2, 2, 2}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"Hire 1 Warrior. (Max <color=blue>5</color>)", 2001, InGameManager.Instance.warriorCount, 3,"Warrior"), // 워리어 개수
            new ItemData("Upgrade Warrior Attack", null, new int[] {5, 5, 5}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"Warrior's attack increases. <color=red>Lv.{warriorAttackLevel}</color> (Max <color=blue>5</color>)", 2002, warriorAttackLevel, 5,"Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            new ItemData("Example", null, new int[] {10, 20, 30}, new Sprite[] {fishSprite, mineSprite, foodSprite}, $"<color=red>Example</color>", 9999, 1, 5, "Skill"),
            // 여기에 추가 아이템을 추가할 수 있습니다.
        };

        Debug.Log("CREATE MD");
        // 아이템 데이터를 프리팹에 표시합니다.
        for (int i = 0; i < itemData.Length; i += 2) // 2씩 증가하면서 두 개씩 묶어서 처리
        {
            CreateMD(itemData[i], itemData[i + 1]); // 두 개의 아이템 데이터를 전달
        }
    }

    void Update()
    {
        IndexWarriorWithLayer(warrior, "Warrior");
    }

    private void OnClickCancel()
    {
        InGameManager.Instance.isMerchant = false;
        GameManager.Instance.ReStartGame();
    }
    // image에 sprite 크기가 작음?????
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
        for (int i = 2; i > item1.currencyAmounts.Length - 1; i--)
        {
            newMDtrans1.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        newMDtrans2.GetChild(0).gameObject.GetComponent<Image>().sprite = item2.itemImage;
        newMDtrans2.GetChild(2).gameObject.GetComponent<Text>().text = item2.description;
        for (int i = 0; i < item2.currencyAmounts.Length; i++)
        {
            newMDtrans2.GetChild(1).gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = item2.currencyImages[i];
            newMDtrans2.GetChild(1).gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = $"{item2.currencyAmounts[i]}";
        }
        for (int i = 2; i > item2.currencyAmounts.Length - 1; i--)
        {
            newMDtrans2.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        Button itemButton1 = newMDtrans1.GetComponent<Button>();
        itemButton1.onClick.AddListener(() => OnItemClick(item1));

        Button itemButton2 = newMDtrans2.GetComponent<Button>();
        itemButton2.onClick.AddListener(() => OnItemClick(item2));

        CheckAndDisableButton(item1, itemButton1);
        CheckAndDisableButton(item2, itemButton2);
    }
    private void OnItemClick(ItemData item)
    {
        Debug.Log("Clicked item: " + item.itemName);

        // 재화 검사
        if (!CheckResource(item))
        {
            return;
        }

        // 해당 버튼의 효과 적용
        switch (item.code)
        {
            case 1001:
                Player player = FindObjectOfType<Player>();
                if (player != null && playerMovespeedLevel < 6)
                {
                    player.boPlayer.moveSpeed += 3;
                    playerMovespeedLevel++;
                }
                else
                {
                    Debug.LogError("Player instance not found!");
                }
                break;
            case 1002:
                miningLevel++;
                break;
            case 1003:
                loggingLevel++;
                break;
            case 1004:
                fishingLevel++;
                break;
            case 2001:
                GeneratorWarrior(warriorIndexInLayer);
                break;
            case 2002:
                Warrior[] warriors = Resources.FindObjectsOfTypeAll<Warrior>();
                foreach (Warrior warrior in warriors)
                {
                    if (warrior != null && warriorAttackLevel < 6)
                    {
                        warrior.boWarrior.power += 20;
                    }
                    else
                    {
                        Debug.LogError("Warrior instance not found!");
                    }
                }
                warriorAttackLevel++;
                break;
        }
        // 재화 소모
        ConsumeResource(item);
        // UI 다시 그리기
        InitializeItemData();
    }

    // 재화 검사하기
    private bool CheckResource(ItemData item)
    {
        int requiredMine = 0;
        int requiredTree = 0;
        int requiredFish = 0;
        int requiredFood = 0;

        // 요구되는 자원의 양을 설정합니다.
        for (int i = 0; i < item.currencyAmounts.Length; i++)
        {
            if (item.currencyImages[i] == mineSprite)
                requiredMine = item.currencyAmounts[i];
            else if (item.currencyImages[i] == treeSprite)
                requiredTree = item.currencyAmounts[i];
            else if (item.currencyImages[i] == fishSprite)
                requiredFish = item.currencyAmounts[i];
            else if (item.currencyImages[i] == foodSprite)
                requiredFood = item.currencyAmounts[i];
        }

        // 자원이 충분한지 확인하고 부족한 경우에 대해 로그를 출력합니다.
        if (requiredMine > 0 && requiredMine > InGameManager.Instance.mine)
        {
            InGameManager.Instance.SetGuideUI("Not enough mine.", true);
            return false;
        }

        if (requiredTree > 0 && requiredTree > InGameManager.Instance.tree)
        {
            InGameManager.Instance.SetGuideUI("Not enough tree.", true);
            return false;
        }

        if (requiredFish > 0 && requiredFish > InGameManager.Instance.fish)
        {
            InGameManager.Instance.SetGuideUI("Not enough fish.", true);
            return false;
        }

        if (requiredFood > 0 && requiredFood > InGameManager.Instance.food)
        {
            InGameManager.Instance.SetGuideUI("Not enough food.", true);
            return false;
        }

        return true;
    }

    // 재화 소모하기
    private void ConsumeResource(ItemData item)
    {
        for (int i = 0; i < item.currencyAmounts.Length; i++)
        {
            if (item.currencyImages[i] == mineSprite)
            {
                InGameManager.Instance.mine -= item.currencyAmounts[i];
            }
            else if (item.currencyImages[i] == treeSprite)
            {
                InGameManager.Instance.tree -= item.currencyAmounts[i];
            }
            else if (item.currencyImages[i] == fishSprite)
            {
                InGameManager.Instance.fish -= item.currencyAmounts[i];
            }
            else if (item.currencyImages[i] == foodSprite)
            {
                InGameManager.Instance.food -= item.currencyAmounts[i];
            }
        }
    }

    // 최대 레벨 확인하기
    private void CheckAndDisableButton(ItemData item, Button button)
    {
        if (item.currentLv >= item.max)
        {
            button.interactable = false;
        }
    }

    // Warrior 생성
    private void GeneratorWarrior(List<int> warriorList)
    {
        var sdWarrior = GameManager.SD.sdNonePlayer.Where(_ => _.index == 7000).SingleOrDefault();
        var _warrior = Instantiate(Resources.Load<GameObject>(sdWarrior.resourcePath)).GetComponent<Warrior>();
        _warrior.Initialize(new BoWarrior(sdWarrior));
        _warrior.transform.position = Vector2.zero;
        _warrior.transform.SetParent(warrior);
        Destroy(warrior.GetChild(warriorList[0]).gameObject);
        _warrior.transform.SetSiblingIndex(warriorList[0]);
        InGameManager.Instance.charactors.Add(_warrior);
        InGameManager.Instance.warriorCount++;
    }

    // Warrior 관리
    void IndexWarriorWithLayer(Transform tr, string layerName)
    {
        for (int i = 0; i < tr.childCount; i++)
        {
            Transform child = tr.GetChild(i);
            if (child.gameObject.layer != LayerMask.NameToLayer(layerName))
            {
                if (!warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                    warriorIndexInLayer.Add(child.GetSiblingIndex());
            }
            else
            {
                if (warriorIndexInLayer.Contains(child.GetSiblingIndex()))
                    warriorIndexInLayer.Remove(child.GetSiblingIndex());
            }
        }
        warriorIndexInLayer.Sort();
    }
}
