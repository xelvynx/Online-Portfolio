using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

// Canvas
//   Character Info
//      Text
//      Texture2D icon
public class UIMenu : MonoBehaviour {    
    public Text mainCounterDisplay;
    public Text mainTimeDisplay;
    public Text mainScoreDisplay;
    public Text mainHealthDisplay;
    public Text mainMultiplierDisplay;
    public Text mainUltimateDisplay;
    public Text mainCurrency;
    public Text mainCurrentCurrency;
    public GameObject gameOverDisplay;
    public GameObject beatGameDisplay;


    public GameObject mainMenuButton;
    public GameObject playAgainButton;
    public GameObject playNextLevelButton;
    public GameObject pauseButton;
    public string mainMenu;
    public string nextLevelToLoad;
    public string playAgainLevelToLoad;

    private static UIMenu _instance;
    public static UIMenu instance
    {
        get
        {
            if ( _instance == null)
                _instance = GameObject.Find("UIMenu").GetComponent<UIMenu>();

            return _instance;
        }

    }
    private bool isPaused = false;

    public void EndGame()
    {
        GameManager.gm.gameIsOver = true;
        mainTimeDisplay.text = "Time: 0.00";
        if (gameOverDisplay)
            gameOverDisplay.SetActive(true);
        if (mainMenuButton)
            mainMenuButton.SetActive(true);
        if (playAgainButton)
            playAgainButton.SetActive(true);
        if (playNextLevelButton)
            playNextLevelButton.SetActive(true);
    }

    public void BeatLevel()
    {
        beatGameDisplay.SetActive(true);
        if (mainMenuButton)
            mainMenuButton.SetActive(true);
        if (playAgainButton)
            playAgainButton.SetActive(true);
        if (playNextLevelButton)
            playNextLevelButton.SetActive(true);
        if (mainMenuButton)
            mainMenuButton.SetActive(true);
    }
    // public function that can be called to restart the game
    public void RestartGame()
    {
        // we are just loading a scene (or reloading this scene)
        // which is an easy way to restart the level
        //Application.LoadLevel(playAgainLevelToLoad);
        SceneManager.LoadScene(playAgainLevelToLoad);
        Time.timeScale = 1.0f;
    }

    // public function that can be called to go to the next level of the game
    public void NextLevel()
    {
        // we are just loading the specified next level (scene)
        //Application.LoadLevel(nextLevelToLoad);
        SceneManager.LoadScene(nextLevelToLoad);
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1.0f;
    }
    public void SetTimeDisplay(string s)
    {
        mainTimeDisplay.text = s;
    }
    public void SetScoreDisplay(string s)
    {
        mainScoreDisplay.text = s;
    }
    public void SetHealthDisplay(string s) 
    {
        mainHealthDisplay.text = s;
    }
    public void SetCounterDisplay(string s) 
    {
        mainCounterDisplay.text = s;
    }
    public void SetMultiplierDisplay(string s)
    {
        mainMultiplierDisplay.text = s;
    }
    public void SetUltimateDisplay(string s)
    {
        mainUltimateDisplay.text = s;
    }
    public void SetCurrency(string s)
    {
        mainCurrency.text = s;
    }
    public void SetCurrentCurrency(string s)
    {
        mainCurrentCurrency.text = s;
    }
    public void PauseButton()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            if (mainMenuButton)
                mainMenuButton.SetActive(true);
            if (playAgainButton)
                playAgainButton.SetActive(true);
            if (playNextLevelButton)
                playNextLevelButton.SetActive(true);
        }
        else if (!isPaused)
        {
            Time.timeScale = 1.0f;
            if (mainMenuButton)
                mainMenuButton.SetActive(false);
            if (playAgainButton)
                playAgainButton.SetActive(false);
            if (playNextLevelButton)
                playNextLevelButton.SetActive(false);
        }

    }

}
