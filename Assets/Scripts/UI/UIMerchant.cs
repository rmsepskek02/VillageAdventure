using System.Collections;
using System.Collections.Generic;
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
    int warriorAttackLevel = 1;
    int playerMovespeedLevel = 1;
    private BoWarrior boWarrior;
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
        InitializeItemData();
    }

    void InitializeItemData()
    {
        foreach (Transform child in mdVertical.transform)
        {
            Destroy(child.gameObject);
        }
        // ���� ���, ��������Ʈ�� "Assets/Resources/Model/Pixelole's overword Tileset1.0 free/MasterSimple" ��ο� �ִٰ� �����մϴ�.
        
        ItemData[] itemData = new ItemData[]
        {
            // 1000 ���� - Player ����
            // 2000 ���� - Warrior ����
            // 3000 ���� - ��Ÿ ����
            new ItemData("Upgrade Player Speed", null, new int[] {6, 6, 6}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"The speed of the player's movement increases. <color=red>Lv.{playerMovespeedLevel}</color> (Max <color=blue>5</color>)", 1001, 5,"Skill"),
            new ItemData("Buy Warrior", warriorSprite, new int[] {20, 20, 20}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"Hire 1 Warrior. (Max <color=blue>5</color>)", 2001, 3,"Warrior"),
            new ItemData("Upgrade Warrior Attack", null, new int[] {10, 10, 10}, new Sprite[] {mineSprite, treeSprite, fishSprite}, $"Warrior's attack increases. <color=red>Lv.{warriorAttackLevel}</color> (Max <color=blue>5</color>)", 2002, 5,"Skill"),
            new ItemData("Strength Potion", null, new int[] {}, null, "���ݷ��� �Ͻ������� ��ȭ�մϴ�. ������ ���� ��ȭ���� �����մϴ�.", 1002, 5, "����"),
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
    // image�� sprite ũ�Ⱑ ����?????
    // ��ư �̺�Ʈ �� ���� ����
    // �ش� ��ȭ�� SAVE �Ҷ��� ���� �����ؾ���
    // max�� ���޽� ��Ȱ��ȭ? �ƴϸ� ����?
    // �ִ뷹���� ���緹�� ���� data ����
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

        CheckAndDisableButton(item1, itemButton1, playerMovespeedLevel);
        CheckAndDisableButton(item2, itemButton2, warriorAttackLevel);
    }
    private void OnItemClick(ItemData item)
    {
        Debug.Log("Clicked item: " + item.itemName);

        // ��ȭ �˻�
        if (!CheckResource(item))
        {
            return;
        }

        // �ش� ��ư�� ȿ�� ����
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
                break;
            case 1003:
                break;
            case 2001:
                break;
            case 2002:
                Warrior[] warriors = FindObjectsOfType<Warrior>();
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
        // ��ȭ �Ҹ�
        ConsumeResource(item);
        // UI �ٽ� �׸���
        InitializeItemData();
    }

    // ��ȭ �˻��ϱ�
    private bool CheckResource(ItemData item)
    {
        int requiredMine = 0;
        int requiredTree = 0;
        int requiredFish = 0;
        int requiredFood = 0;

        // �䱸�Ǵ� �ڿ��� ���� �����մϴ�.
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

        // �ڿ��� ������� Ȯ���ϰ� ������ ��쿡 ���� �α׸� ����մϴ�.
        if (requiredMine > 0 && requiredMine > InGameManager.Instance.mine)
        {
            Debug.Log("Not enough mine.");
            return false;
        }

        if (requiredTree > 0 && requiredTree > InGameManager.Instance.tree)
        {
            Debug.Log("Not enough tree.");
            return false;
        }

        if (requiredFish > 0 && requiredFish > InGameManager.Instance.fish)
        {
            Debug.Log("Not enough fish.");
            return false;
        }

        if (requiredFood > 0 && requiredFood > InGameManager.Instance.food)
        {
            Debug.Log("Not enough food.");
            return false;
        }

        return true;
    }

    // ��ȭ �Ҹ��ϱ�
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

    // �ִ� ���� Ȯ���ϱ�
    private void CheckAndDisableButton(ItemData item, Button button, int level)
    {
        if (item.code == 1001 && level >= item.max)
        {
            button.interactable = false;
        }
    }
}
