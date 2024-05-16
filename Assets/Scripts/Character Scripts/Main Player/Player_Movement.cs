using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    #region Editor Data
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        // Ensure the Rigidbody2D component is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Movement Logic
    private void ProcessInput()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = moveInput * moveSpeed;
    }

    private void Move()
    {
        // Apply movement
        rb.velocity = moveVelocity;
    }
    #endregion
}
