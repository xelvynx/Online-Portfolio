using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    public List<SO_Skills> skillList = new List<SO_Skills>();
    public List<Transform> skillSlot = new List<Transform>();
    public Transform skillBar;
    public GameObject skillSlotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(skillBar.childCount);
        foreach (Transform child in skillBar)
        {
            skillSlot.Add(child);
        }
        for (int i = 0; i < skillList.Count; i++)
        {


            skillSlot[i].GetChild(0).GetComponent<SkillSlot>().LoadSkill(skillList[i]);
            

        }
        foreach(Transform t in skillSlot) 
        {
            if (t.GetChild(0).GetComponent<SkillSlot>().skill == null) t.GetChild(0).gameObject.SetActive(false);
        }

        //for (int i = 0; i < skillBar.childCount; i++) 
        //{
        //    skillBar.GetChild(i).name = skillList[i].name;
        //    skillBar.GetChild(i).GetChild(0).name = skillList[i].name;
        //    skillBar.GetChild(i).GetChild(0).GetComponent<SkillSlot>().LoadSkill(skillList[i]);
        //} 

        //foreach(Skill s in skillList) 
        //{
        //    GameObject go = Instantiate(skillSlotPrefab, skillBar.position,Quaternion.identity,skillBar);
        //    go.GetComponent<SkillSlot>().LoadSkill(s);
        //    go.name = s.name;
        //    go.transform.GetChild(0).GetComponent<Text>().text = s.name;
        //}
    }


}
