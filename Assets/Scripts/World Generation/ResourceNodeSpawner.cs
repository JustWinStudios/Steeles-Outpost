using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
    #region Editor Data
    [Header("Spawner Settings")]
    [SerializeField] private List<ResourceNodeEntry> resourceNodes;
    [SerializeField] private int maxResourceNodes = 50;
    [SerializeField] private float spawnInterval = 300f; // Time in seconds

    [Header("Spawn Area Settings")]
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    private float nextSpawnTime;
    private int totalSpawnProbability;

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // Calculate total spawn probability
        CalculateTotalSpawnProbability();

        // Initial Spawn
        SpawnResourceNodes();

        // Set the timer for the next spawn interval
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnResourceNodes();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
    #endregion

    #region Resource Spawning Logic
    private void CalculateTotalSpawnProbability()
    {
        totalSpawnProbability = 0;
        foreach (ResourceNodeEntry entry in resourceNodes)
        {
            totalSpawnProbability += entry.spawnProbability;
        }
    }

    private void SpawnResourceNodes()
    {
        int currentResourceNodes = GameObject.FindGameObjectsWithTag("ResourceNode").Length;
        int nodesToSpawn = maxResourceNodes - currentResourceNodes;

        for (int i = 0; i < nodesToSpawn; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject resourceToSpawn = SelectResource();
            if (resourceToSpawn != null)
            {
                Instantiate(resourceToSpawn, spawnPosition, Quaternion.identity);
                Debug.Log("Spawned Successfully!");
            }
        }
    }

    private GameObject SelectResource()
    {
        float randomPoint = Random.value * totalSpawnProbability;
        float cumulativeProbability = 0;

        foreach (ResourceNodeEntry entry in resourceNodes)
        {
            cumulativeProbability += entry.spawnProbability;
            if (randomPoint <= cumulativeProbability)
            {
                return entry.prefab;
            }
        }

        return null; // This should never happen if spawnProbabilities are set correctly
    }
    #endregion

    #region Spawn Area Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0), new Vector3(spawnAreaMin.x, spawnAreaMax.y, 0));
        Gizmos.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMax.y, 0), new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0));
        Gizmos.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0), new Vector3(spawnAreaMax.x, spawnAreaMin.y, 0));
        Gizmos.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMin.y, 0), new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0));
    }
    #endregion
}
