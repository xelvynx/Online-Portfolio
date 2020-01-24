using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BaseCharacter
{
    public void Start()
    {
        InitializeStats(100,3, 3, 3);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//ranged skill
        {
            AttackFunction(skillList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//melee skill
        {
            AttackFunction(skillList[1]);
        }
        if (Input.GetKeyDown(KeyCode.Space))//Reset Skill
        {
            for(int i = 0; i < skillList.Count; i++)
            {
                skillList[i].level = 1;
            }
            Debug.Log("Skills have been reset.");
        }
        if (Input.GetKeyDown(KeyCode.X))//Upgrade skill
        {
            skillList[1].level++;
            Debug.Log(skillList[0].level);
        }
    }
}
