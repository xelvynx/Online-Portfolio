using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public Stats stats;
    public bool isEquipped = false;

    public override void UseItem()
    {
        base.UseItem();
        //Equip Item
        EquipmentManager.Instance.Equip(this);
        Testing();
    }
    public override void AddToInventory(Inventory inventory) 
    {
        inventory.equipList.Add(this);
        UseItem();
        Debug.Log("Using Base class Equipment AddToInventory");

    }
    public override void RemoveFromInventory(Inventory inventory)
    {
        inventory.equipList.Remove(this);
    }
    public void Testing() 
    {
        Debug.Log("Testing");
        EquipmentManager.Instance.Unequip((int)this.equipSlot);
    }

}
