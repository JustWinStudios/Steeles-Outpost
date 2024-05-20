using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceNode))]
public class ResourceNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ResourceNode resourceNode = (ResourceNode)target;

#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            InventoryManager.Instance = FindObjectOfType<InventoryManager>();
        }
#endif

        if (InventoryManager.Instance != null && InventoryManager.Instance.resources.Count > 0)
        {
            List<string> resourceNames = new List<string>();
            foreach (var resource in InventoryManager.Instance.resources)
            {
                resourceNames.Add(resource.resourceName);
            }

            resourceNode.resourceIndex = EditorGUILayout.Popup("Resource", resourceNode.resourceIndex, resourceNames.ToArray());
        }
        else
        {
            EditorGUILayout.LabelField("No resources available. Please add resources to the InventoryManager.");
        }

        resourceNode.resourceAmount = EditorGUILayout.IntField("Resource Amount", resourceNode.resourceAmount);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(resourceNode);
        }
    }
}
