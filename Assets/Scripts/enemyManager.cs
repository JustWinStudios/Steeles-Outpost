using UnityEngine;
using System; // Needed for the Action delegate

public class enemyManager : MonoBehaviour
{
    // Health-related properties
    public int maxHealth = 100;
    public int currentHealth;

    // Movement-related properties
    public float moveSpeed = 2f;
    private Transform player;

    // Attack properties
    public float attackCooldown = 2.0f; // Cooldown duration between attacks, adjustable from the Inspector
    public float damageValue = 10f; // Damage inflicted upon the player, adjustable from the Inspector
    private float lastAttackTime; // Timestamp of the last attack

    // Event to signal enemy death
    public event Action OnEnemyDeath;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = Time.time - attackCooldown; // Ensure the enemy can attack immediately on the first contact
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= lastAttackTime + attackCooldown)
        {
            playerManager player = collision.gameObject.GetComponent<playerManager>();
            if (player != null)
            {
                player.TakeDamage((int)damageValue); // Use the damageValue variable for the damage amount
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
