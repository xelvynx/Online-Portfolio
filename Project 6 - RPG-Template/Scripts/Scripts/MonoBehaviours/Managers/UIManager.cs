using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public Text strengthText, vitalityText, healthText, damageText;

    public void UpdateStrength(int i) 
    {
        strengthText.text = i.ToString();
    }
    public void UpdateVitality(int i) 
    {
        vitalityText.text = i.ToString();
    }
    public void UpdateHealth(int i) 
    {
        healthText.text = "Health : " + i.ToString();
    }
    public void UpdateDamage(int i)
    {
        damageText.text = "Damage : " + i.ToString();
    }
}
