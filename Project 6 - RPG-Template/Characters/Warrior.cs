using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BaseCharacter
{
    public static Warrior player;
    public void Start()
    {
        player = this;
        InitializeStats(100,3, 3, 3);
    }
    public void Update()
    {
        if (InputManager.im.GetButtonDown("Slot1")) 
        {
            AttackFunction(sm.skillList[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//melee skill
        {
            AttackFunction(sm.skillList[1]);
        }
        if (Input.GetKeyDown(KeyCode.Space))//Reset Skill
        {
            for(int i = 0; i < sm.skillList.Count; i++)
            {
                sm.skillList[i].level = 1;
            }
            Debug.Log("Skills have been reset.");
        }
        if (Input.GetKeyDown(KeyCode.X))//Upgrade skill
        {
           sm. skillList[1].level++;
            Debug.Log(sm.skillList[0].level);
        }
    }
}
