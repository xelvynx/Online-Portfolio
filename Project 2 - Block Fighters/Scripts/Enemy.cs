
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Color color;
    public Player player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player = player.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Hit");
        if (other.gameObject.tag == "Counter")
        {
            Debug.Log(player.bluePress);
            if ((Player.play.bluePress && gameObject.tag == "Enemy1" || Input.GetKeyDown(KeyCode.Z) && gameObject.tag == "Enemy1")
                || (Player.play.yellowPress && gameObject.tag == "Enemy2")|| (Input.GetKeyDown(KeyCode.X) && gameObject.tag == "Enemy2"))  
            {
                //Debug.Log("3");
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

        if (other.gameObject.tag == "Player")
        {
            if ((Player.play.bluePress && gameObject.tag == "Enemy1" || Input.GetKeyDown(KeyCode.Z) && gameObject.tag == "Enemy1")
                || (Player.play.yellowPress && gameObject.tag == "Enemy2") || (Input.GetKeyDown(KeyCode.X) && gameObject.tag == "Enemy2"))
            {
                GameManager.gm.Score(1);
                Destroy(gameObject);
            }
            //if ((other.gameObject.GetComponent<Player>().blueN >= 1 && gameObject.tag == "Enemy1")
            //    || (other.gameObject.GetComponent<Player>().yellowN >= 1 && gameObject.tag == "Enemy2"))
            //{
            //    Destroy(gameObject);
            //    GameManager.gm.Score(1);
            //}
        }

        if (other.gameObject.tag == "Counter")
        {
            if ((Player.play.bluePress && gameObject.tag == "Enemy1" || Input.GetKeyDown(KeyCode.Z) && gameObject.tag == "Enemy1")
                || (Player.play.yellowPress && gameObject.tag == "Enemy2") || (Input.GetKeyDown(KeyCode.X) && gameObject.tag == "Enemy2"))

            {
                GameManager.gm.Score(1);
                Destroy(gameObject);
            }
        }
    }


}
