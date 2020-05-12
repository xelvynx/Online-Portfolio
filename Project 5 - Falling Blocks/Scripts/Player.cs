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
public class Player : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving = false;

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
    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;
    bool facingUp = false;
    bool facingDown = false;
    bool facingLeft = false;
    bool facingRight = false;
    public bool running = false;
    InputManager im;
    bool skillCooldown = false;
    float s1CD = 3;
    float s2CD = 2;
    float s3CD = 3;
    float s4CD = 4;
    float nextSkill;
    Collider2D cl;
    //Possibly add shared cooldowns with different cooldown rates;  skill1 gives cd of 3, skill2 gives cd of 4
    public void InitialMoveCount(int i) { moveCount = i; }
    public bool GetSkillCooldown()
    {
        return skillCooldown;
    }
    public void SetSkillCooldown(bool b) { skillCooldown = b; }
    //Possibly add limited movement.  6 movements and it refreshes every 3 seconds
    void Start()
    {
        cl = GetComponent<BoxCollider2D>();
        if (play == null)
        {
            play = this;
        }
        direction = Vector2.zero;
        im = InputManager.im;
    }
    void Firing()
    {
    }
    void FixedUpdate()
    {
        ControlAction();
        if (wobbly)
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                isMoving = false;
                cl.enabled = true;
            }
        }
        if (wobbly && isMoving)
        {
            Debug.Log("Ahhhh");//detect if there is lava around, if there is then respawn
            Respawn();
        }
    }

    public void SetControls()
    {
        if (gameObject.name == "Player1")
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
    public void AddJump()
    {
        GameManager.gm.nullify++;
        UIManager.um.UpdateJump(GameManager.gm.jump);
    }
    public void AddAttackAhead()
    {
        GameManager.gm.attackAhead++;
        UIManager.um.UpdateAttackAhead(GameManager.gm.attackAhead);
    }
    public void ReduceJump()
    {
        GameManager.gm.jump--;
        UIManager.um.UpdateJump(GameManager.gm.jump);
    }
    public void ReduceAttackAhead()
    {
        GameManager.gm.attackAhead--;
        UIManager.um.UpdateAttackAhead(GameManager.gm.attackAhead);
    }
    public void Respawn()
    {
        canMove = false;
        transform.position = respawnPoint;
        endPosition = transform.position;
        if (GameManager.gm.singlePlayer)
        {

            TileManager.tm.CheckTileList();

        }
        Invoke("CanMoveHim", 3);
    }
    public void CanMoveHim() { canMove = true; }
    void ForwardSquare()
    {
        //checks 2 squares ahead in a certain direction
        //RaycastHit2D hit = Physics2D.Raycast(FinalDirection(transform.position, direction), -direction, 2, whatIsTile);
        Collider2D hits = Physics2D.OverlapCircle(transform.position + (direction * 2), .1f, whatIsTile);
        if (!hits)
        {
            endPosition = transform.position;
            jump = false;
            skill = false;
            return;
        }
        Tile t = hits.GetComponent<Tile>();
        if (jump)
        {
            if (GameManager.gm.jump > 0)
            {
                if (t.GetObstacle() == false && t.GetPlayer() == false)
                {

                    cl.enabled = false;
                    TrackingMoveCount(-1);
                    endPosition = hits.transform.position;
                    isMoving = true;
                    jump = false;
                    ReduceJump();
                    if (skillCooldown && Time.time >= nextSkill)
                    {
                        nextSkill = Time.time + s3CD;
                        return;
                    }
                }
                
            }
            else { jump = false; }

        }
        if (skill && GameManager.gm.attackAhead > 0)
        {
            if (t.canRun)
            {
                t.SetStepped();
                t.StartLava();
                ReduceAttackAhead();
                skill = false;
            }
        }
        return;
    }
    void Move()
    {
        endPosition = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y);
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
        TileManager.tm.UpdateMoveCount();
        if (moveCount == 0)
        {
            TileManager.tm.CheckTileList();
        }
    }
    public void SetWobble()
    {
        wobbly = true;
        Invoke("DeWobble", 3);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100, whatIsPlayer);
        if (!hit) { return; }
        if (hit.collider.tag == "Player")
        {
            hit.collider.GetComponent<Player>().SetWobble();
        }
    }
    void Nullify()
    {
        if (GameManager.gm.nullify > 0)
        {

            //Turns square you're standing on safe/into land instead of lava.
            Collider2D hit = Physics2D.OverlapCircle(transform.position, .2f, whatIsTile);
            if (!hit) return;
            if (hit.gameObject.GetComponent<Tile>().lavaAnimationPlaying)
            {
                Debug.Log("Yayyyy");
                //if (skillCooldown && Time.time >= nextSkill)
                //{
                //    nextSkill = Time.time + s2CD;
                //    hit.collider.GetComponent<Tile>().StopLava();
                //    ReduceNullify();
                //    return;
                //}
                hit.gameObject.GetComponent<Tile>().StopLava();
                //ReduceNullify();
            }
        }

    }
    public void ControlAction()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(cs.ability1) && !isMoving)
            {
                if (skillCooldown && Time.time >= nextSkill)
                {
                    nextSkill = Time.time + s1CD;
                    Debug.Log("Using SKill");
                    skill = true;
                    ForwardSquare();
                    return;
                }
                else if (!skillCooldown)
                {
                    skill = true;
                    ForwardSquare();
                }
            }
            if (Input.GetKeyDown(cs.ability2) && !isMoving)
            {
                if (skillCooldown && Time.time >= nextSkill)
                {
                    nextSkill = Time.time + s3CD;
                    jump = true;
                    ForwardSquare();
                    return;
                }
                jump = true;
                ForwardSquare();
            }
            if (Input.GetKeyDown(cs.ability3) && !isMoving)
            {
                Nullify();
            }
            if (Input.GetKeyDown(cs.ability4) && !isMoving)
            {
                if (skillCooldown && Time.time >= nextSkill)
                {
                    nextSkill = Time.time + s4CD;
                    KnockOffAbility();
                    return;
                }
                KnockOffAbility();
            }
            if (Input.GetKey(cs.left)) //Left
            {
                direction = Vector3.left;
                if (!facingLeft && !running)
                {
                    running = true;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    Invoke("FacingLeft", .05f);
                    return;
                }
                if (facingLeft && !isMoving)
                {
                    Move();
                }
            }
            if (Input.GetKey(cs.right)) //Right
            {
                direction = Vector3.right;
                if (!facingRight && !running)
                {
                    running = true;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    Invoke("FacingRight", .05f);
                    return;
                }
                if (facingRight && !isMoving)
                {
                    Move();
                }
            }
            if (Input.GetKey(cs.up)) //Up
            {
                direction = Vector3.up;
                if (!facingUp && !running)
                {
                    running = true;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    Invoke("FacingUp", .05f);
                    return;
                }
                if (facingUp && !isMoving)
                {
                    Move();
                }
            }
            if (Input.GetKey(cs.down))//Down
            {
                direction = Vector3.down;
                if (!facingDown && !running)
                {
                    running = true;
                    Invoke("FacingDown", .05f);
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    return;
                }
                if (facingDown && !isMoving)
                {
                    Move();
                }
            }
        }
    }
    void FacingLeft() { DisableFace(); running = false; facingLeft = true; }
    void FacingRight() { DisableFace(); running = false; facingRight = true; }
    void FacingUp() { DisableFace(); running = false; facingUp = true; }
    void FacingDown() { DisableFace(); running = false; facingDown = true; }
    void DisableFace() { facingUp = false; facingDown = false; facingLeft = false; facingRight = false; }
    public void GetButton(string s)
    {
        if (s == "Up")
        {
            moveUp = true;
        }
        if (s == "Down")
        {
            moveDown = true;
        }
        if (s == "Left")
        {
            moveLeft = true;
        }
        if (s == "Right")
        {
            moveRight = true;
        }
        if (s == "Stop")
        {
            moveLeft = false;
            moveUp = false;
            moveDown = false;
            moveRight = false;
        }
    }
    public void MobileControls()
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
                if (GameManager.gm.nullify > 0)
                {
                    //Turns square you're standing on safe/into land instead of lava.
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 2, whatIsTile);
                    if (!hit) return;
                    if (hit.collider.gameObject.GetComponent<Tile>().lavaAnimationPlaying)
                    {
                        hit.collider.gameObject.GetComponent<Tile>().StopLava();
                        //ReduceNullify();
                    }
                }
            }
            if (Input.GetKeyDown(cs.ability3) && !isMoving)
            {
                jump = true;
                ForwardSquare();
            }
            if (moveLeft && !isMoving) //Left
            {
                direction = Vector3.left;
                Move();
            }
            if (moveRight && !isMoving) //Right
            {
                direction = Vector3.right;
                Move();
            }
            if (moveUp && !isMoving) //Up
            {
                direction = Vector3.up;
                Move();
            }
            if (moveDown && !isMoving)//Down
            {
                direction = Vector3.down;
                Move();
            }
        }
    }
}
