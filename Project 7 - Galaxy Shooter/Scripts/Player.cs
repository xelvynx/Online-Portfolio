using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedMultiplier = 2;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisual;

    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _isTripleShotActive = false;
    //[SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldBoostActive = false;
    [SerializeField] private int _score;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    [SerializeField] private GameObject _thruster;
    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _leftEngine;

    [Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSoundClip;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        GettingComponents();
    }
    public void GettingComponents()
    {
        _thruster = GameObject.Find("Thruster");
       
        _audioSource = GetComponent<AudioSource>();
        _rightEngine = GameObject.Find("Right_Engine");
        _leftEngine = GameObject.Find("Left_Engine");
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
        _thruster.SetActive(false);
        if (_spawnManager == null)
        {
            Debug.LogError("spawn manager is NULL!");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }
        if (_rightEngine == null)
        {
            Debug.LogError("Right_Engine == NULL!");
        }
        if (_leftEngine == null)
        {
            Debug.LogError("Left_Engine == NULL!");
        }
        if (_audioSource == null)
        {
            Debug.LogError("AUDIO SOURCE IS NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Optimized form, not necessarily optimal on performance
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //Clamps or prevents player from exceeding 0 or -3.8f
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EnableThrusters();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableThrusters();
        }
    }

    public void EnableThrusters()
    {
        _speed += 3;
        _thruster.SetActive(true);
    }
    public void DisableThrusters()
    {
        _speed -= 3;
        _thruster.SetActive(false);
    }
    private void FireLaser()
    {
        _audioSource.Play();
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldBoostActive)
        {
            _isShieldBoostActive = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives--;
        CheckEngine();
        _uiManager.UpdateLivesSprite(_lives);
        if (_lives < 1)
        {
            //Communicate with spawn manager to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void CheckEngine()
    {
        
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
            return;
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        //add a number of seconds to be tracked so you can have triple shot longer than 5 seconds
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        Invoke("DisableSpeedBoost", 5);
    }
    public void DisableSpeedBoost()
    {
        _speed /= _speedMultiplier;
    }
    public void ShieldBoostActive()
    {
        _isShieldBoostActive = true;
        _shieldVisual.SetActive(true);
    }
    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScoreText(_score);
    }

    

}
