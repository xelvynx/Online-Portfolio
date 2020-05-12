using UnityEngine;
using System.Collections;

public class AsteroidSmall : MonoBehaviour {
    public float speed;
    public float hp;
    public int score;
    public float damage;
	// Update is called once per frame
    void Start() 
    {
        LoadStats(PlayerPrefs.GetInt("Diff"));
        hp += GameManager.gm.asteroidMultiplier;
        speed += GameManager.gm.asteroidMultiplier;
        if (GameManager.gm.player == null)
        {
            Destroy(gameObject);
        }
    }
	void Update ()
    {        
        transform.Translate (Vector2.down * speed * Time.deltaTime);
        //if (GameObject.Find("Enemy(Clone)") != null)
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
    public void LoadStats(int diff) 
    {
        switch (diff)
        {
            case 1:
                hp = Global.ASTEROIDSMALL_HP;
                speed = Global.ASTEROIDSMALL_SPEED;
                score = Global.ASTEROIDSMALL_SCORE;
                damage = Global.ASTEROIDSMALL_DAMAGE;
                break;
            case 2:
                hp = Global.ASTEROIDSMALL_HP;
                speed = Global.ASTEROIDSMALL_SPEED;
                score = Global.ASTEROIDSMALL_SCORE;
                damage = Global.ASTEROIDSMALL_DAMAGE;
                break;
        }
    
    }
}
