using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public static BaseCharacter player;
    public SkillManager sm;
    public Skill skillUsed;
    public AttackInfo attackInfo;
    public AttackAttribute attackAttribute;
    public ElementalAttribute elementalAttribute;
    
    [Header("Player Stats")]
    public int healthPoints;
    public int strength;
    public int dexterity;
    public int defense;
    public int vitality;

    [Header("Player Object Requirements")]
    public GameObject projectile;
    public LayerMask whatIsEnemy;
    public float radius;

    public void Awake()
    {
        if(GetComponent<SkillManager>() != null)
        sm = GetComponent<SkillManager>();
        
    }
    public void InitializeStats(int hp, int str, int dex, int vit)
    {
        healthPoints = hp;
        strength = str;
        dexterity = dex;
        vitality = vit;
        defense = vit * 15;
    }

    public void MoveCharacter()
    {

    }

    public virtual void AttackFunction(Skill s)
    {
        if (s is RangedSkill)
        {
            Debug.Log("Ranged");
            GameObject o = Instantiate(projectile, transform.position, Quaternion.identity);
            skillUsed = s;
            UpdateAttackInfo();
            o.GetComponent<Projectile>().SetProjectile(attackInfo,s);
        }
        if (s is MeleeSkill)
        {
            Debug.Log("Melee Skill Used");
            skillUsed = s;
            UpdateAttackInfo();
            Collider[] col = Physics.OverlapSphere(transform.position, s.radius, whatIsEnemy);
            for (int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Enemy>().TakeDamage(attackInfo);
                Debug.Log("Enemy's naem is : " + col[i].name);
            }
        }
    }
    public void UpdateAttackInfo()
    {
        int i = skillUsed.baseDamage * skillUsed.level * strength;//skill's base damage multiplied by the level multiplied by character's strength
        attackInfo.damage = i;//Updates AttackInfo damage
        attackInfo.eAttribute = skillUsed.eAttribute;
        attackInfo.aAttribute = skillUsed.aAttribute;
        Debug.Log("Damage = " + i);
    }
    public int CalculateDamage(int i) 
    {
        int dmg = i-defense;
        return dmg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
    public void TakeDamage(AttackInfo atk)
    {
        int damageDealt;
        if (atk.eAttribute != null)
        {
            if (atk.eAttribute.name == "Water" && elementalAttribute.name == "Fire")
            {
                damageDealt = CalculateDamage(atk.damage * 2);
                healthPoints -= damageDealt ;
                Debug.Log("Damage taken = " + damageDealt);
                Debug.Log("Double Damage");
            }
        }
        else
        {
            damageDealt = CalculateDamage(atk.damage);
            healthPoints -= damageDealt; 
            Debug.Log("Damage taken = " + damageDealt);
        }
    }

}
