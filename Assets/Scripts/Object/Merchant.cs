using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageAdventure;
using VillageAdventure.Enum;

public class Merchant : MonoBehaviour
{
    private static Merchant instance;

    // 인스턴스가 이미 있는지 확인하고, 있다면 새로 생성된 인스턴스를 파괴합니다.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //CheckActive();
    }

    // Scene & Time 따라 상인 활성화 여부 결정
    private void CheckActive()
    {
        if (GameManager.Instance.currentScene == SceneType.Forest)
        {
            if (InGameManager.Instance.min > 5 * 60 && InGameManager.Instance.min < 15 * 60)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else if (GameManager.Instance.currentScene == SceneType.Mine)
        {
            if (InGameManager.Instance.min > 25 * 60 && InGameManager.Instance.min < 35 * 60)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else if (GameManager.Instance.currentScene == SceneType.FishingZone)
        {
            if (InGameManager.Instance.min > 45 * 60 && InGameManager.Instance.min < 55 * 60)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else if (GameManager.Instance.currentScene == SceneType.Field || GameManager.Instance.currentScene == SceneType.House)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
