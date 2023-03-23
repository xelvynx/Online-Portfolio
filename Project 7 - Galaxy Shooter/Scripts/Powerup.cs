using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    //ID for powerups
    //0 = triple shot
    //1 = speed
    //2 = shield

    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _powerupAudioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerupAudioClip, transform.position);
            if (player != null)
            {
                switch (_powerupID) 
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldBoostActive();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }  
            }
            Destroy(this.gameObject);
        }
    }
}
