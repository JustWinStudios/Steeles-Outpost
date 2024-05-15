using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    #region Editor Data
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 2.0f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Dependencies")]
    [SerializeField] private Transform interactionPoint;

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
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the interaction range in the editor for visualization
        if (interactionPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }
    #endregion

    #region Interaction Logic
    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for interaction
        {
            // Convert mouse position to a ray
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // Perform raycast from the interaction point in the direction of the ray
            RaycastHit2D hit = Physics2D.Raycast(interactionPoint.position, ray.direction, interactionRange, interactableLayer);

            Debug.DrawRay(interactionPoint.position, ray.direction * interactionRange, Color.red, 1f);

            if (hit.collider != null)
            {
                Debug.Log($"Hit: {hit.collider.name}");

                // Check if the hit object has an IInteractable component
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
            else
            {
                Debug.Log("No interactable object hit.");
            }
        }
    }
    #endregion
}
