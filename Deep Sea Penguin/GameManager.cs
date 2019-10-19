using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public GameObject healthBar;
    public GameObject startButton;
    public GameObject retryButton;
    public GameObject skinButton;
    public GameObject tipScreen;
    public GameObject rightO;
    public GameObject leftO;
    public GameObject controlButton;

    private static GameManager _instance;
    public static GameManager gm
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }

            return _instance;
        }
    }

    public bool gameStart = false;
    public bool gameEnded = false;

    public float playerSpeed = 10f;
    public float bombSpeed = 10f;
    public float addSpeed;
    public float gameTime;
    public float obDelay;

    public float maxStamina = 25;
    public float currentStamina;
    public float stamDegradeRate = 2;
    public int score;
    public int setScore = 100;
    public int difficultyUp = 500;
    public int highScore;

    public Text scoreText;
    public Text highScoreText;

    int ran;
    Spawner sp;

    int gameEndNum = 0;

#if UNITY_IOS
	private string gameId = Configuration.myGameIdiOS;
#elif UNITY_ANDROID
    private string gameId = Configuration.myGameIdAndroid;
#endif
    void Awake()
    {        
        currentStamina = maxStamina;
        Time.timeScale = 0;
        sp = Spawner._spawner;        
        //PlayerPrefs.SetInt("HighScore", score);

    }
     public void Start()
    {

        Advertisement.Initialize(gameId, true);
        //Advertisement.Initialize(gameId, true);

        //Invoke("ShowAd", 3);
    }
    public void ShowAd()
    {
        //var options = new ShowOptions();

        
        Advertisement.Show("video");
    }
    void Update ()
    {
        //if (tipScreen.activeSelf == true && Input.touchCount > 0 || tipScreen.activeSelf == true && Input.GetMouseButtonDown(0))
        //{
        //    ControlScreen();
        //}
        if (gameStart == true)
        {
            Time.timeScale = 1;
            TextChange();
            SetStaminaBar();
            if (sp.obCount <= 0)
                sp.obCount = 0;
            if (sp.spawnCount <= 0)
                sp.spawnCount = 0;         
        }
        if (gameEndNum >= 4)
        {
            ShowAd();
            gameEndNum = 0;
        }
    }
    public void Controls()
    {

        
        tipScreen.SetActive(true);
        rightO.SetActive(true);
        leftO.SetActive(true);
        controlButton.SetActive(false);
    }
    public void ControlScreen()
    {
        
        tipScreen.SetActive(false);
        rightO.SetActive(false);
        leftO.SetActive(false);
        controlButton.SetActive(true);
    }
    public void StartGame()
    {
        controlButton.SetActive(false);
        tipScreen.SetActive(false);
        gameStart = true;
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString("F0");
        setScore = 100;
        score = 0;
        currentStamina = maxStamina;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform.position = new Vector3(0, 0, transform.position.z);
        sp.StartCoroutine("StaminaSpawn");
        sp.StartCoroutine("BombSpawn");
        sp.StartCoroutine("ObstacleSpawn");
        startButton.SetActive(false);
        retryButton.SetActive(false);
        sp.spawnCount = 0;
        sp.obCount = 0;
        addSpeed = 0;
        playerSpeed = 10 + addSpeed;
        ButtonsOff();
    }
    void ButtonsOff()
    {
        startButton.SetActive(false);
        retryButton.SetActive(false);
        skinButton.SetActive(false);
    }
    void TextChange()
    {
        
        scoreText.text = score.ToString("N0");
        highScoreText.text = highScore.ToString("N0");
    }
    public void StaminaInfo(float f)
    {
        currentStamina -=f;
    }

    void SetStaminaBar()
    {
        float calcStamina = currentStamina / maxStamina;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            GameOver();
            
            
        }
        
        if (currentStamina >= 25)
            currentStamina = 25;
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(calcStamina, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void GameOver()
    {
        gameEndNum++;
        sp.Stop();
        StopCoroutine("Scoring");
        gameStart = false;
        Time.timeScale = 0;
        retryButton.SetActive(true);
        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString("N0");
    }
    public void Retry()
    {
        DestroyAll();
        StartGame();
        Reset();
        gameStart = true;
    }
    public void Reset()
    {
        sp.maxOb = Configuration.maxObSpawn;
        sp.maxSpawn = Configuration.maxBombSpawn;
        sp.maxSpawnDelay = Configuration.maxSpawnDelay;
        addSpeed = 0;
        playerSpeed = Configuration.playerSpeed;
        bombSpeed = Configuration.bombSpeed;
        sp.obDelay = Configuration.obDelay;
    }
    public void ScorePlus(int i)
    {
        score += i;
        if (score % 5 == 0)
        {
            //difficultyUp *= 2;
            sp.maxSpawnDelay -= .2f;
            if (sp.maxSpawnDelay <= .5f)
                sp.maxSpawnDelay = .5f;
            sp.maxSpawn+=3;
            addSpeed += .5f;
            playerSpeed = addSpeed + 10;
            sp.obDelay -= .2f;
            if (sp.obDelay <= 1)
                sp.obDelay = 1;
        }
        if (score % 10 == 0)
            sp.maxOb += 1;
    }
    void DestroyAll()
    {

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Stamina");
        foreach (GameObject go in gos)
            Destroy(go);
        GameObject[] go1 = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject go in go1)
            Destroy(go);
        GameObject[] go2 = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject go in go2)
            Destroy(go);

    }

}

//possible ideas
//Multiple characters, easy mode with very responsive controls, medium with a slight resistance as you move, hard: takes a long time to move in other direction, requires counter steering
//at 5000 points, every once in a while there will be bombs that drop in the same x position as you
//achievement system
//for the obstacle, create a small maze using multiple of the "Obstacle" prefab and having people weave through
//for my game, i'll use rocks for the bombs, if you hit them, you stumble and lose stamina, the obstacle will be a line of trees, the stamina will be an apple, and you're running in forest
//****spawn single wall obstacles so they can enter from two different sides possibly and change bombs to just slow down the movement speed of the character instead of taking out stamina
//****increase movement speeds of player+bomb+obstacles based off of how many obstacles they have passed, obstacles passed = score