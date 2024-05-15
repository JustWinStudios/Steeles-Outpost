using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IInteractable
{
    #region Editor Data
    [Header("Resource Settings")]
    [SerializeField] private int resourceAmount = 10;
    [SerializeField] private string resourceType = "Wood"; // Example resource type
    #endregion

    #region Resource Logic
    public void Interact()
    {
        CollectResource();
    }

    private void CollectResource()
    {
        Debug.Log($"Collected {resourceAmount} {resourceType}");
        // Implement resource collection logic here (e.g., add to player inventory)

        // Destroy the resource node after collection
        Destroy(gameObject);
        Debug.Log("Resource node destroyed.");
    }
    #endregion
}
