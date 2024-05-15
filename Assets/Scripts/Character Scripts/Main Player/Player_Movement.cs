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
        if (rb == null) { rb = GetComponent<Rigidbody2D>(); }   
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
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    #endregion
}
