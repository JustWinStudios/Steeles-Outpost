using System.Collections.Generic;
using UnityEngine;

#region Unity Data
[System.Serializable]
public class Resource 
{
    public string resourceName;
    public Sprite icon;
    public int amount;
    public List<CraftableItem> craftableItems;
}
#endregion

#region Craftable Item Information
[System.Serializable]
public class CraftableItem
{
    public string itemName;
    public List<ResourceRequirement> requirements;
}
#endregion

#region Resource Requirements
[System.Serializable]
public class ResourceRequirement
{
    public string resourceName;
    public int amount;
}
#endregion