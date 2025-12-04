using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private List<PowerUpBuyItem> powerUpList;
    [SerializeField] private GameObject shopGameObject;

    
    private void Start()
    {
        CloseShop();
        ScoreSystem.Instance.OnScoreUpdated += UpdateShop;
    }

    private void UpdateShop(int obj)
    {
        if (!gameObject.activeSelf) return;
        foreach (PowerUpBuyItem item in powerUpList)
        {
            if (item) item.SetActive(item.GetPowerUpData().cost <= ScoreSystem.Instance.CurrentScore);
        }
    }

    public void OpenShop()
    {
        Time.timeScale = 0;
        foreach (PowerUpBuyItem item in powerUpList) { 

            if (item) item.SetActive(item.GetPowerUpData().cost <= ScoreSystem.Instance.CurrentScore);
        }
        shopGameObject.SetActive(true);
    }

    public void CloseShop()
    {
        Time.timeScale = 1;
        shopGameObject.SetActive(false);
    }
}
