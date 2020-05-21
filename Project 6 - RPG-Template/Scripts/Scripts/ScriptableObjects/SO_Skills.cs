using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill",menuName = "RPG/New Skill")]
public class SO_Skills : ScriptableObject
{
    public string name;

    public int baseDamage;
    public int level;
    public AttackInfo attackInformation;

    public float radius;
    public int speed;
}
