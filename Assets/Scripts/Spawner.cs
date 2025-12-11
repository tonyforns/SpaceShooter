using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList;
    [SerializeField] private Transform topPosition;
    [SerializeField] private Transform bottomPosition;

    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;
    [SerializeField] private float difficultyIncreaseRate = 30f;
    
    private float currentSpawnInterval;
    private float timer;
    private float gameTime = 0f;

    private void Awake()
    {
        currentSpawnInterval = initialSpawnInterval;
        timer = currentSpawnInterval;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        UpdateDifficulty();
        
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = currentSpawnInterval;
        }
    }

    private void UpdateDifficulty()
    {
        float progress = gameTime / difficultyIncreaseRate;
        progress = Mathf.Clamp01(progress); // Mantener entre 0 y 1
        
        float curveProgress = 1f - Mathf.Pow(1f - progress, 2f); // Curva acelerada
        
        currentSpawnInterval = Mathf.Lerp(initialSpawnInterval, minimumSpawnInterval, curveProgress);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = Vector3.Lerp(topPosition.position, bottomPosition.position, UnityEngine.Random.value);
        GameObject prefab = prefabList[UnityEngine.Random.Range(0, prefabList.Count)];
        Instantiate(prefab, spawnPoint, prefab.transform.rotation);
    }

    public float GetCurrentSpawnInterval()
    {
        return currentSpawnInterval;
    }

    public float GetDifficultyProgress()
    {
        return Mathf.Clamp01(gameTime / difficultyIncreaseRate);
    }

    public void ResetDifficulty()
    {
        gameTime = 0f;
        currentSpawnInterval = initialSpawnInterval;
        timer = currentSpawnInterval;
    }
}
