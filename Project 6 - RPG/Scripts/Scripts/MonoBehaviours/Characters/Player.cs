using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Job job;
    public float strength =5;
    public int health;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        strength = strength + (strength * job.strMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
