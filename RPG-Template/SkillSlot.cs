using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Skill skill;
    public bool dragging = false;
    public Text childText;

    public void LoadSkill(Skill s) 
    {
        skill = s;
        transform.GetChild(0).GetComponent<Text>().text = s.name;
        gameObject.name = s.name;
    }
    public void Update()
    {
        if (dragging) 
        {
            transform.position = Input.mousePosition;
        }
    }
    public void UseSkill()
    {
        Debug.Log("Detected");
        if (Input.GetMouseButtonDown(1)) 
        {
            Warrior.player.AttackFunction(skill);
        }
    }
    public void Dragging() 
    {
        dragging = !dragging;
    }

}
