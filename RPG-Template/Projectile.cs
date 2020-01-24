using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AttackInfo atkInfo;
    public Skill skill;
    public int speed;
    public void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    public void SetProjectile(AttackInfo a,Skill s) 
    {
        atkInfo = a;
        skill = s;
        speed = s.speed;

    }
}
