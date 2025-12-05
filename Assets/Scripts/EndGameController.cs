using System;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private Animator  animator;
    private void Start()
    {
        ScoreSystem.Instance.OnAllTurretsDestroyed += HandleEndGame;
    }

    private void HandleEndGame()
    {
        animator.SetTrigger("NextAnim");
    }
}
