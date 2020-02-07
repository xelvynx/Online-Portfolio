using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public float health = 5;


    public void OnEnable()
    {
        health = 5;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        health--;
        Destroy(other.gameObject);
        
        Debug.Log("Boss got hit");
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
