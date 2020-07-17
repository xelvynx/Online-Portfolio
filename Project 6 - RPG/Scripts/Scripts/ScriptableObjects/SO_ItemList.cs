using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Item Me", menuName = "RPG/Inventory/Item List")]
public class SO_ItemList : ScriptableObject
{
    public static SO_ItemList itemList;
    [SerializeField]
    public List<Item> itemDatabase;

    public Item GetItem(string name)
    {
        Item item = itemDatabase.Find(p => p.name == name);

       
        if(item!= null) { return item; }
        else
        {
            Debug.Log("Nothing was found");
            return null;
        }
    }
}
