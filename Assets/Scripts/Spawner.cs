using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList;
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
        GameObject prefab = prefabList[UnityEngine.Random.Range(0, prefabList.Count)];
        Instantiate(prefab, spawnPoint, prefab.transform.rotation);
    }
}
