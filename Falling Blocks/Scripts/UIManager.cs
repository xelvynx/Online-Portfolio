using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
public class UIManager : MonoBehaviour
{
    public GameObject startScreen;
    public Image gameImage;
    public Sprite gameImageSprite;
     public GameObject previousScreen;
     public GameObject optionScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public GameObject controlScreen;
    public GameObject instructionScreen;
    public GameObject keyBindDialogBox;
    public Text jumpText;
    public Text attackAheadText;
    public Text team1Text;
    public Text team2Text;
    public Text timerText;
    public Text skillCDButtonText;
    public Text moveCountText;
    private int team1Points;
    private int team2Points;
    public bool inGame = false;
    public static UIManager um;
    public bool canAddPoints = true;
    [SerializeField]
    private List<Player> players = new List<Player>();
    public InputField ipTile;
    public InputField ipObstacle;
    public InputField ipPower;
    int intTile;
    int intObs;
    int intPower;
    public float timer = 99;
    private string startString;
    public bool paused = false;
    public bool skillCD;
    public GameObject mobileControls;
    //create UI and input for # of tiles/powerups/obstacles/instatiles in starting menu
     // Start is called before the first frame update
    void Start()
    {
        skillCD = Player.play.GetSkillCooldown();
        if (skillCD)
        {
            skillCDButtonText.text = "Skill CD is on";
            foreach (Player go in players)
            {
                go.SetSkillCooldown(skillCD);
            }
         }
        if (!skillCD)
        {
            skillCDButtonText.text = "Skill CD is off";
            foreach (Player go in players)
            {
                go.SetSkillCooldown(skillCD);
            }
        }
        mobileControls = GameObject.Find("Mobile Controls");
        mobileControls.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS
        mobileControls.SetActive(true);
#endif
         Time.timeScale = 0;
        if (um == null)
        {
            um = this;
        }
        gameImage.sprite = gameImageSprite;
        FindAndDisable();
        players.Add(GameObject.Find("Player1").GetComponent<Player>());
        players.Add(GameObject.Find("Player2").GetComponent<Player>());
     }
    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            if (!paused)
            {
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    inGame = false;
                    paused = true;
                    endScreen.SetActive(true);
                    Time.timeScale = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
    public void Pause() 
    {
        if (paused)
        {
            paused = false;
            pauseScreen.SetActive(paused);
            Time.timeScale = 1;
            return;
        }
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(paused);
            Time.timeScale = 0;
            return;
        }
     }
    private void FixedUpdate()
    {
        timerText.text = Mathf.RoundToInt(timer).ToString();
    }
    public void SetTile()
    {
        if (ipTile.text == "") { Debug.Log("nothing"); return; }
        intTile = int.Parse(ipTile.text);
        Debug.Log("intTile : " + intTile);
        ipTile.text = "";
        ipTile.placeholder.GetComponent<Text>().text = "Set tile count to " + intTile;
        GameManager.gm.SetTileCount(intTile);
    }
    public void SetObstacle()
    {
        if (ipObstacle.text == "") { Debug.Log("nothing"); return; }
        intObs = int.Parse(ipObstacle.text);
        Debug.Log("IntObs : " + intObs);
        ipObstacle.text = "";
        ipObstacle.placeholder.GetComponent<Text>().text = "Set tile count to " + intObs;
        GameManager.gm.SetObstacleCount(intObs);
    }
    public void SetPowerUp()
    {
        if (ipPower.text == "") { Debug.Log("nothing"); return; }
        intPower = int.Parse(ipPower.text);
        Debug.Log("IntPower : " + intPower);
        ipPower.text = "";
        ipPower.placeholder.GetComponent<Text>().text = "Set tile count to " + intPower;
        GameManager.gm.SetPowerupCount(intPower);
    }
    public void ToggleWindow()
    {
        previousScreen = EventSystem.current.currentSelectedGameObject;
        Debug.Log("this gameobject name is " + previousScreen.name);
        string s = previousScreen.name;
        switch (s)
        {
            case "Singleplayer":
                 StartGame(s);
                break;
            case "Multiplayer":
                 StartGame(s);
                break;
            case "Instructions":
                previousScreen = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                instructionScreen.SetActive(true);
                break;
            case "Options":
                previousScreen = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                previousScreen.SetActive(false);
                optionScreen.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS
        optionScreen.transform.GetChild(1).gameObject.SetActive(false);
#endif
                break;
             case "Pause Screen":
                previousScreen = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                previousScreen.SetActive(false);
                pauseScreen.SetActive(true);
                break;
             case "End Screen":
                previousScreen = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                previousScreen.SetActive(false);
                endScreen.SetActive(true);
                break;
             case "Controls":
                previousScreen = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                previousScreen.SetActive(false);
                controlScreen.SetActive(true);
                break;
         }
    }
    public void Resume()
    {
        paused = false;
        pauseScreen.SetActive(paused);
        Time.timeScale = 1;
        return;
     }
    public void Restart()
    {
        StartGame(startString);
    }
    public void StartGame(string s)
    {
        if (s == "Singleplayer")
        {
            GameManager.gm.SinglePlayerMode();
            players[0].CanMoveHim();
            players[1].gameObject.SetActive(false);
         }
        if (s == "Multiplayer")
        {
            foreach (Player go in players)
            {
                go.CanMoveHim();
            }
            GameManager.gm.SendInfo();
        }
        paused = false;
        inGame = true;
        Time.timeScale = 1;
        timer = 99;
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        startString = s;
        Resetpoints();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        previousScreen.SetActive(true);
    }
    public void ReturnToMainScreen() { SceneManager.LoadScene(0); }
    public void KeyBinds()
    {
        keyBindDialogBox.SetActive(true);
    }
    public void SaveKeyBinds() 
    {
        Player.play.SetControls();
    }
    void FindAndDisable()
    {
        startScreen = GameObject.Find("Start Screen");
        optionScreen = GameObject.Find("Option Screen");
        pauseScreen = GameObject.Find("Pause Screen");
        endScreen = GameObject.Find("End Screen");
        keyBindDialogBox = GameObject.Find("Key Bind Dialog Box");
        optionScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endScreen.SetActive(false);
        keyBindDialogBox.SetActive(false);
        controlScreen.SetActive(false);
        instructionScreen.SetActive(false);
    }
    public void NextLevel() 
    {
        GameManager.gm.SetAbilities();
        StartGame(startString);

    }
    public void UpdateJump(int i)
    {
        Debug.Log("I've been here");
        jumpText.text = "Jump :   " + i.ToString();
    }
    public void UpdateAttackAhead(int i)
    {
        attackAheadText.text = "Attack Ahead:   " + i.ToString();
    }
    public void ReduceNullify() { }
    public void AddTeam1Points()
    {
        if (canAddPoints)
        {
            team1Points += 100;
            canAddPoints = false;
            team1Text.text = "Team 1 Points :    " + team1Points.ToString();
            EnableAddPoints();
        }
    }
    public void AddTeam2Points()
    {
        if (canAddPoints)
        {
            team2Points += 100;
            canAddPoints = false;
            team2Text.text = "Team 2 Points :    " + team2Points.ToString();
            Invoke("EnableAddPoints", 3);
        }
    }
    void Resetpoints() 
    
