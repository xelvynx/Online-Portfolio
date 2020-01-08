using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private List<Collider2D> colliderList = new List<Collider2D>();
    public bool isLava = false;
    float lavaTransitionDelay = 1.5f;
    public float lavaDuration = 2;
    public bool lavaAnimationPlaying = false;
    public float timerForPlayer = 2;
    public bool canRun = true;
    bool hasObstacle = false;
    public LayerMask whatIsTile;
    public int checker = 0;
    public bool canPlace = true;
    public bool nullify = false;
    public bool attackAhead = false;
    private Color previousColor;
    private bool canAddPoints = true;
    public bool hasPlayer = false;
    public float playerStayDuration = 3;
    public float addDuration;
    public bool hasBeenStepped;
    public bool GetNullify() { return nullify; }

    void OnEnable()
    {
        checker = 0;
        whatIsTile = LayerMask.GetMask("Tile");
        sr = GetComponent<SpriteRenderer>();
        previousColor = sr.color;
        lavaDuration = 2;
        playerStayDuration = 1;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasPlayer = false;
        if (!lavaAnimationPlaying && !isLava && !hasObstacle)
        {
            //if (GameManager.gm.singlePlayer&& other.CompareTag("Player"))
            //{
            //    CancelInvoke();
            //    SpawnLava();
            //    return;
            //}
            if (other.CompareTag("Player"))
            {
                CancelInvoke();
                StartLava();

            }
        }
        hasBeenStepped = true;
    }
    public bool GetStepped()
    {
        return hasBeenStepped;
    }
    public bool GetObstacle() { return hasObstacle; } 
   
    public bool GetCanPlace() 
    {
        return canPlace;
    }
    public bool GetPlayer() { return hasPlayer; }
    void EnableAddPoints() { canAddPoints = true; }
    void CanStart()
    {
        canRun = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!UIManager.um.paused)
        {
            if (other.tag == "Player" && isLava)
            {
                if (canAddPoints)
                {
                    if (other.name == "Player1") UIManager.um.AddTeam2Points();
                    if (other.name == "Player2") UIManager.um.AddTeam1Points();
                    canAddPoints = false;
                }
                
                Invoke("EnableAddPoints", lavaDuration);
                other.gameObject.GetComponent<Player>().Respawn();
            }
            if (other.tag == "Player"&&GameManager.gm.singlePlayer == false)
            {
                hasPlayer = true;
                Invoke("StartLava", 3);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = true;
            if (nullify)
            {
                //Debug.Log("added nullify");
                other.GetComponent<Player>().AddNullify();
                ResetPowerUp();
            }
            if (attackAhead)
            {
                ////Debug.Log("Added attack ahead");
                other.GetComponent<Player>().AddAttackAhead();
                ResetPowerUp();
            }
            if (isLava)
            {
                if (other.name == "Player1") UIManager.um.AddTeam2Points();
                if (other.name == "Player2") UIManager.um.AddTeam1Points();
                other.gameObject.GetComponent<Player>().Respawn();

                //Debug.Log("Enter Death");
            }
        }
    }


    public IEnumerator StartingPositions()
    {
        yield return new WaitForEndOfFrame();
        Collider2D hit1 = Physics2D.OverlapCircle(transform.position + Vector3.left, .5f, whatIsTile);
        if (hit1 != null)
        {
            hit1.GetComponent<Tile>().canPlace = false;
        }
        Collider2D hit2 = Physics2D.OverlapCircle(transform.position + Vector3.right, .5f, whatIsTile);
        if (hit2 != null) { colliderList.Add(hit2); hit2.GetComponent<Tile>().canPlace = false; }
        Collider2D hit3 = Physics2D.OverlapCircle(transform.position + Vector3.up, .5f, whatIsTile);
        if (hit3 != null) { colliderList.Add(hit3);  hit3.GetComponent<Tile>().canPlace = false; }
        Collider2D hit4 = Physics2D.OverlapCircle(transform.position + Vector3.down, .5f, whatIsTile);
        if (hit4 != null) { colliderList.Add(hit4);  hit4.GetComponent<Tile>().canPlace = false; }
    }
    void AddToList(params Collider2D[] list)
    {
        colliderList = new List<Collider2D>();
        for (int i = 0; i < list.Length; i++)
        {
            colliderList.Add(list[i]);
        }
    }
    public void SpawnObstacle()
    {
        if (canPlace)
        {
            hasObstacle = true;
            sr.color = Color.black;
            canPlace = false;
            canRun = false;
        }
    }
    public void SpawnPowerUp()
    {
        {
            int i = Random.Range(0, 3);
            if (i < 1)
            {
                nullify = true;
                canPlace = false;
                sr.color = Color.green;
            }
            else if (i > 0)
            {
                attackAhead = true;
                canPlace = false;
                sr.color = Color.yellow;
            }
        }
    }
    
    void ResetPowerUp()
    {
        sr.color = Color.white;
        nullify = false;
        attackAhead = false;
        canPlace = true;
    }
    public void InstaLavaTile()
    {
        canPlace = false;
        sr.color = Color.grey;
        lavaTransitionDelay = 0;
        previousColor = sr.color;
    }
    public void StartLava()
    {
        if (canRun)
        {
            StartCoroutine(TurnToLava());
        }
    }
    public IEnumerator TurnToLava()
    {
        canRun = false;
        lavaAnimationPlaying = true;
        Invoke("StopLavaTransition", lavaTransitionDelay);
        while (lavaAnimationPlaying)
        {
            sr.color = Color.white;
            yield return new WaitForSeconds(.25f);
            sr.color = Color.red;
            yield return new WaitForSeconds(.25f);
        }
        SpawnLava();
        if (GameManager.gm.singlePlayer) { yield break; }
        Invoke("StopLava", lavaDuration);
    }
    public void SpawnLava()
    {
        sr.color = Color.red;
        isLava = true;
 
    }
    void StopLavaTransition()
    {
        lavaAnimationPlaying = false;
    }
    public void StopLava()
    {
        StopAllCoroutines();
        StopLavaTransition();
        Invoke("CanStart", 3);
        sr.color = previousColor;
        isLava = false;
    }
   
}
