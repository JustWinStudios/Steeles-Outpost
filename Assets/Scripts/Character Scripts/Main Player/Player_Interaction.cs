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
        //Ensure the main camera is assigned:
        mainCamera = Camera.main;

        //Ensure the interaction point is assigned:
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
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(interactionPoint.position, ray.direction, interactionRange, interactableLayer);

            if (hit.collider != null)
            {
                // Check if the hit object has an IInteractable component
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
    #endregion
}
