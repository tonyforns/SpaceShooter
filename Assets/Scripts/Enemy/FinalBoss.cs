using System;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] private GameObject finalBossUI;
    bool isDestroyed = false;

    private void Start()
    {
        finalBossUI.SetActive(false);
        ScoreSystem.Instance.OnAllTurretsDestroyed += DestroyShip;
    }


    private void DestroyShip()
    {
        if (isDestroyed)
        {
            finalBossUI.SetActive(true);
        }
        else
        {
            isDestroyed = true;
        }
    }
}
