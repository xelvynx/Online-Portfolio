using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    public GameObject lootPrefab;
    public List<GameObject> itemPool;
    public Transform lootContainer;

    public void Start()
    {
        GenerateLoot(10);
    }

    List<GameObject> GenerateLoot(int amountOfItems)
    {
        for (int i = 0; i < amountOfItems; i++)
        {
            GameObject item = Instantiate(lootPrefab, lootContainer);
            itemPool.Add(item);
            item.SetActive(false);
        }
        return null;
    }
    public GameObject RequestLoot()
    {
        foreach(var item in itemPool) 
        {
            if (!item.activeInHierarchy) 
            {
                //item is Available
                item.SetActive(true);
                return item;
            }
        }

        GameObject newItem = Instantiate(lootPrefab, lootContainer);
        itemPool.Add(newItem);
        return newItem;

        //need to create new item
        //if we made it to this pooint, we need to generate more items
        //when an item is picked up, setactive to false

    }
}
