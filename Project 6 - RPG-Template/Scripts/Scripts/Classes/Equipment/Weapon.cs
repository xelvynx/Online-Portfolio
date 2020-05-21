using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon : Equipment
{
    public void Start() { equipSlot = EquipmentSlot.Weapon; }
    
}
