using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {
    public float speed = 5.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (GameObject.Find("Enemy(Clone)") == null)
            Destroy(gameObject);
    }
    
}
