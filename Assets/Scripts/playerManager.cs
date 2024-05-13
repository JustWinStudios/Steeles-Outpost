using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // For scene management

public class playerManager : MonoBehaviour
{
    // Movement properties
    public float speed = 5f;
    private Rigidbody2D rb;

    // Shooting properties
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    // Health properties
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // Assign a UI Slider in the Inspector for the health bar
    public Image healthFillImage; // Assign the Fill Image of the health bar slider
    public CanvasGroup healthBarCanvasGroup; // Assign in the Inspector for fade in/out effect

    // Game Over properties
    public GameObject gameOverPanel; // Assign a UI panel for the game over screen in the Inspector
    public Canvas playerCanvas; // Assign the Player Canvas in the Inspector

    private bool isHealthBarVisible = false;
    private Coroutine fadeCoroutine;
    private bool isGameOver = false; // Flag to check if the game is over

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RespawnPlayer();
    }

    void Update()
    {
        if (isGameOver) return; // Skip Update if the game is over

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootingDirection = (mousePosition - (Vector2)transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = shootingDirection * projectileSpeed;
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
            healthFillImage.color = Color.Lerp(Color.red, Color.green, (float)currentHealth / maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            LoseGame();
        }
        else
        {
            if (!isHealthBarVisible)
            {
                isHealthBarVisible = true;
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeHealthBar(1, 0.5f));
            }
        }
    }

    void LoseGame()
    {
        Debug.Log("Player defeated");
        isGameOver = true; // Set the game over flag
        gameOverPanel.SetActive(true);
        playerCanvas.enabled = false; // Disable the Player Canvas
    }

    public void RespawnPlayer()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        healthBarCanvasGroup.alpha = 0;
        gameOverPanel.SetActive(false);
        playerCanvas.enabled = true; // Re-enable the Player Canvas
        isGameOver = false; // Reset the game over flag
        // Reset player position and other necessary states here
    }

    IEnumerator FadeHealthBar(float targetAlpha, float duration)
    {
        float startAlpha = healthBarCanvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            healthBarCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        healthBarCanvasGroup.alpha = targetAlpha;

        if (targetAlpha == 1)
        {
            yield return new WaitForSeconds(2);
            fadeCoroutine = StartCoroutine(FadeHealthBar(0, 1.5f));
        }
        else
        {
            isHealthBarVisible = false;
        }
    }
}
