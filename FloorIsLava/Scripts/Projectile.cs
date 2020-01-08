using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        
    }
    
}
