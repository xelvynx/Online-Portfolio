using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform shotSpawn;
    public GameObject projectile;
    public GameObject ultBeam;
    public GameObject ani1;
    public GameObject homing;
    
    
    private float startTime = 0f;
    private float shotTime = 0f;
    public float shotCount = 0;
    
    public float touchStart = 0f;
    
   
    public float currentHealth;
   
    public int ultimate = 0;
    public int counter = 0;
    public int multiplier = 1;

    public int speed = 5;
    public int maxHealth = 5;
    public int shotCountMax = 10;
    public int damage = 2;
    public float cdRate = 1f;
    public float fireRate = .6f;
    
    public float minSwipeDistY = 0f;
 
    public float minSwipeDistX;
         
    private Vector2 startPos1;

    public bool ultimateUsed;
    
    public bool isSwipe = false;
    public bool ultimateActive = false;

    public GameObject ultBeamAni;

    private string ult1;
    private string ult2;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    //Test Variables
    public float comfortZone = 70.0f;
    public float minSwipeDist = 14.0f;
    public float maxSwipeTime = 0.5f;

    private Vector2 startPos;
    private bool couldBeSwipe;

    Camera cam;

    float dist;
    float leftBorder;
    float rightBorder;

    public enum SwipeDirection
    {
        None,
        Up,
        Down
    }

    public SwipeDirection lastSwipe = SwipeDirection.None;
    public float lastSwipeTime;
    public Vector3 clampage;
    public float Damage
    {
        get
        {
            return damage * multiplier;
        }
    }

    //manipulate object's mass for drag resistance upgrade
    void Awake() 
    {
        LoadStats(PlayerPrefs.GetInt("Diff"));
    }

    public void Start() 
    {
        clampage = gameObject.transform.position;
        
        currentHealth = maxHealth;
        
        Input.multiTouchEnabled = true; //enabled Multitouch


        //dist = (transform.position - Camera.main.transform.position).z;
        //leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        //rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;



    }

  

    void FixedUpdate()
    {
        
        clampage.x = Mathf.Clamp(clampage.x, -7, 7);
        if (Time.time - shotTime >= cdRate)
            ShotCD();
#if UNITY_ANDROID
        if (Input.touchCount > 1 && Input.touchCount < 3 && Time.time - startTime >= fireRate)
            FireBullet();

        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).position.x >= Screen.width / 2.0 && transform.position.x < 8)
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * speed, Time.deltaTime);

        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).position.x < Screen.width / 2.0 && transform.position.x > -8)
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * speed, Time.deltaTime);
        SwipeTest();
#endif

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            
        
        
        
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
                FireBullet();

            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -7)
            
                // rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode2D.Impulse);
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * speed, Time.deltaTime);

            
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 7)
            
                //transform.Translate(Vector3.right * speed * Time.deltaTime);
                //rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode2D.Impulse);
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * speed, Time.deltaTime);
            
            if (Input.GetKey(KeyCode.Space) && Time.time - startTime >= fireRate)
                FireBullet();

            if (Input.GetKeyUp(KeyCode.Z) && ultimate > 0)
            {
                ult1 = PlayerPrefs.GetString("Ult1");

                switch (ult1)
                {
                    case "Destroy":
                        StartCoroutine("DestroyAll");
                        break;
                    case "UltBeam":
                        StartCoroutine("UltBeam");
                        break;
                    case "Homing":
                        StartCoroutine("HomingMissile");
                        break;
                }
            }
