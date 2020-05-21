using UnityEngine;
using UnityEditor;
[System.Serializable]

public class Item 
{
    new public string name = "New Item";
    public Sprite icon = null;
    public ItemType itemType;
    public int amount;

    //public Item(string name, int i)
    //{
    //    this.name = name;
    //    this.amount = i;
    //}
    public virtual void UseItem()
    {
        Debug.Log("Item used : " + name);

    }
    public virtual void AddToInventory(Inventory inventory) 
    {
        inventory.itemList.Add(this);
        Debug.Log("Using Base class ITEM AddToInventory");
    }

    public virtual void RemoveFromInventory(Inventory inventory) 
    {
        inventory.itemList.Remove(this);
    }

}
