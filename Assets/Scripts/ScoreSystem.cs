using System;
using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    public Action<int> OnScoreUpdated;
    public Action<int> OnTurretDestroyed;
    public Action OnAllTurretsDestroyed;

    [SerializeField] private int score = 0;
    [SerializeField] private int turretKilledCount;

    public int CurrentScore { get => score; }

    private new void Awake()
    {
        base.Awake();

        //ResetScore();
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
        OnTurretDestroyed?.Invoke(turretKilledCount);
        if (turretKilledCount == 6)
        {
            OnAllTurretsDestroyed?.Invoke();
        }
    }

    internal void DecreaseScore(int cost)
    {
        score -= cost;
        OnScoreUpdated?.Invoke(score);
    }
}
