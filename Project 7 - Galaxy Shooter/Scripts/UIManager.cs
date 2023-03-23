using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _lifeDisplay;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }
    public void UpdateScoreText(int playerScore) 
    {

        _scoreText.text = "Score: " + playerScore;
    }
    public void UpdateLivesSprite(int currentLives)
    {
        _lifeDisplay.sprite = _livesSprites[currentLives];
        if (currentLives <= 0)
        {
            StartCoroutine(GameOverFlicker());
        }
    }
    IEnumerator GameOverFlicker()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    public void RestartGame()
    {

    }
}
