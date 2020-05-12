using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    public float speed = 5f;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 3f);
        offset = new Vector3(0, .5f); // Set this to be however far away you want the beam from the player
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Asteroid")
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}