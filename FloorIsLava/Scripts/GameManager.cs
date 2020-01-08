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

    private void Awake()
    {
        tm = GetComponent<TileManager>();
        gm = this;
        singlePlayer = false;    
    }
    //change camera orthographic size to enlarge/shrink the size of tiles.  Initial size is 5
    //Add minimum of obstacle/instatile/powerup spawns based off of total tile count
    public void SinglePlayerMode() 
    {
        singlePlayer = true;
        SendInfo();
    }
    public void SendInfo()
    {
        if (tileCount > maxTiles) tileCount = maxTiles;;
        tm.Setup(tileCount, obstacleCount, powerupCount);
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
