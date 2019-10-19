using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {
    private float speed = 5f;
    private GameObject enemy;
	// Use this for initialization
	// Update is called once per frame
	void Update () 
    {
        GameObject enemy = GameObject.Find("Enemy(Clone)");
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
	}
}
