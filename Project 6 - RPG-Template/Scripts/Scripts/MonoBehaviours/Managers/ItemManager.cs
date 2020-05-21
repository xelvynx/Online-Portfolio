using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SO_ItemList itemList;
    public SO_EquipmentList equipmentList;

    public Item GetItem(ItemType itemType,string itemName) 
    {
        switch (itemType) 
        {
            case ItemType.Weapon:
                return equipmentList.GetGear(itemType, itemName) as Equipment;
            case ItemType.Consumable:
                return itemList.GetItem(itemName);
        }
        return null;
    }
}
