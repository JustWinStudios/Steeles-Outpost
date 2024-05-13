using UnityEngine;

public class projectileManager : MonoBehaviour
{
    public float lifespan = 10f; // Time after which the projectile will be destroyed
    public int damage = 10; // Damage dealt by the projectile
    private bool hasDealtDamage = false; // Flag to ensure damage is only dealt once

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifespan); // Automatically destroy the projectile after its lifespan
    }

    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) >= 10f)
        {
            Destroy(gameObject); // Destroy the projectile if it moves 10 units from its starting point
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDealtDamage && collision.gameObject.CompareTag("Enemy")) // Ensure your enemy GameObject has the tag "Enemy"
        {
            enemyManager enemy = collision.gameObject.GetComponent<enemyManager>(); // Reference to enemyManager
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hasDealtDamage = true; // Prevents further damage application
            }
            Destroy(gameObject); // Destroy the projectile after dealing damage
        }
    }

}
