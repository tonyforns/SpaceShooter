using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerLife : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Image playerLifeUI;
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
        if (lifeNormalize == 1) return;
        playerLifeUI.color = lifeNormalize <= previusLife ? Color.red : Color.green;
        playerLifeUI.gameObject.SetActive(false);
        playerLifeUI.gameObject.SetActive(true);

        previusLife = lifeNormalize;
        lifeSlider.value = lifeNormalize;
        lifeSlider.gameObject.SetActive(lifeSlider.value > 0);

    }
}
