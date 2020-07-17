using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear", menuName = "RPG/Equipment List")]
public class SO_EquipmentList : ScriptableObject
{
    //create an editable list of weapons and armors for designers
    //create levelups for each weapon both a manual version and an automatic level up version
    //eg of automatic:every level it increases % of str vs manual: levelup 1 gives 1 str 2 dex, level2 gives 2 dex 3str
    public List<Weapon> weapons;
    public List<Equipment> equipmentDataBase;

    public void Start() 
    {
    }
    public Equipment GetGear(ItemType gearType, string name)
    {
        //Equipment item = equipmentDataBase.Find(p => p.name == name);

        switch (gearType) 
        {
            case ItemType.Weapon:
                Equipment item = weapons.Find(p => p.name == name);
                if (item != null) return item;
                return null;
            case ItemType.Chest:

                break;
            default:
  
                break;
        }
        return null;
        //if (item != null) { return item; }
        
    }
}
