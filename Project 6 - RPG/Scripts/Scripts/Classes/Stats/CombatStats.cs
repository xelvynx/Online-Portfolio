using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CombatStats
{
    public int baseHealth = 10;
    public int currentHealth = 10;
    public int basePhysicalDamage = 1;
    public int minimumPhysicalDamage;
    public int maximumPhysicalDamage;
    #region Calculators
    public void CalculateStrength(int strength)
    {
        minimumPhysicalDamage = basePhysicalDamage + Mathf.RoundToInt(strength + .5f);
        maximumPhysicalDamage = basePhysicalDamage + Mathf.RoundToInt(strength * 1.5f);
        UIManager.Instance.UpdateDamage(maximumPhysicalDamage);

    }
    public void CalculateVitality(int vitality) 
    {
        currentHealth = baseHealth + (vitality * 5);
        UIManager.Instance.UpdateHealth(currentHealth);
    }
    #endregion
}
