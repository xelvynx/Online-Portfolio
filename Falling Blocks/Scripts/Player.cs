using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ControlSet
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;
    public KeyCode ability4;
}
[System.Serializable]
public class PlayerStats
{
    public int nullify = 2;
    public int attackAhead = 2;
    public int jump = 1000;
}
public class Player : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving = false;
    [SerializeField] PlayerStats ps;
    public Vector3 endPosition;
    public Vector3 testEndPosition;
    public LayerMask whatIsTile;
    public LayerMask whatIsPlayer;
    private bool canMove = false;
    private Vector3 respawnPoint;
    public int moveCount = 10;
    Vector3 direction;
    [SerializeField]
    public ControlSet cs;
    Tile t;
    public static Player play;
   
    public bool skill = false;
    public bool jump = false;
    public bool wobbly = false;

    InputManager im;
    public void InitialMoveCount(int i) { moveCount = i; }


    //Possibly add limited movement.  6 movements and it refreshes every 3 seconds
    void Start()
    {

        if (play == null)
        {
            play = this;
        }
        direction = Vector2.zero;
        im = InputManager.im;
    }
    void Update() 
    {
        ControlAction();
        if (wobbly)
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);

        }

    }

    public void SetControls() 
    {
        if(gameObject.name == "Player1") 
        {
            cs.up = im.GetP1KeyCode("Up");
            cs.down = im.GetP1KeyCode("Down");
            cs.left = im.GetP1KeyCode("Left");
            cs.right = im.GetP1KeyCode("Right");
            cs.ability1 = im.GetP1KeyCode("Ability1");
            cs.ability2 = im.GetP1KeyCode("Ability2");
            cs.ability3 = im.GetP1KeyCode("Ability3");
            cs.ability4 = im.GetP1KeyCode("Ability4");
        }
        if (gameObject.name == "Player2")
        {
            cs.up = im.GetP2KeyCode("Up");
            cs.down = im.GetP2KeyCode("Down");
            cs.left = im.GetP2KeyCode("Left");
            cs.right = im.GetP2KeyCode("Right");
            cs.ability1 = im.GetP2KeyCode("Ability1");
            cs.ability2 = im.GetP2KeyCode("Ability2");
            cs.ability3 = im.GetP2KeyCode("Ability3");
            cs.ability4 = im.GetP2KeyCode("Ability4");
        }
    }
    public void SetRespawnPoint(Vector3 v)
    {
        respawnPoint = v;
        direction = Vector2.zero;
        endPosition = v;
        transform.position = v;
        SetControls();
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                isMoving = false;
            }
        }
        if (wobbly && isMoving) 
        {
            Debug.Log("Ahhhh");//detect if there is lava around, if there is then respawn
            Respawn();
        }
    }

    public void AddNullify()
    {
        ps.nullify++;
        UIManager.um.UpdateNullify(ps.nullify);
    }
    public void AddAttackAhead()
    {
        ps.attackAhead++;
        UIManager.um.UpdateAttackAhead(ps.attackAhead);
    }
    public void ReduceNullify()
    {
        ps.nullify--;
        UIManager.um.UpdateNullify(ps.nullify);
    }
    public void ReduceAttackAhead()
    {
        ps.attackAhead--;
        UIManager.um.UpdateAttackAhead(ps.attackAhead);
    }
    public void Respawn()
    {
        canMove = false;
        transform.position = respawnPoint;
        endPosition = transform.position;
        Invoke("CanMoveHim", 3);
    }
    public void CanMoveHim() { canMove = true; }
    void ForwardSquare()
    {
        //checks 2 squares ahead in a certain direction
        //RaycastHit2D hit = Physics2D.Raycast(FinalDirection(transform.position, direction), -direction, 2, whatIsTile);
        Collider2D hits = Physics2D.OverlapCircle(transform.position+(direction*2), .1f, whatIsTile);
        if (!hits)
        {
            endPosition = transform.position;
            jump = false;
            skill = false;
            return;
        }
        if (jump)
        {
            if (hits.gameObject.GetComponent<Tile>().GetObstacle() == false && hits.gameObject.GetComponent<Tile>().GetPlayer() == false)
            {
                TrackingMoveCount(-1);
                Debug.Log(hits.gameObject.name);
                endPosition = hits.transform.position;
                isMoving = true;
                jump = false;
            }
        }
        if (skill && ps.attackAhead > 0)
        {
            if (hits.GetComponent<Tile>().canRun)
            {
                hits.GetComponent<Tile>().StartLava();
                ReduceAttackAhead();
                skill = false;
            }
        }
        return;
    }
    void Move() 
    {
        endPosition = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y);
        //Vector3Int.RoundToInt(endPosition);
        Collider2D hit = Physics2D.OverlapCircle(endPosition, .2f, whatIsTile);
        if (!hit)
        {
            Vector3Int.RoundToInt(endPosition);
            endPosition = transform.position;
            return;
        }
        if (hit.tag == "Tile")
        {
            endPosition = hit.transform.position;
            Tile t = hit.GetComponent<Tile>();
            if (t.GetObstacle() || t.GetPlayer()) return;
            if (t.GetObstacle() == false && t.GetPlayer() == false)
            {
                if (GameManager.gm.singlePlayer)
                {
                    if (moveCount > 0)
                    {
                        TrackingMoveCount(-1);
                        isMoving = true;
                    }
                    return;
                }
                else
                {

                    isMoving = true;
                    return;
                }
            }
        }
    }
    void TrackingMoveCount(int i)
    {
        moveCount--;
        TileManager.tm.moveCount--;
        if(moveCount == 0) 
        {
            TileManager.tm.CheckTileList();
        }
    }
    public void SetWobble() 
    {
        wobbly = true;

        Invoke("DeWobble", 3) ;
        
    }

    
    public void DeWobble() 
    {
        wobbly = false;
        transform.rotation = Quaternion.identity; 
    }
    void KnockOffAbility()
    {
        //Ability where you shoot a blast of air in a direction and people get knocked off if they move within a certain timeframe
        Vector3 directions = transform.TransformDirection(direction) * 10;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,100, whatIsPlayer);
        if (!hit) { return; }
        if (hit.collider.tag == "Player") 
        {
            Debug.Log("Gotya");
            hit.collider.GetComponent<Player>().SetWobble();
            Debug.Log(hit.collider.name);
        }
        
    }
    public void ControlAction()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(cs.ability1) && !isMoving)
            {
                skill = true;
                KnockOffAbility();
                //ForwardSquare();
            }
            if (Input.GetKeyDown(cs.ability2) && !isMoving)
            {
                if (ps.nullify > 0)
                {
                    //Turns square you're standing on safe/into land instead of lava.
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 2, whatIsTile);
                    if (!hit) return;
                    if (hit.collider.gameObject.GetComponent<Tile>().lavaAnimationPlaying)
                    {
                        hit.collider.gameObject.GetComponent<Tile>().StopLava();
                        ReduceNullify();
                    }

                }
            }
            if (Input.GetKeyDown(cs.ability3) && !isMoving)
            {
                jump = true;
                ForwardSquare();
            }
            if (im.GetButtonDown("Left") && !isMoving) //Left
            {
                direction = Vector3.left;
                Move();
            }
            if (Input.GetKey(cs.right) && !isMoving) //Right
            {
                direction = Vector3.right;
                Move();
            }
            if (Input.GetKey(cs.up) && !isMoving) //Up
            {
                direction = Vector3.up;
                Move();
            }
            if (Input.GetKey(cs.down) && !isMoving)//Down
            {
                direction = Vector3.down;
                Move();
            }
        }
    }
}
