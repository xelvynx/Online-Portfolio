using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PREGAME, RUNNING, PAUSED}
public class GameManager : Singleton<GameManager>
{
    public event GameUpdated gameUpdate;
    public GameObject skillWindow;
    public BaseCharacter baseCharacter;
    public GameState currentGameState = GameState.PREGAME;
    bool active = false;

    public void Start()
    {
        skillWindow = GameObject.Find("SkillWindow");
       
        skillWindow.SetActive(false);

        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            skillWindow.SetActive(!skillWindow.activeInHierarchy);
            
        }
    }

    public void UpdateState(GameState state) 
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;
        switch (currentGameState)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                break;
            case GameState.PAUSED:
                break;
            default:
                break;

        }
    }

    public void TogglePause() 
    {
        if(currentGameState == GameState.RUNNING) 
        {
            UpdateState(GameState.PAUSED);
        }
        else 
        {
            UpdateState(GameState.RUNNING);
        }
        //UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING); same as writing the if/else above
    }
    public void RestartGame() 
    {

    }
    public void QuitGame() 
    {
        //AutoSaving and cleanup
        //Implement features for quitting
        Application.Quit();
    }
}