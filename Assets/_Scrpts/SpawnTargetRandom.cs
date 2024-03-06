using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargetRandom : MonoBehaviour
{
    public GameObject targetObject;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 20f;

    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = randomDirection * randomDistance;

        Instantiate(targetObject, spawnPosition, Quaternion.identity);
    }
}
