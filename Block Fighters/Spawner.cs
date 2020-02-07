using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float startWait = 3;
    public float spawnCount = 0;
    public float hazardCount = 3;
    public float spawnWait = 1;
    public float waveWait = 10;
    public GameObject[] enemy = new GameObject[4];
    public string enemyTag;
    public Vector2 spawnPosition;

    private static Spawner _instance;
    static public Spawner spawner
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Spawner>();
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        hazardCount = 3;
        spawnPosition = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {                
                int x = Random.Range(0, enemy.Length);
                switch (x)
                {
                    case 0:
                        GameObject o = Instantiate(enemy[0], spawnPosition, Quaternion.identity);
                        o.GetComponent<Enemy>().speed = 2;
 
                        break;
                    case 1:
                        GameObject p = Instantiate(enemy[1], spawnPosition, Quaternion.identity);
                        p.GetComponent<Enemy>().speed = 2;

                        break;
                    case 2:
                        GameObject q = Instantiate(enemy[2], spawnPosition, Quaternion.identity);
                        q.GetComponent<Enemy3>().speed = 2;

                        break;
                    case 3:
                        GameObject r = Instantiate(enemy[3], spawnPosition, Quaternion.identity);
                        r.GetComponent<Enemy3>().speed = 3;

                        break;
                }
                yield return new WaitForSeconds(Random.Range(2, 4)) ;
            }
            yield return new WaitForSeconds(Random.Range(2,5));//waveWait);
        }
    }
}
