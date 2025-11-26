using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    public Action OnDeath;
    private int baseLife = 5;
    private int currentLife;

    private void Awake()
    {
        ResetLife();
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void ResetLife()
    {
        currentLife = baseLife;
    }
    public void Heal(int healHits)
    {
        currentLife += healHits;
        if(currentLife > baseLife)
        {
            currentLife = baseLife;
        }
    }

}
