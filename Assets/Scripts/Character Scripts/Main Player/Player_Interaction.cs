using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    #region Editor Data
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactableLayer;

    [Header("Dependencies")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private InteractionRange interactionRange;

    private Camera mainCamera;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        // Ensure the main camera is assigned:
        mainCamera = Camera.main;

        // Ensure the interaction point is assigned:
        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }

        // Ensure the interaction range is assigned:
        if (interactionRange == null)
        {
            interactionRange = interactionPoint.GetComponent<InteractionRange>();
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the interaction range in the editor for visualization
        if (interactionPoint == null) return;
        CircleCollider2D collider = interactionPoint.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactionPoint.position, collider.radius);
        }
    }
    #endregion

    #region Interaction Logic
    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for interaction
        {
            foreach (var collider in interactionRange.interactableObjects)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break; // Interact with the first valid interactable object found
                }
            }
            interactionRange.RemoveDestroyedObjects(); // Clean up the list
        }
    }
    #endregion
}
