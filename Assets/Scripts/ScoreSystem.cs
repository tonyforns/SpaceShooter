using System;
using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    public Action<int> OnScoreUpdated;

    [SerializeField] private int score = 0;

    private new void Awake()
    {
        base.Awake();

        ResetScore();
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
}
