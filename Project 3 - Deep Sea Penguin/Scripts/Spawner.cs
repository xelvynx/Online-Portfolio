using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject obstacle;
    public GameObject jellyfish;
    public GameObject staminaPrefab;
    public GameObject obstacle1;

    private static Spawner _instance;
    public static Spawner _spawner
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Spawner").GetComponent<Spawner>();
            }

            return _instance;
        }
    }
    public int spawnCount = 0;
    public int maxSpawn = 8;
    public float obCount = 0;
    public float maxOb = 2;
    public int amtToSpawn = 0;

    public float maxSpawnDelay = 1.5f;
    public float obDelay;
    GameManager gm;
    void Start()
    {
        gm = GameManager.gm;
        maxOb = Configuration.maxObSpawn;
        maxSpawn = Configuration.maxBombSpawn;
        maxSpawnDelay = Configuration.maxSpawnDelay;
        obDelay = Configuration.obDelay;
    }


    IEnumerator ObstacleSpawn()
    {
        yield return new WaitForSeconds(obDelay);
        while (gm.gameStart == true)
        {
            if (obCount < maxOb)
            {
                yield return new WaitForSeconds(2);
                Instantiate(obstacle, new Vector3(Random.Range(-4.5f, 4.5f), transform.position.y + 3, 0), Quaternion.identity);
                obCount++;                
                yield return new WaitForEndOfFrame();
            }
            else 
            {
                yield return null;
                yield return new WaitForSeconds(2);
            }
        }


    }
    IEnumerator BombSpawn()
    {
        yield return new WaitForSeconds(1);
        while (gm.gameStart == true)
        {
            if(spawnCount < maxSpawn)
            {
                yield return new WaitForSeconds(Random.Range(.3f,maxSpawnDelay));
                spawnCount++;
                Instantiate(jellyfish, new Vector3(Random.Range(-4.5f, 4.5f), transform.position.y, 0), Quaternion.identity);
                yield return new WaitForEndOfFrame();
                //Debug.Log(spawnCount);
            }                        
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator StaminaSpawn()
    {
        yield return new WaitForSeconds(2);//eventually, increase the delay making it more difficult, requiring you to be more precise about movements
        while (gm.gameStart == true)
        {            
            Instantiate(staminaPrefab, new Vector3(Random.Range(-4.5f, 4.5f), transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);            
        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }

}
