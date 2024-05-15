using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    public List<Collider2D> interactableObjects = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ResourceNode")) 
        {
            interactableObjects.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ResourceNode")) 
        {
            interactableObjects.Remove(other);
        }
    }

    public void RemoveDestroyedObjects()
    {
        interactableObjects.RemoveAll(item => item == null);
    }
}
