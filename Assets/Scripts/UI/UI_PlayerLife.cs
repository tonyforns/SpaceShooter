using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerLife : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;

    private void Start()
    {
        Player.Instance.OnLifeChanged += UpdateLifeUI;

    }
    private void UpdateLifeUI(float lifeNormalize)
    {
        lifeSlider.value = lifeNormalize;
        lifeSlider.gameObject.SetActive(lifeSlider.value > 0);
    }
}
