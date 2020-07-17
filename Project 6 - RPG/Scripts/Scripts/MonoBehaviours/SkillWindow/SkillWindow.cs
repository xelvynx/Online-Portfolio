using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillWindow : MonoBehaviour
{
    private static SkillWindow _instance;
    public static SkillWindow sw
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("SkillWindow").GetComponent<SkillWindow>();
            }

            return _instance;
        }
    }
    public BaseCharacter player;
    public List<SO_Skills> skillList = new List<SO_Skills>();
    public Text skillPointText;
    public int skillPoints =1 ;




    // Start is called before the first frame update
    void Start()
    {
        
        skillPointText = GameObject.Find("SkillPointText").GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<BaseCharacter>();
        foreach(SO_Skills s in player.sm.skillList) 
        {
            skillList.Add(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetSkillPoints(1);
        }
    }
    public void SetSkillPoints(int i)
    {
        skillPoints += i;
        skillPointText.text = skillPoints.ToString();
    }
}
