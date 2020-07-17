using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AttackInfo atkInfo;
    public SO_Skills skill;
    public int speed;
    public void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    public void SetProjectile(AttackInfo a,SO_Skills s) 
    {
        atkInfo = a;
        skill = s;
        speed = s.speed;

    }
}
