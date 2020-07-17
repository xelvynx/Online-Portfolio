using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAllocation : MonoBehaviour
{
    [SerializeField] public BaseCharacter player;
    

    public void Start()
    {
        player.stats.UpdateCombatStats();
    }
    public void UpdateStats() 
    {
        
    }
    public void IncreaseStrength()
    {

        player.stats.IncreaseStrength(1);
    }
    public void IncreaseVitality()
    {
        player.stats.IncreaseVitality(1);
    }
    public void DecreaseStrength()
    {
        player.stats.DecreaseStrength(1);
    }
    public void DecreaseVitality()
    {
        player.stats.DecreaseVitality(1);
    }


}
