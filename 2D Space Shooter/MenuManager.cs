using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Text infoText;
    public Text gameText;
    public Text selectedText;
    public Text ult1;
    public string mainMenu;
    public string startGame;
    public string shopMenu;

    public GameObject infoMenu;
    public GameObject controlsMenu;
    public GameObject ultMenu;
    public GameObject ultChoices;
    public GameObject difficulty;
    public GameObject charSelect;
    public GameObject charPicker;

    public GameObject startButton;
    public GameObject shopButton;
    public GameObject quitButton;
    public GameObject controlButton;
    public GameObject infoButton;
    public GameObject resetButton;
    public GameObject ultButton;
    public GameObject charButton;

    public GameObject musicSelect;
    public Text musicText;
    public GameObject musicPicker;



    public GameObject homing;
    public GameObject beam;
    
    public int levelsBeat;

    private int ultimateSelection = 0;

    public bool ultBeam = false;
    public bool ultHoming = false;
    public int beamUpg = 0;
    public int homingUpg = 0;

    public string a;




    public void Awake() { }


    public void Start() 
    {
        beamUpg = PlayerPrefs.GetInt("BeamCount");
        homingUpg = PlayerPrefs.GetInt("HomingCount");
        levelsBeat = PlayerPrefs.GetInt("LevelsBeat");
        Debug.Log(levelsBeat + "" + "levels beat");
        a = PlayerPrefs.GetString("Ult1");




    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);

    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
       
        SceneManager.LoadScene(startGame);
    }
    public void ShopMenu()
    {
        // we are just loading the specified next level (scene)
        //Application.LoadLevel(nextLevelToLoad);
        SceneManager.LoadScene(shopMenu);
    }
    public void UpgradeReset() 
    {
        PlayerPrefs.DeleteAll();
    }

    public void ControlInfo()
    {
        controlsMenu.SetActive(true);
        EnableButtons(false);
        gameText.text = "\bCONTROLS \nClick left half of the screen to move left and click the right half of the screen to move right. \n Hold both sides to fire. \n Swipe up to use ultimate one, swipe down to use ultimate 2";
    }

    public void GameInfo() 
    {
        infoMenu.SetActive(true);
        EnableButtons(false);
        infoText.text = "Enemy: Rewards you with money and points. \n\n\nBig Asteroid: Destroy ASAP, if it goes past you, you lose life. Increases Score. \n\n\nSmall Asteroid: They move fast, and increase score.  \n\n\nHealth Pack: Recovers 10 hp. \n\n\nPower Up: Every 3 gives you increased damage and 1 ultimate.";
    }
    public void Difficulty(int i)
    {
        switch (i)
        {
            case 0:
                difficulty.SetActive(true);
                EnableButtons(false);
                break;
            case 1:
                PlayerPrefs.SetInt("Diff",i);
                StartGame();
                break;
            case 2:
                PlayerPrefs.SetInt("Diff",i);
                StartGame();
                break;

        }      
    }
    public void Cancel() 
    {
        EnableButtons(true);
        if (infoMenu)
        infoMenu.SetActive(false);
        if(controlsMenu)
        controlsMenu.SetActive(false);
        if(difficulty)
        difficulty.SetActive(false);
        if(ultMenu)
        ultMenu.SetActive(false);
        if(ultChoices)
        ultChoices.SetActive(false);
        if(charSelect)
        charSelect.SetActive(false);
        if(charPicker)
        charPicker.SetActive(false);
        if(musicSelect)
        musicSelect.SetActive(false);
        if(musicPicker)
        musicPicker.SetActive(false);
    }
    
    public void UltimateScreen()
    {
        EnableButtons(false);
        ult1.text = a; 
        ultMenu.SetActive(true);

    }
    public void UltimateChoice(int x) 
    {
        ultMenu.SetActive(false);
        ultChoices.SetActive(true);

        if (homingUpg < 1)            
            homing.GetComponent<Button>().interactable = false;
        if (beamUpg < 1)
            beam.GetComponent<Button>().interactable = false;

        ultimateSelection = x;
    }

    public void UltimateSelection(string s)
    {
        
        switch (s)
        {
            case "Destroy":
                if (ultimateSelection == 1)
                {
                    ult1.text = s + "";
                    PlayerPrefs.SetString("Ult1", s);
                }
                break;
            case "UltBeam":
                 if (ultimateSelection == 1)
                {
                    ult1.text = s + "";
                    PlayerPrefs.SetString("Ult1", s);
                }
                break;
            case "Homing":
                if (ultimateSelection == 1)
                {
                    ult1.text = s + "";
                    PlayerPrefs.SetString("Ult1", s);
                }
                break;

        }
        ultMenu.SetActive(true);
        ultChoices.SetActive(false);
    }

    public void EnableButtons(bool b)
    {
        startButton.SetActive(b);
        quitButton.SetActive(b);
        shopButton.SetActive(b);
        controlButton.SetActive(b);
        infoButton.SetActive(b);
        resetButton.SetActive(b);
        ultButton.SetActive(b);
        charButton.SetActive(b);
    }
    public void Back() 
    {
        ultMenu.SetActive(true);
        ultChoices.SetActive(false);
    }
    public void SelectedText(string s)
    {
        selectedText.text = s;
    }
    public void CharacterSelect(int i) 
    {

        SelectedText("Player " + PlayerPrefs.GetInt("Player") + "");
        switch (i)
        {
            case 0:
                charSelect.SetActive(true);
                EnableButtons(false);
                break;
            case 1:
                PlayerPrefs.SetInt("Player", i);
                charPicker.SetActive(false);
                SelectedText("Player " + i + "");

                break;
            case 2:
                PlayerPrefs.SetInt("Player", i);
                charPicker.SetActive(false);
                SelectedText("Player " + i + "");

                break;
            case 4:
                charPicker.SetActive(true);
                break;
        }
    }
    public void MusicSelection (string s)
    {
        switch (s)
        {
            case "Sleepy":
                musicText.text = s;
                PlayerPrefs.SetString("Music", s);
                musicPicker.SetActive(false);

                break;
            case "Debarge":
                musicText.text = s;
                PlayerPrefs.SetString("Music", s);
                musicPicker.SetActive(false);

                break;
            case "Open":
                musicSelect.SetActive(true);
                break;
            case "Select":
                musicPicker.SetActive(true);
                musicSelect.SetActive(false);
                break;

        }
                


    }

}