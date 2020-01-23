﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{

    public BoxCollider2D hitBox;
    public GameObject hocusPokeusPrefab;
    public GameObject slashPrefab;
    //public GameObject dashPrefab;
    public GameObject beamPrefab;
    public GameObject spinslashPrefab;
    public GameObject superbeamPrefab;
    public GameObject crosshair;
    public GameObject cam;
    public Animator animator;

    float dashDistance = 2.5f;

    private Vector2 movement;
    public float movSpd = 8f;
    public float curHealth = 100;
    public float maxHealth = 100;
    public GameObject healthBar;

    public bool dmgPossible;

    //SPRITE VARIABLES
    private string spriteNames = "dash";
    private int spriteVersion = 0;
    public SpriteRenderer spriteR;
    private Sprite[] sprites;
    Collider2D coll;

    private short count = 0;
    private bool crshrKey = false;

    //Testing Aim Variables
    Vector3 aim;
    Vector2 mouse;
    Vector2 direction;
    public bool attacked = false;

    public LayerMask whatIsEnemies;
    public Transform attackPos;
    public float attackRange;
    public bool dashAttack = false;
    bool playsound = false;
    [HideInInspector]
    GameManager gm;

    [HideInInspector]
    public Vector3 UIoffset;
    [HideInInspector]
    public Vector3 camoffset;
    [HideInInspector]
    public Vector3 move;
    [HideInInspector]
    public Vector2 point;

    Vector3 shrnk = new Vector3(.1f, .1f, .1f);
  

    public Transform[] telepoint;
    Pits[] p0;
    Pits1[] p1;
    Pits2[] p2;
    Pits3[] p3;
    
    // Start is called before the first frame update
    void Start()
    {
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
        spriteR = GetComponentInChildren<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        camoffset = cam.transform.position;
        //SetHealthBar(maxHealth);
        gm = GameManager.gm;
        p0 = FindObjectsOfType<Pits>();
        p1 = FindObjectsOfType<Pits1>();
        p2 = FindObjectsOfType<Pits2>();
        p3 = FindObjectsOfType<Pits3>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.pauseGame();
        }
        if (Time.timeScale > 0)
        {
            //Aiming
            aim = Input.mousePosition;
            aim = Camera.main.ScreenToWorldPoint(aim);
            mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
            direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
            //movement for player
            move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            transform.position += ((movSpd * move) * Time.deltaTime);

            //Animations
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Magnitude", move.magnitude);
            //sound
            movesound();

            aimCrosshair();
            for (int pp3 = 0; pp3 < p3.Length; pp3++)
            {
                if (p3[pp3].point3 == true)
                {
                    StartCoroutine(tele(3));
                    p3[pp3].point3 = false;
                }
            }
            for (int pp2 = 0; pp2 < p2.Length; pp2++)
            {
                if (p2[pp2].point2 == true)
                {
                    StartCoroutine(tele(2));
                    p2[pp2].point2 = false;
                }
            }
            for (int pp1 = 0; pp1 < p1.Length; pp1++)
            {
                if (p1[pp1].point1 == true)
                {
                    StartCoroutine(tele(1));
                    p1[pp1].point1 = false;
                }
            }
            for (int pp0 = 0; pp0 < p0.Length; pp0++)
            {
                if (p0[pp0].point0 == true)
                {
                    StartCoroutine(tele(0));
                    p0[pp0].point0 = false;
                }
            }   

        }
        
    }
    IEnumerator tele(int p)
    {
        this.transform.position-= new Vector3(0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        this.transform.position -= new Vector3(0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        this.transform.position -= new Vector3(0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        this.transform.position -= new Vector3(0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        StartCoroutine(shrink());
        movSpd = 0;       
        yield return new WaitForSeconds(.36f);
        this.transform.position = telepoint[p].position;
        this.GetComponent<Transform>().localScale = new Vector3(1,1,1);
        DecreaseHealth(20); 
        movSpd = 8;
    }
    IEnumerator shrink()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        for (int s = 0; s < 4; s++)
        {
            this.GetComponent<Transform>().localScale -= shrnk;
            yield return new WaitForSeconds(.06f);
        }
        
        
    }
    private void movesound()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            FindObjectOfType<AudioManager>().Play("playerWalk");
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            FindObjectOfType<AudioManager>().Stop("playerWalk");


    }
    public IEnumerator AttackRelease(float t)
    {
        //INSERT SLOW/STOP PLAYER MESS AROUND WITH NUMBERS
        movSpd = .25F;
        yield return new WaitForSeconds(.4f);
        movSpd = 8f;
        yield return new WaitForSeconds(t);
        attacked = false;
        
    }

    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector2 mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        Vector2 direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);


        if (mouse.magnitude > 0)
        {
            mouse.Normalize();
            mouse *= 1.12f;
            crosshair.transform.localPosition = mouse;
            direction.Normalize();
            

            //HOKUS-POKE-US
            if (Input.GetKeyDown(KeyCode.Mouse0) && attacked == false)
            {
                GameObject attack = Instantiate(hocusPokeusPrefab, transform.position,Quaternion.identity);
                
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.4f);
                attacked = true;
                StartCoroutine(AttackRelease(.38f));
                //isMove = true;
            }
        }
    }

    IEnumerator SpriteBlink()
    {
        cam.GetComponent<ScreenShake>().TriggerShake();

        spriteR.enabled = false;
        //cam.transform.position += Vector3.right;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = true;
        //cam.transform.position += Vector3.left;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = false;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = true;
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2f);
    }


    public void SetHealthBar(float f)
    {
        healthBar.transform.localScale = new Vector3(f, 1, 1);
    }
    
    public void Slash()
    {
        GameObject attack = Instantiate(slashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 0.51f);
    }
    public void Beam()
    {
        GameObject attack = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction.normalized * 22f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2);
    }
    public void Dash()
    {
        hitBox.tag = "Dash";
        animator.Play("Dash");
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        FindObjectOfType<AudioManager>().Play("Dash");
        StartCoroutine(DashAtt(direction, coll));

    }

    public void BoomerangDash()
    {
        hitBox.tag = "Dash";
        animator.Play("Dash");
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        FindObjectOfType<AudioManager>().Play("Dash");
        StartCoroutine(BoomerangDashCo(direction, coll));

    }
    public void SuperBeam()
    {
        GameObject attack = Instantiate(superbeamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction.normalized * 25f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2f);
    }
    public void SpinSlash()
    {
        StartCoroutine("SpinSlashAttack");
        GameObject attack = Instantiate(spinslashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, .6f);
    }

    public void DecreaseHealth(float f)
    {
        curHealth -= f;
        StartCoroutine("SpriteBlink");
        FindObjectOfType<AudioManager>().Play("OUCH");
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
        if (curHealth <= 0)
        {
            curHealth = 0;
            SceneManager.LoadScene(0);
        }

    }
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public IEnumerator SpinSlashAttack()
    {
        AttackRange(2);

        yield return new WaitForSeconds(.3F);
        AttackRange(2);
        yield return new WaitForSeconds(.3F);
        AttackRange(2);
        yield break;
    }

    public void AttackRange(float f)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, f, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponentInChildren<enemyAI>() == null) { return; }
            Debug.Log("Enemy" + i);
            enemiesToDamage[i].GetComponentInChildren<enemyAI>().takeDamage(25);

        }
    }
    IEnumerator DashAtt(Vector3 direction, Collider2D coll)
    {
        transform.GetChild(0).Rotate(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        GetComponent<Rigidbody2D>().velocity = direction.normalized * 25;
        gameObject.layer = 9; //Dash layer
        hitBox.tag = "Dash";
        yield return new WaitForSeconds(.3f);
        transform.GetChild(0).Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        gameObject.layer = 8; //Player 

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spriteVersion = 1;
        spriteVersion = 0;
        hitBox.tag = "Hitbox";
        dashAttack = false;
    }
    public IEnumerator BoomerangDashCo(Vector3 direction, Collider2D coll)
    {
        transform.GetChild(0).Rotate(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        GetComponent<Rigidbody2D>().velocity = direction.normalized * 50;
        gameObject.layer = 9; //Dash layer
        hitBox.tag = "Dash";
        yield return new WaitForSeconds(.2f);
        animator.Play("Dash");
        transform.GetChild(0).Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        GetComponent<Rigidbody2D>().velocity = -direction.normalized * 50;
        yield return new WaitForSeconds(.2f);
        gameObject.layer = 8; //Player 

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spriteVersion = 1;
        spriteVersion = 0;
        hitBox.tag = "Hitbox";
        dashAttack = false;
    }

}