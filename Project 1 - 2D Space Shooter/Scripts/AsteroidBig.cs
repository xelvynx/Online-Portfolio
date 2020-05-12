using UnityEngine;
using System.Collections;

public class AsteroidBig : MonoBehaviour {
    public float speed = 5f;
    public float hp = 10;
    public int score;
    public float damage;
    public GameObject asteroid;
	// Update is called once per frame

    void Start() 
    {
        LoadStats();
        hp += GameManager.gm.asteroidMultiplier;
        speed += GameManager.gm.asteroidMultiplier;
        if (GameManager.gm.player == null) 
        {
            Destroy(gameObject);
        }
    }
	void Update () {
        transform.Translate (Vector2.down * speed * Time.deltaTime);
        //if(GameObject.Find("Enemy(Clone)") != null)
        //    Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerLaser") 
        {
            hp -= GameManager.gm.player.GetComponent<Player>().Damage;
            Destroy(other.gameObject);
            if(hp <= 0)
            {
                GameManager.gm.AddToScore(score);
                Destroy(gameObject);
                GameManager.gm.AsteroidIncrement(1);
            }
        }
        if (other.tag == "UltBeam")
        {
            hp -= Global.ULTBEAM_DAMAGE;
            if (hp <= 0)
            {
                Destroy(gameObject);
                GameManager.gm.AddToScore(score);
            }
        }
    }
    public void LoadStats()
    {
        hp = Global.ASTEROIDBIG_HP;
        speed = Global.ASTEROIDBIG_SPEED;
        score = Global.ASTEROIDBIG_SCORE;
        damage = Global.ASTEROIDBIG_DAMAGE;
    }    
}