using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    public List<Collider2D> interactableObjects = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactableObjects.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactableObjects.Remove(other);
        }
    }
}