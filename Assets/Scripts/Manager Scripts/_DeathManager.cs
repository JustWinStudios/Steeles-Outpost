using UnityEngine;
using UnityEngine.SceneManagement;

public class _DeathManager : MonoBehaviour
{
    public static _DeathManager instance; // Singleton instance

    public GameObject gameOverPanel; // Assign through Inspector
    public GameObject player;        // Reference to the player GameObject

    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseGame()
    {
        Debug.Log("Player defeated");
        gameOverPanel.SetActive(true); // Show the game over screen
        // Optionally, add more logic here for game over state, like pausing the game
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart current scene
    }
}
