using UnityEngine;
using System.Collections;

public class BoundaryDestroy : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        if (other.tag == "Asteroid")
            GameManager.gm.player.GetComponent<Player>().currentHealth -= Global.ASTEROIDBIG_DAMAGE;
        if (GameManager.gm.player.GetComponent<Player>().currentHealth <= 0)
        {
            StopAllCoroutines();
            Destroy(GameObject.Find("Player"));
            DestroyAll();
        }
        else if (GameObject.Find("Player") == null) { }
    }

    public void DestroyAll() 
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] objects1 = GameObject.FindGameObjectsWithTag("PowerUp");
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("Health");
        GameObject[] objects4 = GameObject.FindGameObjectsWithTag("AsteroidSmall");

        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        for (int i = 0; i < objects4.Length; i++)
        {
            Destroy(objects4[i]);
        }
        for (int i = 0; i < objects1.Length; i++)
        {
            Destroy(objects1[i]);
        }
        for (int i = 0; i < objects2.Length; i++)
        {
            Destroy(objects2[i]);
        }
    
    }

}
