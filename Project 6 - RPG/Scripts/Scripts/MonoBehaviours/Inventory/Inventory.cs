using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //work on stackable items

    public EquippedGear equippedGear;
    public List<Item> itemList; //All items
    public List<Equipment> equipList;

    public Loot weaponLoot;
    public Loot itemLoot;

    public BaseCharacter baseChar;
    public void Start()
    {
        PickupItem(weaponLoot);
        baseChar = GetComponent<BaseCharacter>();
    }
    public void PickupItem(Loot loot)
    {
        Item item = ItemManager.Instance.GetItem(loot.itemType, loot.itemName);
        AddToInventory(item);
    }
    public void AddToInventory(Item item)
    {
        item.AddToInventory(this);
    }
}
