using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
    #region Editor Data
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] resourcePrefabs;
    [SerializeField] private int maxResourceNodes = 50;
    [SerializeField] private float spawnInterval = 300f; //Time in seconds

    [Header("Spawn Area Settings")]
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    private float nextSpawnTime;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        //Initial Spawn
        SpawnResourceNodes();

        //Set the timer for the next spawn interval
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnResourceNodes();
            nextSpawnTime = Time.time +spawnInterval;
        }
    }
    #endregion

    #region Resource Spawning Logic
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

            int randomIndex = Random.Range(0, resourcePrefabs.Length);
            Instantiate(resourcePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
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
