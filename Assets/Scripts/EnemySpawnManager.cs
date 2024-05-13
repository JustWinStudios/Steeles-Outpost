using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnOffset = 1f;
    public int initialEnemiesPerWave = 5;
    private int enemiesPerWave;
    public int maxEnemiesPerWave = 50;
    private int currentWave = 1;
    private int activeEnemies = 0; // Track the number of active enemies

    void Start()
    {
        enemiesPerWave = initialEnemiesPerWave;
        StartCoroutine(StartWave());
    }

    void Update()
    {
        // Check if all enemies are defeated to start the next wave
        if (activeEnemies <= 0)
        {
            StartCoroutine(StartWave()); // Use StartCoroutine to call the StartWave coroutine
        }
    }

    IEnumerator StartWave() // Changed to a coroutine to handle time delays
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemyAtRandomPoint();
            activeEnemies++; // Increment active enemy count
            // Wait for a random interval between spawns
            yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // Random delay between 0.5 to 2 seconds
        }

        enemiesPerWave += Random.Range(2, 6);
        enemiesPerWave = Mathf.Min(enemiesPerWave, maxEnemiesPerWave);
        currentWave++;
    }

    void SpawnEnemyAtRandomPoint()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float cameraSize = Mathf.Max(Camera.main.orthographicSize, Camera.main.aspect * Camera.main.orthographicSize);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPoint = playerPosition + randomDirection * (cameraSize + spawnOffset);

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        enemy.GetComponent<enemyManager>().OnEnemyDeath += HandleEnemyDeath; // Subscribe to the enemy's death event
    }

    void HandleEnemyDeath()
    {
        activeEnemies--; // Decrement active enemy count when an enemy is defeated
    }
}
