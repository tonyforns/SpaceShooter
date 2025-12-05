using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    public Action OnLifeChange;
    [SerializeField] private int baseLife = 1;
    [SerializeField] private int currentLife;

    public Action OnDeath;

    private void Awake()
    {
        ResetLife();
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        OnLifeChange?.Invoke();
        if (currentLife <= 0)
        {
            currentLife = 0;
            OnDeath?.Invoke();
        }
    }

    public void ResetLife()
    {
        currentLife = baseLife;
        OnLifeChange?.Invoke();

    }
    public void Heal(int healHits)
    {
        currentLife += healHits;
        if(currentLife > baseLife)
        {
            currentLife = baseLife;
        }
        OnLifeChange?.Invoke();
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public float GetLifeNormalize()
    {
        if(currentLife < 0) return 0f;
        return (float)currentLife / baseLife;
    }

    public void IncreaseBaseLife(int increaseAmount)
    {
        baseLife += increaseAmount;
        currentLife += increaseAmount;
        OnLifeChange?.Invoke();
    }
}
