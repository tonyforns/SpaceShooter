using System;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI turretCountText;
    private int currentScore = 0;
    private int finalScore = 0;
    private float updateInterval = 0.25f;
    private float timer = 0f;
    private void Awake()
    {
        if(scoreText == null)
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }
    }
    private void Start()
    {
        ScoreSystem.Instance.OnScoreUpdated += UpdateScoreUI;
        ScoreSystem.Instance.OnTurretDestroyed += UpdateTurretCountUI;
    }
    private void UpdateTurretCountUI(int count)
    {
        turretCountText.text = $"{count}/6";
    }

    private void Update()
    {
        if(currentScore < finalScore)
        {
            currentScore = (int)Mathf.Lerp(currentScore , finalScore, timer / updateInterval);
            if (currentScore > finalScore)
            {
                currentScore = finalScore;
            }
            scoreText.text = $"{currentScore}";
            timer += Time.deltaTime;
        } 
    }
    private void UpdateScoreUI(int newScore)
    {
        timer = 0;
        finalScore = newScore;
        scoreText.text = $"{newScore}";
    }
}
