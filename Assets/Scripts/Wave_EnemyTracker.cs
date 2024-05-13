using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_EnemyTracker : MonoBehaviour
{
    // put this on your enemy prefabs. You could just copy the on destroy onto a pre-existing script if you want.
    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Spawn_Wave") != null)
        {
            GameObject.FindGameObjectWithTag("Spawn_Wave").GetComponent<Spawn_Wave>().spawnedEnemies.Remove(gameObject);
        }

    }
}