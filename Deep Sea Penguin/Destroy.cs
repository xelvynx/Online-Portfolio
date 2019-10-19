using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    Spawner sp;

    void Start()
    {
        sp = Spawner._spawner.GetComponent<Spawner>();
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject == GameObject.FindGameObjectWithTag("Obstacle1"))
        {
            sp.obCount-=1f;
        }
        if (other.gameObject == GameObject.FindGameObjectWithTag("Bomb"))
            sp.spawnCount -= 1;
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        if (other.tag == "Bomb")
        {
            sp.spawnCount -= 1;
            if (sp.spawnCount <= 0) sp.spawnCount = 0;
            Debug.Log("BombDown");
        }
        if (other.gameObject == GameObject.FindGameObjectWithTag("Obstacle1"))
        {
            sp.obCount -= 1f;
        }

    }

    /////////////////amount of stamina decreases when moving left and right, not returning to middle, reduce falling speed, and stamina degradation rate 
}
