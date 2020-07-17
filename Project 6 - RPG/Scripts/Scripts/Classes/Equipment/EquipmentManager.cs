using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public Equipment[] currentEquipment;

    public delegate void EquipmentChanged(Equipment newItem, Equipment oldItem);
    public EquipmentChanged onEquipmentChanged;
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }
    #region Brackey's Version
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            //Inventory.instance.Add(oldItem)  // Removes item from slot and adds it back to inventory
        }
        if(onEquipmentChanged!= null) 
        {
            onEquipmentChanged(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem; 
    }
    public void Unequip(int slotIndex) 
    {
        if(currentEquipment[slotIndex] != null) 
        {
            Equipment oldItem = currentEquipment[slotIndex];
            //Inventory.instance.Add(oldItem)  // Removes item from slot and adds it back to inventory
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged(null, oldItem);
            }
        }
       

    }
    public void UnequipAll() 
    {
        for(int i = 0; i < currentEquipment.Length; i++) 
        {
            Unequip(i);
        }
    }
    #endregion
    #region MyVersion
    public void EquipItem(Equipment newItem) 
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        
        
        if (!currentEquipment[slotIndex].isEquipped) 
        {
            currentEquipment[slotIndex] = newItem;
        }
        if (currentEquipment[slotIndex].isEquipped) 
        {
            
        }
    }
    public void UnEquip(Equipment newItem) 
    {
        int slotIndex = (int)newItem.equipSlot;
        //currentEquipment[slotIndex];
    }
#endregion
    //public void CheckEquipped(Inventory inventory)
    //{
    //    if (!inventory.equippedGear.weapon.isEquipped)
    //    {
    //        Debug.Log("Not Equipped");
    //        inventory.equippedGear.weapon = this;
    //        inventory.equippedGear.weapon.isEquipped = true;
    //        Debug.Log("Equipping now");
    //        inventory.gameObject.GetComponent<BaseCharacter>().stats.AddStats(this.stats);
    //    }
    //    if (inventory.equippedGear.weapon.isEquipped)
    //    {
    //        Debug.Log("is Equipped");
    //    }
    //}

    //public void EquipGear(Weapon weapon)
    //{
    //    if (!isEquipped)
    //    {
    //        isEquipped = true;
    //        this.stats = weapon.stats;

    //    }
    //}
}
