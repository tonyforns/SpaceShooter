using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : Singleton<ShopSystem>
{
    [SerializeField] private List<PowerUpBuyItem> powerUpList;
    [SerializeField] private GameObject shopGameObject;

    
    private void Start()
    {
        CloseShop();
        foreach (PowerUpBuyItem item in powerUpList)
        {
           item.RegisterOnPowerUpBought(UpdateShop);
        }
        ScoreSystem.Instance.OnScoreUpdated += UpdateShop;
    }

    private void UpdateShop()
    {
        //if (!shopGameObject.activeSelf) return;
        foreach (PowerUpBuyItem item in powerUpList)
        {
            if (item) item.SetActive(item.GetPowerUpData().cost <= ScoreSystem.Instance.CurrentScore);
        }
    }
    private void UpdateShop(int obj)
    {
        UpdateShop();
    }

    public void OpenShop()
    {
        UpdateShop();
        shopGameObject.SetActive(true);
    }

    public void CloseShop()
    {
        shopGameObject.SetActive(false);
        UpdateShop();
    }

    public bool IsShopOpen()
    {
        return shopGameObject.activeSelf;
    }
}
