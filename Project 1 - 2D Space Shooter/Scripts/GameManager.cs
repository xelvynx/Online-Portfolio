using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;//Reference for other scripts to this GameManager Script
    public GameObject player;

    public int loadPlayer;

    public int score = 0;
    public int beatLevelScore = 10;
    public bool gameIsOver = false;
    public bool canBeatLevel = false;

    public int currency;

    public float startTime = 5.0f;
    public float currentTime;
    public float timeToBeat;
    public float timeMod = 10f;
    public bool doublePass = false;
    public int difficulty;

    public int prevScore;

    public float bossMultiplier = 0f;
    public float asteroidMultiplier = 1;

    public int levelsBeat;
    private GameObject spawner;
    public int astCount;
    public int prevAstCount;

    private UIMenu ui;

    void Awake() {
        spawner = GameObject.Find("Spawner");
        ui = UIMenu.instance;
        //LoadCurrency();
        //PlayerPrefs.SetInt("Currency", 0);
        //PlayerPrefs.SetInt("ShotCountMax", 0);
        loadPlayer = PlayerPrefs.GetInt("Player");
        switch (loadPlayer)
        {
            case 0:
                player = (GameObject)Resources.Load("Player1");
                break;
            case 1:
                player = (GameObject)Resources.Load("Player1");
                break;
            case 2:
                player = (GameObject)Resources.Load("Player2");
                break;
        }

    }
	void Start () {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        difficulty = PlayerPrefs.GetInt("Diff");
        switch(difficulty)
        {
            case 1:
                beatLevelScore *= difficulty;
                break;
            case 2:
                canBeatLevel = false;
                break;


        }
       
        currentTime = startTime;


        Instantiate(player, new Vector2(0,-4.5f), player.transform.rotation);
        player = GameObject.FindGameObjectWithTag("Player");
	}
    void Update()
    {
        if (!gameIsOver)
        {
            if (canBeatLevel && (score >= beatLevelScore))
            {
                gameIsOver = true;
                BeatLevel(difficulty);
                Save();
                ui.SetTimeDisplay("Time: " + currentTime.ToString("0.00"));
                ui.SetScoreDisplay("Score: " + score.ToString("0"));
                ui.SetHealthDisplay("Health: " + player.GetComponent<Player>().currentHealth.ToString("0"));
                ui.SetCounterDisplay("Counter: " +player.GetComponent<Player>().counter.ToString("0"));
                ui.SetMultiplierDisplay("Multiplier:  " + player.GetComponent<Player>().multiplier.ToString("0"));
                ui.SetUltimateDisplay("Ultimate:  " + player.GetComponent<Player>().ultimate.ToString("0"));
                ui.BeatLevel();
                Time.timeScale = 0.0f;
            }
            if (currentTime <= 0 || player == null || player.GetComponent<Player>().currentHealth == 0)
            {
                gameIsOver = true;
                Save();
                ui.SetTimeDisplay("Time: " + currentTime.ToString("0.00"));
                ui.SetScoreDisplay("Score: " + score.ToString("0"));
                ui.SetHealthDisplay("Health: 0");
                ui.SetCounterDisplay("Counter: 0");
                ui.SetMultiplierDisplay("Multiplier: 1");
                ui.SetUltimateDisplay("Ultimate: 0");
                ui.EndGame();
                Time.timeScale = 0.0f;

            }
            else
            {
                currentTime -= Time.deltaTime;
                //ui.mainTimeDisplay.text = "Time: " + currentTime.ToString("0.00");
                ui.SetTimeDisplay("Time: " + currentTime.ToString("0.00"));
                ui.SetScoreDisplay("Score: " + score.ToString("0"));
                ui.SetHealthDisplay("Health: " + player.GetComponent<Player>().currentHealth.ToString("0"));
                ui.SetCounterDisplay("Counter: " + player.GetComponent<Player>().counter.ToString("0"));
                ui.SetMultiplierDisplay("Multiplier:  " + player.GetComponent<Player>().multiplier.ToString("0"));
                ui.SetUltimateDisplay("Ultimate:  " + player.GetComponent<Player>().ultimate.ToString("0"));
                ui.SetCurrency("Currency: " + PlayerPrefs.GetInt("Currency").ToString("0"));
                ui.SetCurrentCurrency("Current Currency: " + currency.ToString("0"));
            }
                
        }
    }
    public void CurrencyIncrement() 
    {
        if (currentTime < timeToBeat)
        {
            currency += Global.ENEMY_CURRENCY;
            Debug.Log("Currency = " + currency.ToString("0"));
        }
        if (currentTime > timeToBeat)
        {

            currency += Global.ENEMY_CURRENCY * 2;
            Debug.Log("Currency = " + currency.ToString("0"));
        }        
    }
    public void Save()
    {
        currency += PlayerPrefs.GetInt("Currency");
        PlayerPrefs.SetInt("Currency",currency);
        
        //PlayerPrefs.GetInt("Currency", Global.CURRENCY);
    }
    public void BossIncrement()
    {
        bossMultiplier++;
    }
    public void AddToScore(int i) 
    {
        score += i;
        if (score != 0 && score % 5000 == 0 && score != prevScore) 
        {
            spawner.GetComponent<Spawner>().SpawnInc();
            prevScore = score;
            BossIncrement();
            AsteroidIncrement();
        }
    }
    public void AsteroidIncrement() 
    {
        //if (score != 0 && score % 1000 == 0 && score != prevScore)
            asteroidMultiplier += .5f;
    }
    public void BeatLevel(int i) 
    {
        levelsBeat = i;
        if (PlayerPrefs.GetInt("LevelsBeat") > levelsBeat) 
        {
            levelsBeat = PlayerPrefs.GetInt("LevelsBeat");
        }
        PlayerPrefs.SetInt("LevelsBeat", levelsBeat);
    }
    public void AsteroidIncrement(int i) 
    {
        astCount += i;
        if (astCount != 0 && astCount % 5== 0 && astCount != prevAstCount)
        {
            Debug.Log("EnemyScore");

            GameObject.Find("Spawner").GetComponent<Spawner>().EnemySpawn();

            prevAstCount = astCount;
            StopCoroutine("asteroidSpawnWaves");
        }
    }
}