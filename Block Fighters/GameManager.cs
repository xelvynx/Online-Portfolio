using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public GameObject startButton;
    public GameObject panel;
    public GameObject player;
    public GameObject retryButton;
    public Text scoreT;
    private static GameManager _instance;
    static public GameManager gm
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    public float score;
    public GameObject boss;
    public GameObject bossSpawn;

	// Use this for initialization
	void Start () {
        _instance = this;
        scoreT.text = "Score : 0";
        Time.timeScale = 0;
        boss.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player == null)
        {
            Time.timeScale = 0;
            retryButton.SetActive(true);
        }
		
	}
    public void Score(int i)
    {
        score += i;
        scoreT.text = "Score : " + score;

        if (score >= 1 )
        {
            if (boss.activeSelf == false)
            {
                Debug.Log("boss active self = " + boss.activeInHierarchy);
                boss.SetActive(true);
            }


        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
