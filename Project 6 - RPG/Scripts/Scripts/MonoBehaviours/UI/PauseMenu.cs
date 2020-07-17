using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    public void Start()
    {
        resumeButton.onClick.AddListener(HandleResumeClicked);
    }

    void HandleResumeClicked() 
    {
        GameManager.Instance.TogglePause();
    }
}
