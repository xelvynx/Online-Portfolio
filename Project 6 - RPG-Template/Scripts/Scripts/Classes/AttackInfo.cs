using UnityEngine;

public class AttackInfo
{
    public int damage;
    public AttackRange attackRange;
    public AttackAttribute aAttribute;
    public ElementalAttribute eAttribute;

    public void LoadInfo(AttackInfo a)
    {
        damage = a.damage;
        aAttribute = a.aAttribute;
        eAttribute = a.eAttribute;
    }
    
}

