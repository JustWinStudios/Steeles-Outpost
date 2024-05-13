using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance; // Singleton instance

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

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        // Any initialization logic you need when the game starts
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

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Quit the game
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Load a specific scene
    }

    // Add any other universal game management functionalities you might need
}
