using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpBuyItem : MonoBehaviour
{
    [Serializable]
    public struct PowerUpData
    {
        public string powerUpName;
        public int cost;
        public Sprite icon;
        public IConsumible powerUp;
    }

    [SerializeField] private IConsumible powerUp;
    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private TextMeshProUGUI powerupName;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image iconRenderer;
    [SerializeField] private Button buyButton;

    private Action OnPowerUpBought;
    private void Awake()
    {
        if(buyButton is null) buyButton = GetComponent<Button>();

        powerUp = GetComponent<IConsumible>();
        powerupName.text = powerUpData.powerUpName;
        cost.text = powerUpData.cost.ToString();
        iconRenderer.sprite = powerUpData.icon;
    }

    public void RegisterOnPowerUpBought(Action action)
    {
        OnPowerUpBought += action;
    }   

    public bool TryToBuyPowerUp(int cost)
    {
        if (ScoreSystem.Instance.CurrentScore >= cost)
        {
            ScoreSystem.Instance.DecreaseScore(cost);
            return true;
        }
        return false;
    }

    public void BuyPowerUp(Player player)
    {
        if (TryToBuyPowerUp(powerUpData.cost))
        {
            OnPowerUpBought?.Invoke();
            powerUp.Consume(player, false);
        }
    }
    public PowerUpData GetPowerUpData()
    {
        return powerUpData;
    }

    public void SetActive(bool setActive)
    {
        buyButton.enabled = setActive;
        buyButton.image.color = setActive ? Color.white : Color.gray;
        powerupName.color = setActive ? Color.white : Color.gray;
        iconRenderer.color = setActive ? Color.white : Color.gray;
    }
}
