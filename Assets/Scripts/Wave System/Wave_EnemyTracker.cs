using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_EnemyTracker : MonoBehaviour
{
    // Make sure you provide your Spawn Manager the "Spawn_Wave" Tag to make this work.
    // Put this on enemy prefabs.
    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Spawn_Wave") != null)
        {
            GameObject.FindGameObjectWithTag("Spawn_Wave").GetComponent<Spawn_Wave>().spawnedEnemies.Remove(gameObject);
        }

    }
}