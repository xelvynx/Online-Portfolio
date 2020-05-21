using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    #region Fields
    
    public static BaseCharacter player;
    public Stats stats;
    
    
    public SkillManager sm;
    public SO_Skills skillUsed;
    public AttackInfo attackInfo;
    public AttackAttribute attackAttribute;
    public ElementalAttribute elementalAttribute;

    [Header("Player Object Requirements")]
    public GameObject projectile;
    public LayerMask whatIsEnemy;
    public float radius;
    #endregion

    public void Awake()
    {
        stats = stats;
        

    }

    //public void EquipWeapon(Weapon w)//Item weapon 
    //{
    //    w.stats.Increment(w.stats.strength);
    //    w.stats.strength.IncreaseAmount(w.stats.strength.amount);
    //}
    
    //public void AddStats(SO_Equipment equip) 
    //{
    //    playerStats.strength.IncreaseAmount(equip.stats.strength.amount);
    //    playerStats.vitality.IncreaseAmount(equip.stats.vitality.amount);
        
    //}

    //public void InitializeStats(int hp, int str, int dex, int vit)
    //{
    //    playerStats.healthPoints = hp;
    //    playerStats.strength = str;
    //    playerStats.dexterity = dex;
    //    playerStats.vitality = vit;
    //    playerStats.defense = vit * 15;
    //}

    public void MoveCharacter()
    {

    }

    public virtual void AttackFunction(SO_Skills s)
    {
        if (s.attackInformation.attackRange == AttackRange.Ranged)
        {
            Debug.Log("Ranged");
            GameObject o = Instantiate(projectile, transform.position, Quaternion.identity);
            skillUsed = s;
            //UpdateAttackInfo();
            o.GetComponent<Projectile>().SetProjectile(attackInfo,s);
        }
        if (s.attackInformation.attackRange == AttackRange.Melee)
        {
            Debug.Log("Melee Skill Used");
            skillUsed = s;
            //UpdateAttackInfo();
            Collider[] col = Physics.OverlapSphere(transform.position, s.radius, whatIsEnemy);
            for (int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Enemy>().TakeDamage(attackInfo);
                Debug.Log("Enemy's naem is : " + col[i].name);
            }
        }
    }
    //public void UpdateAttackInfo()
    //{
    //    //int i = skillUsed.baseDamage * skillUsed.level * strength;//skill's base damage multiplied by the level multiplied by character's strength
    //    attackInfo.damage = i;//Updates AttackInfo damage
    //    attackInfo.eAttribute = skillUsed.eAttribute;
    //    attackInfo.aAttribute = skillUsed.aAttribute;
    //    Debug.Log("Damage = " + i);
    //}
    //public int CalculateDamage(int i) 
    //{
    //    //int dmg = i-defense;
    //    return dmg;
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
    public void TakeDamage(AttackInfo atk)
    {
        int damageDealt;
        if (atk.eAttribute != ElementalAttribute.None)
        {
            if (atk.eAttribute == ElementalAttribute.Water && elementalAttribute == ElementalAttribute.Fire)
            {
                //damageDealt = CalculateDamage(atk.damage * 2);
                //healthPoints -= damageDealt ;
                //Debug.Log("Damage taken = " + damageDealt);
                Debug.Log("Double Damage");
            }
        }
        else
        {
            //damageDealt = CalculateDamage(atk.damage);
            //healthPoints -= damageDealt; 
            //Debug.Log("Damage taken = " + damageDealt);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item") 
        {
            //inventory.Add
        }
    }
}
