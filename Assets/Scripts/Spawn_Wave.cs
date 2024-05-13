using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Wave : MonoBehaviour
{
    [Header("Enemy Types and Costs")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("Wave Counter and Rate")]
    [Tooltip("Current Wave Number")]
    public int currentWave;
    [Tooltip("Wave's Enemy Spawning Power")]
    private int waveValue;
    [Tooltip("The amount of increase in Spawning Power per round. It takes the current wave number and multiplies it by the rate of increase to decide what the next wave's spawning power is.")]
    [SerializeField] private int rateOfIncrease;

    [Header("Enemies Being Spawned This Wave")]
    [Tooltip("Amount and Type of enemies the game will spawn for this current round. DON'T TOUCH")]
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    [Header("Spawn Location and Spawn Interval")]
    public Transform[] spawnLocation;
    [Tooltip("When the scene contains multiple spawn locations, this spawnIndex decides which location to spawn in. DON'T TOUCH!")]
    public int spawnIndex;

    [Tooltip("The given duration that the wave is allowed to spawn enemies in. If set to twenty seconds, it will spawn enemies equally across 20 seconds")]
    [SerializeField] private int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;

                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currentWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currentWave * rateOfIncrease;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }

    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.

        // repeat... 

        //  -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}