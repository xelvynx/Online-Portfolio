using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    public SkillWindow sw;
    public SO_Skills skill;
    public int skillLevel;
    public Text skillLevelText;
    // Start is called before the first frame update
    public void Awake()
    {
        sw = SkillWindow.sw;
    }
    void Start()
    {
        skillLevel = skill.level;
        skillLevelText = transform.GetChild(1).GetComponent<Text>();
        skillLevelText.text = skillLevel.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde)) { skill.level= 1; }
    }
    public void SkillUp()
    {
        if (sw.skillPoints > 0)
        {
            sw.SetSkillPoints(-1);
            skill.level++;
            skillLevelText.text = skill.level.ToString();
        }
    }
}
