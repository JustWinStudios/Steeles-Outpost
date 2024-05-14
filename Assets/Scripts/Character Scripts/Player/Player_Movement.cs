using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    #region Editor Data
    [Header("Movement Attributes")]
    [SerializeField] float _moveSpeed = 100f;
    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rigidbody;
    #endregion

    #region Internal Data
    private Vector2 _moveDirection = Vector2.zero;
    #endregion

    #region Tick
    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region Input Logic
    private void GatherInput()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed * Time.fixedDeltaTime;
    }
    #endregion 
}
