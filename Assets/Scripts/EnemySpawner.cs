using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform topPosition;
    [SerializeField] private Transform bottomPosition;

    [SerializeField] private float spawnInterval = 2f;
    private float timer;

    private void Awake()
    {
        timer = spawnInterval;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
       Vector3 spawnPoint = Vector3.Lerp(topPosition.position, bottomPosition.position, UnityEngine.Random.value);
       Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation);
    }
}
