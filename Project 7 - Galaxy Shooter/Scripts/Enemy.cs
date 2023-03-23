using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    private Player _player;
    private Animator _animator;
    private Collider2D _collider2D;
    private AudioSource _audioSource;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private AudioClip _explosionSoundClip;

    private float _fireRate;
    private float _canFire;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AUDIO SOURCE IS NULL");
        }
        else { _audioSource.clip = _laserSoundClip; }
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL!");
        }
        if (_collider2D == null)
        {
            Debug.LogError("Collider is NULL!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.4)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 7.4f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                StartCoroutine(PlayDeathAnim());
            }

        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore();
            }
            StartCoroutine(PlayDeathAnim());

        }
    }
    IEnumerator PlayDeathAnim()
    {
        AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
        _collider2D.enabled = false;
        _animator.SetTrigger("OnEnemyDeath");
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }
    private void FireLaser()
    {
        _audioSource.Play();
        _fireRate = Random.Range(3f, 7f);
        _canFire = Time.time + _fireRate;
        GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();
        }

    }

}
