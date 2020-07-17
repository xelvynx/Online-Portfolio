using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemType itemType; 
    public string itemName;
    public void Start()
    {
        gameObject.name = itemName + " Loot";
    }

    public void AddToInventory() 
    {

    }
}
