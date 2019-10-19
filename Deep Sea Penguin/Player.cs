using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    public float speed = 1; // speed in meters per second
    public Vector3 originalPosition;
    public bool buttonLift = true;
    public float releaseTime;
    public float reflectTime;
    Spawner sp;
    public float lerpNum;
    GameManager gm;


    public bool moveRight = false;
    public bool moveLeft = false;
    public Animator anim;

    public float slowTime;


    void Start()
    {
        anim = GetComponent<Animator>();
        originalPosition = new Vector3(0, 0, 0);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sp = Spawner._spawner.GetComponent<Spawner>();
        speed = gm.playerSpeed;
        
    }

    void Update()
    {
        releaseTime += Time.deltaTime;
        Movement2();

        Movement3();

        Movement4();
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        slowTime -= Time.deltaTime;
        if (slowTime <= 0)
        {
            subReset_Player_Speed();
            slowTime = 0;
        }
        Debug.Log(sp.maxOb);
    }


    void Movement2()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveLeft = false;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            moveRight = false;
    }
    void Movement4()
    {
        if (moveLeft == true)
        {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            gm.StaminaInfo(Time.deltaTime * gm.stamDegradeRate);
            anim.Play("Left", 0);
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
        }
        if (moveRight == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            gm.StaminaInfo(Time.deltaTime * gm.stamDegradeRate);
            anim.Play("Right", 0);
        }
        else if (moveLeft == false && moveRight == false)
        {
            anim.Play("Straight", 0);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), Time.deltaTime);
        }
    }
#if UNITY_ANDROID
    void Movement3()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 2)
            {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
                gm.StaminaInfo(Time.deltaTime * gm.stamDegradeRate);
                if (transform.position.x <= -4.5f)
                {
                    transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
                }
            }
            else if (touch.position.x > Screen.width / 2)
            {
                transform.Translate(Vector3.right * gm.playerSpeed * Time.deltaTime);
                gm.StaminaInfo(Time.deltaTime * gm.stamDegradeRate);
                if (transform.position.x >= 4.5f)
                {
                    transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
                }
            }
        }
    }
#endif
#if UNITY_EDITOR
    public void OnMouseDown(int i)
    {
        if (i == 2)
        {
            moveRight = true;
            anim.Play("Right", 0);
        }
        if (i == 1)
        {
            moveLeft = true;
            anim.Play("Left", 0);
        }
    }
    public void OnMouseUp(int i)
    {
        if (i == 2)
        {
            moveRight = false;
        }
        if (i == 1)
        {
            moveLeft = false;
        }
    }
#endif


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Stamina")
        {
            gm.currentStamina += 5;
            Debug.Log("It Worked.");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            gm.GameOver();
           
        }
        if (other.tag == "Bomb")
        {
            Destroy(other.gameObject);
            
            speed = speed*.8f;
            if (speed <= gm.playerSpeed / 4) { speed = gm.playerSpeed / 4; }
            slowTime += 2;
            if (slowTime >= 5) { slowTime = 5; }        
            sp.spawnCount -= 1;
            if (sp.spawnCount <= 0) sp.spawnCount = 0;
            Debug.Log("hit");
        }
        if (other.gameObject.tag == "Obstacle1")
        {
            gm.ScorePlus(1);
            sp.obCount --;                    
        }
    }
    void subReset_Player_Speed()
    {
        speed = gm.playerSpeed;
    }
}
