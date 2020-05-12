using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {
	public float speed = 5f;
	public GameObject enemy;
	// Update is called once per frame
    
    void Update () {
		transform.Translate (Vector2.up * Time.deltaTime * speed);

	}
}
