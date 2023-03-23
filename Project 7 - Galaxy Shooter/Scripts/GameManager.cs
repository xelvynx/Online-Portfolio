using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;

    private void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
