using UnityEngine;
using System.Collections;

//public class Boss : Enemy
//{
//    public float hp = Enemy.hp
//    void Start()
//    {
//        hp *= GameManager.gm.bossMultiplier;
//        damage *= GameManager.gm.bossMultiplier;
//    }
//}

public class Enemy : MonoBehaviour {
    public GameObject projectile;
    public float speed = 3f;
    public float fireRate;
    public float nextFire;
    public float damage;
    public float hp;
    public int score;
    public int currency;
    public float randomWait;
    public float randomWaitRange;
    public Vector3 randomPos;

    private bool updatePos = true;

    void Start()
    {
        StartCoroutine("EnemyMovement");
        LoadStats(PlayerPrefs.GetInt("Diff"));

        hp *= GameManager.gm.bossMultiplier;
        damage *= GameManager.gm.bossMultiplier;
        fireRate -= .1f * GameManager.gm.bossMultiplier;
        speed *= GameManager.gm.bossMultiplier;


        //randomWaitRange -= 0f;
        randomWait = Random.Range(1, 3);
        
        projectile = (GameObject)Resources.Load(Global.PATH_ENEMY_LASER);

        if (GameManager.gm.player == null)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, transform.position, transform.rotation);
        }
        if (GameManager.gm.gameIsOver == true) 
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject);
            hp -= GameManager.gm.player.GetComponent<Player>().Damage;
            if (hp <= 0)
            {
                GameManager.gm.CurrencyIncrement();
                Destroy(gameObject);
                //GameManager.gm.score += 10;
               
                GameManager.gm.currentTime += 10.0f;
                GameManager.gm.AddToScore(score);
                GameObject.FindObjectOfType<Spawner>().StartSpawn();
            }
        }
        if (other.tag == "UltBeam") 
        {
            hp -= Global.ULTBEAM_DAMAGE;
            if (hp <= 0) 
            {

                GameManager.gm.CurrencyIncrement();
                Destroy(gameObject);
                //GameManager.gm.score += 10;
                GameManager.gm.currentTime += 10.0f;
                GameManager.gm.AddToScore(score);
                GameObject.FindObjectOfType<Spawner>().StartSpawn();
            }
        }
        if (other.tag == "HomingMissile") 
        {
            hp -= 15;
            Destroy(other.gameObject);
            if (hp <= 0)
            {
                GameManager.gm.CurrencyIncrement();
                Destroy(gameObject);
                //GameManager.gm.score += 10;
                GameManager.gm.currentTime += 10.0f;
                GameManager.gm.AddToScore(score);
                GameObject.FindObjectOfType<Spawner>().StartSpawn();
            }
        }
    }
    IEnumerator EnemyMovement()
    {
        yield return new WaitForSeconds(1);
        
        while (true)
        {
            yield return new WaitForSeconds(randomWait);
            if (updatePos)
            {
                randomPos.x = Random.Range(-7, 7);
                randomPos.y = Random.Range(-1.5f, 1.5f) ;
                randomPos.z = 0;
                updatePos = false;
            }

            Vector3 targetPos = transform.position + randomPos; 
            targetPos.x = Mathf.Clamp(targetPos.x, -7, 7);
            targetPos.y = Mathf.Clamp(targetPos.y, 3, 4.5f);
            while (Vector3.Distance(transform.position,targetPos) > 0.01f)
            {
                //yield return null;
                //yield return new WaitForSeconds(2);
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);//Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);//Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            updatePos = true;
            //yield return new WaitForSeconds(2);
        }
    }
    public void LoadStats(int diff) 
    {
        switch (diff)
        {
            case 1:
                hp = Global.ENEMY_HP;
                fireRate = Global.ENEMY_FIRERATE;
                damage = Global.ENEMY_DAMAGE;
                speed = Global.ENEMY_SPEED;
                score = 0;
                currency = Global.ENEMY_CURRENCY;
                break;
            case 2 :
                hp = Global.ENEMY_HP *2f;
                fireRate = Global.ENEMY_FIRERATE*1.5f;
                damage = Global.ENEMY_DAMAGE;
                speed = Global.ENEMY_SPEED*1.5f;
                score = 0;
                currency = Global.ENEMY_CURRENCY*2;
                break;
        }
            
    }
}