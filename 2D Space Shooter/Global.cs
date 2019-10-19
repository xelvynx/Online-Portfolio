using UnityEngine;
using System.Collections;

public static class Global {
    public static string PATH_ENEMY_LASER = "Bullets/EnemyLaser";
    public static string PATH_PLAYER = "Player";

    public static int CURRENCY;
    public static int ULTBEAM_DAMAGE = 10;
    //Player
    public static float PLAYER_DAMAGE = 1;

    //Asteroid 1
    public static int ASTEROIDBIG_SCORE = 100;
    public static int ASTEROIDBIG_DAMAGE = 1;
    public static int ASTEROIDBIG_HP = 1;
    public static float ASTEROIDBIG_SPEED = 1f;
    
    //Asteroid 2
    public static int ASTEROIDSMALL_SCORE = 100;
    public static int ASTEROIDSMALL_DAMAGE = 1;
    public static int ASTEROIDSMALL_HP = 1;
    public static float ASTEROIDSMALL_SPEED = 1.2f;

    //Powerup/Comboup
    public static float POWERUP_HEALTH_BONUS = 10f;
    public static float POWERUP_MULTIPLIER = 2f;


    //Enemy
    public static float ENEMY_HP = 2;
    public static float ENEMY_DAMAGE = 2f;
    public static int ENEMY_SPEED = 1;
    public static float ENEMY_FIRERATE = 2.2f;
    public static float ENEMY_NEXTFIRE = 0.0f;
    public static int ENEMY_SCORE = 100;
    public static int ENEMY_CURRENCY = 40;
    //Heal
    public static float HEAL_HEAL = 1f;
}

//public class Asteroid : MonoBehaviour
//{
//    private const int SPEED = 10;

//    public int hp = 10f;

//    void Awake()
//    {
//        hp = Global.DEFAULT_ASTEROID_HP;
//    }
//}
//GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().damage