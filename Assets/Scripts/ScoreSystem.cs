using System;
using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    public Action<int> OnScoreUpdated;
    public Action<int> OnTurretDestroyed;
    public Action OnAllTurretsDestroyed;

    [SerializeField] private int score = 0;
    [SerializeField] private int totalTurrets = 6;
    [SerializeField] private int turretKilledCount;

    public int CurrentScore { get => score; }

    private new void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        OnScoreUpdated?.Invoke(score);

    }
    public void AddScore(int points)
    {
        score += points;
        OnScoreUpdated?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreUpdated?.Invoke(score);
    }

    public void TurretKilled()
    {
        turretKilledCount++;
        if (turretKilledCount == totalTurrets)
        {
            OnAllTurretsDestroyed?.Invoke();
            if(totalTurrets != 12) turretKilledCount = 0;
            totalTurrets = 12;
        }
        OnTurretDestroyed?.Invoke(turretKilledCount);

    }

    public int GetTotalTurrerCount()
    {
        return totalTurrets;
    }

    internal void DecreaseScore(int cost)
    {
        score -= cost;
        OnScoreUpdated?.Invoke(score);
    }
}
