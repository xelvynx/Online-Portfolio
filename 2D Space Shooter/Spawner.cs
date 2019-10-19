using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public Vector2 spawnValues = new Vector2(5,0);
    public GameObject asteroidBig;
    public GameObject enemy;
    public GameObject powerUp;
    public GameObject healUp;
    public GameObject asteroidSmall;

    public float StartWait = 1f;
    public float SpawnWait = 3f;
    public float WaveWait = 1f;
    float astCount = 2;
    float smallAstCount = 12;
    public int healCount = 1;
    public int powerCount = 1;

    public float healTime = 2f;
    public float powerTime = 2f;

    public float randomTime;
    public float randomSec;
    //private float randomTime;

        //randomTime = Random.Range(.5f, 3f);

    void Start () {
        StartCoroutine("asteroidSpawnWaves");
        StartCoroutine("PowerUpSpawnWaves");
        StartCoroutine("asteroidSmallSpawnWaves");
        StartCoroutine("HealSpawnWaves");
        randomSec = 1f;
        randomTime = Random.Range(randomSec, 2f);
    }
    void Update()
    {
        if (GameManager.gm.score % 3000 == 0 && GameManager.gm.score != 0 && GameManager.gm.score != GameManager.gm.prevScore)
            SpawnInc();
    }
    IEnumerator asteroidSpawnWaves()
    {
        yield return new WaitForSeconds (1.5f); 												//Wait for Seconds before start the wave

        //Infinite Loop
        while (GameManager.gm.gameIsOver == !true)
        {
            //Spawn Specific number of Objects in 1 wave
            for (int i = 0; i < astCount; i++)
            {
                Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x),4.5f);		//Random Spawn Position
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (asteroidBig, spawnPosition, spawnRotation); 									//Instantiate Object
                yield return new WaitForSeconds /*(Random.Range(1f,5f)*/(SpawnWait); 													//Wait for seconds before spawning the next object
            }
            yield return new WaitForSeconds (WaveWait); 														//wait for seconds before the next wave
        }
    }
    IEnumerator asteroidSmallSpawnWaves() 
    {
        yield return new WaitForSeconds(3f); 															//Wait for Seconds before start the wave

        //Infinite Loop
        while (GameManager.gm.gameIsOver == !true)
        {
            //Spawn Specific number of Objects in 1 wave
            for (int i = 0; i < 4; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), 4.5f);		//Random Spawn Position
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(asteroidSmall, spawnPosition, spawnRotation); 									//Instantiate Object
                yield return new WaitForSeconds /*(Random.Range(1f,5f)*/(randomTime); 													//Wait for seconds before spawning the next object
            }
            yield return new WaitForSeconds(2f); 														//wait for seconds before the next wave
        }
    }


    IEnumerator PowerUpSpawnWaves() 
    {
        yield return new WaitForSeconds(4.25f); 															//Wait for Seconds before start the wave

        //Infinite Loop
        while (GameManager.gm.gameIsOver == !true)
        {
            //Spawn Specific number of Objects in 1 wave
            for (int i = 0; i < powerCount; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), 4.5f);		//Random Spawn Position
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(powerUp, spawnPosition, spawnRotation); 									//Instantiate Object
                yield return new WaitForSeconds /*(Random.Range(1f,5f)*/(powerTime); 													//Wait for seconds before spawning the next object
            }
            yield return new WaitForSeconds(8); 														//wait for seconds before the next wave
        }
    }
    IEnumerator HealSpawnWaves() 
    {
        yield return new WaitForSeconds(6.75f); 															//Wait for Seconds before start the wave

        //Infinite Loop
        while (GameManager.gm.gameIsOver == !true)
        {
            //Spawn Specific number of Objects in 1 wave
            for (int i = 0; i < 1; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), 4.5f);		//Random Spawn Position
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(healUp, spawnPosition, spawnRotation); 									//Instantiate Object
                yield return new WaitForSeconds /*(Random.Range(1f,5f)*/(SpawnWait); 													//Wait for seconds before spawning the next object
            }
            yield return new WaitForSeconds(25); 														//wait for seconds before the next wave
        }
    }
    public void EnemySpawn()
    {
            Debug.Log("EnemySpawn");
            Instantiate(enemy, new Vector2(0, 4.5f), Quaternion.identity);
            GameManager.gm.timeToBeat = GameManager.gm.currentTime - GameManager.gm.timeMod;
            StopCoroutine("asteroidSpawnWaves");
            StopCoroutine("asteroidSmallSpawnWaves");
    }

    public void StartSpawn()
    {
        StartCoroutine("asteroidSpawnWaves");
        StartCoroutine("HealSpawnWaves");
        StartCoroutine("PowerUpSpawnWaves");
        StartCoroutine("asteroidSmallSpawnWaves");

    }
    public void SpawnInc()
    {
        astCount ++;
        smallAstCount++;
        WaveWait -= .05f;
        SpawnWait -= .05f;
        if (WaveWait <= .5f)
            WaveWait = .5f;
        if (SpawnWait <= .5f)
            SpawnWait = .5f;
        if (smallAstCount >= 20)
            smallAstCount = 20;
        if (astCount >= 10)
            astCount = 10;
        randomSec -= .025f;
        if (randomSec <= .5f)
            randomSec = .5f;
        

    }
}