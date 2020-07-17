using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    #region Fields
    [SerializeField] protected int strength;
    [SerializeField] protected int vitality;

    public CombatStats combatStats;
    //public int dexterity;
    #endregion
    public int Strength()
    {
        if (strength <= 0)
            return strength;
        return strength;


    }
    public int Vitality()
    {
        if (vitality <= 0)
            return 0;
        return vitality;
    }
    public void OnEnable()
    {
        strength = 5;
        vitality = 5;
        Strength();
        Vitality();
    }
    #region Increase Stats
    public void AddStats(Stats stats)
    {
        strength += stats.strength;
        vitality += stats.vitality;

        UpdateCombatStats();
    }
    public void RemoveStats(Stats stats) 
    {
        strength -= stats.strength;
        vitality -= stats.vitality;

        UpdateCombatStats();
    }
    public void UpdateCombatStats()
    {
        combatStats.CalculateStrength(Strength());
        UIManager.Instance.UpdateStrength(Strength());
        combatStats.CalculateVitality(Vitality());
        UIManager.Instance.UpdateVitality(Vitality());
    }
    public void IncreaseStrength(int i)
    {
        strength += i;
        UpdateCombatStats();
    }
    public void IncreaseVitality(int i)
    {

        vitality += i;
        UpdateCombatStats();
    }
    #endregion
    #region Decrease Stats
    public void DecreaseStrength(int i)
    {

        strength -= i;
        UpdateCombatStats();
    }
    public void DecreaseVitality(int i)
    {
        vitality -= i;
        UpdateCombatStats();
    }
    #endregion
}

