using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> players;
    public float moveSpeed;
    public Transform activeChar;
    public Vector3 targetPos;
    public Vector3 startingPos;
    public bool isAttacking;
    public bool goBack = false;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        players = new List<GameObject>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            enemies.Add(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) 
        {
            players.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)&&!isAttacking) 
        {
            activeChar = players[0].transform;
            targetPos = enemies[0].transform.position;
            isAttacking = true;
            startingPos = activeChar.position;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isAttacking)
        {
            activeChar = players[1].transform;
            targetPos = enemies[0].transform.position;
            isAttacking = true;
            startingPos = activeChar.position;

        }
        if (isAttacking && activeChar.position != targetPos) //Moves player towards enemy
        {
            //setup a cooldown before able to attack
            activeChar.position = Vector3.MoveTowards(activeChar.position, targetPos, moveSpeed);

            if(activeChar.position == targetPos) 
            {

                Debug.Log("Hit");
                Invoke("GoBack", 1);
                
            }
        }
        if (goBack)  // Moves player back to starting pos
        {
            activeChar.position = Vector3.MoveTowards(activeChar.position, startingPos, moveSpeed);
            isAttacking = false;
            if(activeChar.position == startingPos) 
            {
                goBack = false;
            }
        }
    }
    public void GoBack() 
    {
        goBack = true;
    }
}
