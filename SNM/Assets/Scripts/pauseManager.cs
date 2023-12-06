using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject resumeMenuUI;
    public Button resumeButton;
    public Button menuButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        // Set up button click listeners
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(ReturntoMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Hide the pause menu initially
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
    }

    void Update()
    {
        // Check for pause input (e.g., pressing "P" key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
