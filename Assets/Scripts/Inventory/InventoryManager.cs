using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Unity Data
    public static InventoryManager Instance;

    [Header("Resources")]
    public List<Resource> resources;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        Instance = FindObjectOfType<InventoryManager>();
        if (Instance == null)
        {
            GameObject inventoryManager = new GameObject("InventoryManager");
            Instance = inventoryManager.AddComponent<InventoryManager>();
        }
    }
    #endif
    #endregion

    #region Inventory Management Logic
    public void AddResource(string resourceName, int amount)
    {
        Resource resource = resources.Find(r  => r.resourceName == resourceName);

        if (resource != null)
        {
            resource.amount += amount;
        }

        else
        {
            Debug.LogWarning($"Resource {resourceName} not found in inventory!");
        }
    }

    public void RemoveResource(string resourceName, int amount)
    {
        Resource resource = resources.Find(r => r.resourceName == resourceName);
        if (resource != null)
        {
            resource.amount = Mathf.Max(0, resource.amount -  amount);
        }

        else
        {
            Debug.LogWarning($"Resource {resourceName} not foudn in inventory!");
        }
    }

    public int GetResourceAmount(string resourceName)
    {
        Resource resource = resources.Find(r => r.resourceName == resourceName);
        return resource != null ? resource.amount : 0;
    }

    public bool CanCraftItem(string itemName)
    {
        foreach (Resource resource in resources)
        {
            foreach (CraftableItem item in resource.craftableItems)
            {
                if (item.itemName == itemName)
                {
                    foreach (ResourceRequirement req in item.requirements)
                    {
                        if (GetResourceAmount(req.resourceName) < req.amount)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }

    public void CraftItem(string itemName)
    {
        if (CanCraftItem(itemName))
        {
            foreach (Resource resource in resources)
            {
                foreach (CraftableItem item in resource.craftableItems)
                {
                    if (item.itemName == itemName)
                    {
                        foreach (ResourceRequirement req in item.requirements)
                        {
                            RemoveResource(req.resourceName, req.amount);
                        }

                        Debug.Log($"Crafted {itemName}");
                        return;
                    }
                }
            }
        }

        else
        {
            Debug.LogWarning($"Cannot craft {itemName}. Insufficient resources.");
        }
    }
    #endregion
}
