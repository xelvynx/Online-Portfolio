using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {
    public float speed;
    public  Transform target;

    public Transform target1;
    public Transform target2;
    public float rotateSpeed = 500f;
    private Vector2 direction;
    public GameObject bosss;

    // Use this for initialization
    void Start()
    {
        target1 = GameObject.Find("Player").transform;
        bosss = GameObject.Find("Boss");
        if(bosss!= null) 
        {
            target2 = bosss.transform;
        }
        //Debug.Log(bosss.activeInHierarchy);
        target = target1;
        Destroy(gameObject, 15);
        //direction = (Vector2)target.position - rb.position;
    }
    private void Update()
    {

            if (target == target1)
            {
                transform.position = Vector2.MoveTowards(transform.position, target1.position, speed * Time.deltaTime);
                //direction = Vector2.left;
                //direction.Normalize();
                //transform.Translate(direction * Time.deltaTime * speed);
                //float rotateAmount = Vector3.Cross(direction, transform.up).z;
                //rb.angularVelocity = rotateAmount * rotateSpeed;
                //rb.velocity = transform.up * speed;
                //Debug.Log(direction);
            }
            if (target == target2)
            {
                //Vector2 direction = Vector2.right;
                //direction.Normalize();
                //transform.Translate(direction * Time.deltaTime * speed);
                if (bosss == null) { Destroy(gameObject); } 
                transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
                //float rotateAmount = Vector3.Cross(direction, transform.up).z;
            }
        

    }
    public void SetTarget() 
    {
        target = target2;
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Counter" && Player.play.redPress)
        {
            if (bosss == null) { Destroy(gameObject); }
            else if (bosss.activeSelf)
            {
                target = target2;
                Debug.Log("Enemy Destroyed");
                //Destroy(gameObject);
            }
            
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            target = null;
        }
        //else { Destroy(gameObject); }
    }
}
