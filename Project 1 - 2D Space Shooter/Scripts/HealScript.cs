using UnityEngine;
using System.Collections;

public class HealScript : MonoBehaviour {
    public GameObject player;
    public float speed = 2f;

    void Awake() 
    {
        player = GameManager.gm.player;
    }
	void Update () {
        transform.Translate (Vector2.down * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "PlayerLaser") { 
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (player.GetComponent<Player>().currentHealth >= player.GetComponent<Player>().maxHealth)
                player.GetComponent<Player>().currentHealth += 0;
            else if (player.GetComponent<Player>().currentHealth < player.GetComponent<Player>().maxHealth)
            {
                player.GetComponent<Player>().currentHealth += Global.HEAL_HEAL;
                if (player.GetComponent<Player>().currentHealth > player.GetComponent<Player>().maxHealth)
                    player.GetComponent<Player>().currentHealth = player.GetComponent<Player>().maxHealth;
            }
        }
        if (other.tag == "UltBeam")
        {
            Debug.Log("Hit");
            if (player.GetComponent<Player>().currentHealth >= player.GetComponent<Player>().maxHealth)
               player.GetComponent<Player>().currentHealth += 0;
            else if (player.GetComponent<Player>().currentHealth < player.GetComponent<Player>().maxHealth)
               player.GetComponent<Player>().currentHealth += Global.HEAL_HEAL;
            Destroy(gameObject);

        }
    }
}
