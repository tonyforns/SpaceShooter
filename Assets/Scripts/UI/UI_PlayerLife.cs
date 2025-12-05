using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerLife : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private GameObject playerLifeUI;
    [SerializeField] private GameObject restartMessage;
    private float previusLife = 1;
    private bool isShieldUp = false;

    private void Start()
    {
        Player.Instance.OnLifeChanged += UpdateLifeUI;
        Player.Instance.OnShieldChanged += SetShieldStatus;
        Player.Instance.OnDeath += ShowGameOverUI;

        shieldSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isShieldUp)
        {
            shieldSlider.value = Player.Instance.GetShieldTimerNormalize();
        }
    }

    private void ShowGameOverUI()
    {
        restartMessage.SetActive(true);
    }

    private void SetShieldStatus(bool isShieldUp)
    {
        this.isShieldUp = !isShieldUp;
        shieldSlider.gameObject.SetActive(!isShieldUp);
    }
    private void UpdateLifeUI(float lifeNormalize)
    {
        if (lifeNormalize < previusLife)
        {
            playerLifeUI.SetActive(false);
            playerLifeUI.SetActive(true);
        }

        lifeSlider.value = lifeNormalize;
        lifeSlider.gameObject.SetActive(lifeSlider.value > 0);

    }
}
