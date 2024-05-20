using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IInteractable
{
    #region Editor Data
    [Header("Resource Settings")]
    [SerializeField] private int resourceIndex; // Index to select the resource from InventoryManager
    [SerializeField] private int resourceAmount = 10;
    #endregion

    #region Resource Logic
    public void Interact()
    {
        CollectResource();
    }

    private void CollectResource()
    {
        if (InventoryManager.Instance.resources.Count > resourceIndex)
        {
            string resourceName = InventoryManager.Instance.resources[resourceIndex].resourceName;
            Debug.Log($"Collected {resourceAmount} {resourceName}");
            InventoryManager.Instance.AddResource(resourceName, resourceAmount);

            // Destroy the resource node after collection
            Destroy(gameObject);
            Debug.Log("Resource node destroyed.");
        }
        else
        {
            Debug.LogWarning("Invalid resource index.");
        }
    }
    #endregion
}
