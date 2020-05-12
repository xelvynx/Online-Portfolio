using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RPG/Attack Info")]
public class AttackInfo : ScriptableObject
{
    public int damage;
    public AttackAttribute aAttribute;
    public ElementalAttribute eAttribute;

    public void LoadInfo(AttackInfo a)
    {
        damage = a.damage;
        aAttribute = a.aAttribute;
        eAttribute = a.eAttribute;
    }
}
