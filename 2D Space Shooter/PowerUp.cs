using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
    public float speed = 2f;
    void Update() 
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerLaser") {
            GameObject.FindWithTag("Player").GetComponent<Player>().IncrementCounter();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.tag == "UltBeam")
        {
            Debug.Log("Hit");
            GameObject.FindWithTag("Player").GetComponent<Player>().IncrementCounter();
            Destroy(gameObject);            
        }
    }
}
