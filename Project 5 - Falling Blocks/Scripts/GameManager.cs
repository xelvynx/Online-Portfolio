using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public bool singlePlayer;
    public int tileCount = 56;
    public int maxTiles = 180;
    public int obstacleCount;
    public int powerupCount;
    public int instantTileCount;
    public int nullify = 2;
    public int attackAhead = 2;
    public int jump = 1000;
    public int moveCount;

    public int jumpPerLevel;
    public int attackPerLevel;
    public int stagesCompleted = 0;

    int initialJump;
    int initialAttack;
    private void Awake()
    {
        gm = this;
        singlePlayer = false;
    }
    //change camera orthographic size to enlarge/shrink the size of tiles.  Initial size is 5
    //Add minimum of obstacle/instatile/powerup spawns based off of total tile count
    public void SinglePlayerMode()
    {
        singlePlayer = true;
        SendInfo();
        UIManager.um.UpdateJump(jump);
        UIManager.um.UpdateAttackAhead(attackAhead);
    }
    public void SetAbilities() 
    {
        jump += jumpPerLevel;
        //nullify = nullify;
        attackAhead = attackPerLevel;
        stagesCompleted++;
    }
    public void SendInfo()
    {
        if (tileCount > maxTiles) tileCount = maxTiles; ;
        if (obstacleCount + powerupCount+ instantTileCount>= tileCount- 4)
        {
            int i = tileCount- 4;
            int j = i / 2;
            obstacleCount = j;
            //numberOfPowerUps = Mathf.RoundToInt(tempList.Count / 8);
            //numberOfObstacles = Mathf.RoundToInt(i/2);
            //j = numberOfObstacles;
            //numberOfPowerUps = i - j;
            //Debug.Log("Number of powerups = " + numberOfPowerUps);
        }
        moveCount = tileCount- Mathf.Abs(obstacleCount- powerupCount) + 2;
        UIManager.um.UpdateMoveCount(moveCount);
        TileManager.tm.Setup(tileCount, obstacleCount, powerupCount,moveCount);
    }
    public void SetTileCount(int i)
    {
        tileCount = i;
    }
    public void SetObstacleCount(int i)
    {
        obstacleCount = i;
    }
    public void SetPowerupCount(int i)
    {
        powerupCount = i;
    }
    public void SetInstantTileCount(int i)
    {
        instantTileCount = i;
    }
}
