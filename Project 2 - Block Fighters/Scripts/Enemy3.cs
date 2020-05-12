using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed;
    public Color color;
    public GameObject bullet;
    public Transform bulletTrans;
    public bool canFire;
    


    private void Start()
    {
        canFire = false;
        InvokeRepeating("Fire", 0, 2);
        Destroy(gameObject, 15);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (canFire == true)
            InvokeRepeating("Fire", 0, 2);
        else { }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Counter")
        {
            if (Player.play.redPress || Input.GetKeyDown(KeyCode.C))

            {
                GameManager.gm.Score(1);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
                Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.name == "Player")
        {
            if (Player.play.redPress || Input.GetKeyDown(KeyCode.C))

            {
                GameManager.gm.Score(1);
                Destroy(gameObject);
            }
            //if (obj.GetComponent<Player>().redN >= 1)
            //{
            //    GameManager.gm.Score(1);
            //    Destroy(gameObject);
            //}
        }
        else
        {

        }
    }
    void Fire()
    {
        GameObject obj;
        obj = Instantiate(bullet, bulletTrans.position, Quaternion.identity);
        obj.GetComponent<Bullets>().speed = 4;
        Debug.Log("Fire");
    }

}


