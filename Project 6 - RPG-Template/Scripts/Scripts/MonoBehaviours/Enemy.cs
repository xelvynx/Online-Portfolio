using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        //InitializeStats(100, 3, 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(other.gameObject.GetComponent<Projectile>().atkInfo);//need to fix, not working yet
        }
        
        
    }

}
