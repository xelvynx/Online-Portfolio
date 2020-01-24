using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skillWindow;
   

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
}
