using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string name;

    public int baseDamage;
    public int level;
    public AttackAttribute aAttribute;
    public ElementalAttribute eAttribute;

    public float radius;
    public int speed;
}
