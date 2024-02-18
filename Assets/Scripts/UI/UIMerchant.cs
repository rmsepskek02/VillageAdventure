using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VillageAdventure;

public class UIMerchant : MonoBehaviour
{
    public Button cancelButton;

    void Start()
    {
        cancelButton.onClick.AddListener(OnClickCancel);
    }

    void Update()
    {
        
    }

    private void OnClickCancel()
    {
        InGameManager.Instance.isMerchant = false;
        GameManager.Instance.ReStartGame();
    }
}
