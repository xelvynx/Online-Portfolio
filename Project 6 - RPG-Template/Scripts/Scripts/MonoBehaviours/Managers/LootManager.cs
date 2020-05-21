using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    public GameObject lootPrefab;
    public GameObject lootHolder;
    public void SpawnLoot(Transform transform) 
    {
        GameObject go = ObjectPooling.Instance.RequestLoot();
        go.transform.position = transform.position;
        SetLoot(go, "Potion");
    }

    //Change Loot prefab to contain the itemType as the item that's being spawned
    public void SetLoot(GameObject lootObject,string name)
    {
        Loot go = lootObject.GetComponent<Loot>();
        go.itemType = ItemManager.Instance.itemList.GetItem(name).itemType;
        go.itemName = ItemManager.Instance.itemList.GetItem(name).name;
        go.name = name;
    }
}
