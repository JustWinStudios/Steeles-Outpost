using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    #region Editor Data
    [Header("Dependencies")]
    [SerializeField] private Transform weapon;
    [SerializeField] private float offset;

    [Header("Weapon Information")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectile;

    [Header("Shooting Information")]
    [SerializeField] private float fireRate;
    float nextShotTime;

    #endregion

    #region Tick
    private void Update()
    {
        WeaponRotation();
    }
    #endregion

    #region Weapon Rotation Logic
    private void WeaponRotation()
    {
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Calculates the angle between the weapon and mouse cursor
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);
    }
    #endregion
}