#endif
        }
    

    void ShotCD() 
    {
        shotTime = Time.time;
        shotCount -= 1;
        if (shotCount <= 0)
            shotCount = 0;
    }
    void FireBullet()
    {
        if (shotCount < shotCountMax && Time.time - startTime >= fireRate)
        {
            startTime = Time.time;
            shotCount += 1;
            Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "EnemyShot" )
        {
            Destroy(other.gameObject);
            currentHealth -= Global.ENEMY_DAMAGE;

            counter = 0;
            multiplier = 1;
            if (ultimate == 0) ultimate -= 0;
            if (ultimate < 4 && ultimate > 0) ultimate -= 1;

        }
        if (other.tag == "Asteroid")
        {
            currentHealth -= Global.ASTEROIDBIG_DAMAGE;
            Destroy(other.gameObject);
            Debug.Log("Damage");
            counter = 0;
            multiplier = 1;
            if (ultimate == 0) ultimate -= 0;
            if (ultimate < 4 && ultimate > 0) ultimate -= 1;
        }
        if (other.tag == "AsteroidSmall")
        {
            currentHealth -= Global.ASTEROIDSMALL_DAMAGE;
            Destroy(other.gameObject);

            counter = 0;
            multiplier = 1;
            if (ultimate == 0) ultimate -= 0;
            if (ultimate < 4 && ultimate > 0) ultimate -= 1;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            DestroyAll();
        }
    }

    public void IncrementCounter()
    {
        counter++;
        if (counter != 0 && counter % 3 == 0 && multiplier < 8) multiplier *= 2 ;
        if (counter != 0 && counter % 3 == 0 && ultimate < 4) ultimate++;
    }


    public IEnumerator DestroyAll() 
    {
        if (ultimate > 0 && ultimateActive == false)
        {
            ultimate = ultimate - 1;
            ultimateActive = true;

            Instantiate(ani1, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Asteroid");
            GameObject[] objects4 = GameObject.FindGameObjectsWithTag("AsteroidSmall");

            for (int i = 0; i < objects.Length; i++)
            {
                Destroy(objects[i]);
            }
            for (int i = 0; i < objects4.Length; i++)
            {
                Destroy(objects4[i]);
            }
            yield return new WaitForSeconds (3f);
            ultimateActive = false;
            yield break;
        }
    }
    IEnumerator UltBeam() 
    {        
        if(ultimate >= 2 && ultimateActive == false)
        {
            
            ultimate = ultimate - 2;
            ultimateActive = true;
            Instantiate(ultBeamAni, shotSpawn.position, shotSpawn.rotation);
            yield return new WaitForSeconds(1f);
            Instantiate(ultBeam, shotSpawn.position , shotSpawn.rotation);
            yield return new WaitForSeconds(3f);
            ultimateActive = false;
            yield break;
        }
    }

    IEnumerator HomingMissile() 
    {
        if (ultimate >= 1 && ultimateActive == false && GameObject.Find("Enemy(Clone)") != null) 
        {
            ultimate = ultimate - 1;
            ultimateActive = true;
            for (int i = 0; i < 5; i++)
            {
                Instantiate(homing, shotSpawn.position, shotSpawn.rotation);
                yield return new WaitForSeconds(1);
            }
            yield break;
        }

    }
    void LoadStats(int i)
    {
        switch (i)
        {
            case 1:
                shotCountMax = shotCountMax + PlayerPrefs.GetInt("AmmoCount");
                maxHealth = 5 + PlayerPrefs.GetInt("MaxHP");
                damage = damage + PlayerPrefs.GetInt("DMG");
                speed = speed + PlayerPrefs.GetInt("SPD");
                fireRate = fireRate - PlayerPrefs.GetFloat("FireRate");
                cdRate = cdRate - PlayerPrefs.GetFloat("CDR");
                break;
            case 2:
                shotCountMax = shotCountMax + PlayerPrefs.GetInt("AmmoCount");
                maxHealth = 3 + PlayerPrefs.GetInt("MaxHP");
                damage = damage + PlayerPrefs.GetInt("DMG");
                speed = speed + PlayerPrefs.GetInt("SPD");
                fireRate = fireRate - PlayerPrefs.GetFloat("FireRate");
                cdRate = cdRate - PlayerPrefs.GetFloat("CDR");
                break;
        }
    }
    void SwipeTest()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastSwipe = SwipeDirection.None;
                    lastSwipeTime = 0;
                    couldBeSwipe = true;
                    startPos = touch.position;
                    startTime = Time.time;
                    break;
                case TouchPhase.Moved:
                    if (Mathf.Abs(touch.position.x - startPos.x) > comfortZone)
                    {
                        couldBeSwipe = false;
                        FireBullet();
                    }
                    break;
                case TouchPhase.Ended:
                    if (couldBeSwipe)
                    {
                        float swipeTime = Time.time - startTime;
                        float swipeDist = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        if (swipeTime < maxSwipeTime && swipeDist > minSwipeDist)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                            if (swipeValue > 0)
                            {
                                lastSwipe = SwipeDirection.Up;
                                ult1 = PlayerPrefs.GetString("Ult1");
                                switch (ult1)
                                {
                                    case "Destroy":
                                        StartCoroutine("DestroyAll");
                                        break;
                                    case "UltBeam":
                                        StartCoroutine("UltBeam");
                                        break;
                                    case "Homing":
                                        StartCoroutine("HomingMissile");
                                        break;
                                }
                                startTime = Time.time;
                                StartCoroutine("UltBeam");
                            }
                            else if (swipeValue < 0)
                            {
                                lastSwipe = SwipeDirection.Down;
                                ult2 = PlayerPrefs.GetString("Ult2");
                                switch (ult2)
                                {
                                    case "Destroy":
                                        StartCoroutine("DestroyAll");
                                        break;
                                    case "UltBeam":
                                        StartCoroutine("UltBeam");
                                        break;
                                    case "Homing":
                                        StartCoroutine("HomingMissile");
                                        break;
                                }
                                startTime = Time.time;
                            }
                            lastSwipeTime = Time.time;
                            Debug.Log("Found a swipe!  Direction: " + lastSwipe);
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator SpriteBlink()
    {
        gameObject.
        break;
    }
}