    {
        team1Points = 0;
        team2Points = 0;
        team1Text.text = "Team 1 Points :    " + team1Points.ToString();
        team2Text.text = "Team 2 Points :    " + team2Points.ToString();
    }
    void EnableAddPoints()
    {
        canAddPoints = true;
    }
    public void SetEndScreen(string s)
    {
        switch (s)
        {
            case "Win":
                endScreen.SetActive(true);
                endScreen.transform.GetChild(0).GetComponent<Text>().text = "You Win";
                Time.timeScale = 0;
                break;
             case "Lose":
                endScreen.SetActive(true);
                endScreen.transform.GetChild(0).GetComponent<Text>().text = "You Lose";
                Time.timeScale = 0;
                break;
        }
     }
    public void SetSKillCooldown() 
    {
        skillCD = !skillCD;
        if (skillCD) 
        {
            skillCDButtonText.text = "Skill CD is on";
            foreach(Player go in players) 
            {
                go.SetSkillCooldown(skillCD);
            }
         }
        if(!skillCD)
        {
            skillCDButtonText.text = "Skill CD is off";
            foreach (Player go in players)
            {
                go.SetSkillCooldown(skillCD);
            }
        }
    }
    public void UpdateMoveCount(int i) 
    {
        moveCountText.text = "Move Count : " + i;
    }
}
