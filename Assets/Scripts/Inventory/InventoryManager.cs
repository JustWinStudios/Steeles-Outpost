using System.Collections.Generic;
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
    #endregion

    #region Inventory Management Logic
    public void AddResource(string resourceName, int amount)
    {
        
    }
}
