using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    #region Editor Data
    [Header("Resource Settings")]
    [Tooltip("This is the amount the resource gives you when interacted with")]
    [SerializeField] private int resourceAmount = 10;
    [Tooltip("Change this to the name of the game object being collected/interacted with")]
    [SerializeField] private string resourceType = "NULL";
    #endregion

    #region Resource Collection Logic

    public void Interact()
    {
        CollectResource();
    }

    private void CollectResource()
    {
        Debug.Log($"Collected {resourceAmount} {resourceType}");

        //Implement Resource Collection Logic here

        //Destroy resource game object after collection
        Destroy(gameObject);
    }
    #endregion
}
