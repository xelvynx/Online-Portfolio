using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class TileManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tileList = new List<GameObject>();
    private List<Vector3> startingPositions = new List<Vector3>();
    [SerializeField]
    private List<GameObject> tempList = new List<GameObject>();
    public int numberOfInstaTiles = 3;
    public int numberOfObstacles = 3;
    public int numberOfPowerUps = 3;
    public GameObject player1;
    public GameObject player2;
    private Vector2 player1StartPos;
    private Vector2 player2StartPos;
    private float one;
    private float two;
    private GameObject tempGo;
    private Tile goTile;
    public GameObject tilePrefab;
    public GameObject tilePrefabHolder;
    int numberOfTiles;
    public LayerMask whatIsTile;
    public static TileManager tm;
    public int moveCount;
    public int lavaTiles;


     //To remove excess lines type ^(\s)*$\n in find
    private void Start()
    {
        if (tm == null) { tm = this; }
     }
    private void FixedUpdate()
    {
        if (UIManager.um.inGame)
        {
            //if (moveCount <=1)
            //{
                CheckTileList();
            //
        }
    }
    public void CheckTileList() // Win Condition for singleplayer
    {
        foreach (GameObject go in tileList)
        {
           
                if (go.GetComponent<Tile>().GetStepped() == true)
                {
                    lavaTiles++;
                }
            
        }

        else { lavaTiles = 0; }
    }
    public void WinCondition() 
    
    {
        if (lavaTiles >= tileList.Count)
        {
            Debug.Log("youWin");
            UIManager.um.SetEndScreen("Win");
            lavaTiles = 0;
            return;
        }
        // if (lavaTiles < tileList.Count)
        //{
        //    Debug.Log("youLose");
        //    UIManager.um.SetEndScreen("Lose");
        //    lavaTiles = 0;
        //    return;
        //}
    }
    public void Setup(int tile, int obstacle, int powerup, int moveC)
    {
        numberOfTiles = tile;
        numberOfObstacles = obstacle;
        numberOfPowerUps = powerup;
        moveCount = moveC;
        StartCoroutine("InstantiateTiles");
    }
    public void UpdateMoveCount() 
    {
        moveCount--;
        UIManager.um.UpdateMoveCount(moveCount);
    }
    public IEnumerator InstantiateTiles()
    {
        player1.transform.position = new Vector3(20, 20, 20);
        player2.transform.position = new Vector3(20, 20, 20);
        foreach (Transform child in tilePrefabHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
         yield return new WaitForEndOfFrame();
        player1.GetComponent<Player>().InitialMoveCount(moveCount);
        tileList = new List<GameObject>();
        tempList = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject go = Instantiate(tilePrefab, tilePrefabHolder.transform);
            go.name = "Tile" + i.ToString();
            tileList.Add(go);
        }
        //tileList.AddRange(GameObject.FindGameObjectsWithTag("Tile").OrderBy(Tile => Tile.name));
        tileList.OrderBy(Tile => Tile.name);
        StartThis();
    }
    void StartThis()
    {
        if (numberOfTiles > 100)
        {
            GridLayoutGroup glg = tilePrefabHolder.GetComponent<GridLayoutGroup>();
            glg.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            glg.constraintCount = 10;
        }
        StartCoroutine("Testing");
    }
     IEnumerator Testing() // Step1 Checks position of tiles and decides possible starting positions.  Chooses corners.
    {
        yield return new WaitForEndOfFrame();
        one = tileList[0].transform.position.y;
        for (int i = 0; i < tileList.Count; i++)
        {
            yield return new WaitForEndOfFrame();
            two = tileList[i].transform.position.y;
            if (one != two)
            {
                int sp2 = i - 1; //grabs index of the tile before the y position changes, also calculates the length of a row
                int sp3 = tileList.Count - 1; //checks the length of the List and you subtract 1 to get the last object within the list
                int sp4 = sp3 - sp2; // 
                                     //AddToStartingTiles(startingPos2, startingPos3, startingPos4);
                yield return new WaitForEndOfFrame();
                Vector3 a = tileList[0].transform.position;
                Vector3 b = tileList[sp2].transform.position;
                Vector3 c = tileList[sp3].transform.position;
                Vector3 d = tileList[sp4].transform.position;
                AddToList(a, b, c, d);
                tileList[0].GetComponent<Tile>().StartCoroutine("StartingPositions");
                tileList[sp2].GetComponent<Tile>().StartCoroutine("StartingPositions");
                tileList[sp3].GetComponent<Tile>().StartCoroutine("StartingPositions");
                tileList[sp4].GetComponent<Tile>().StartCoroutine("StartingPositions");
                RemoveFromTileList(0, sp2, sp3, sp4);
                //StartThisNow();
                break;
            }
        }
    }
     void AddToList(Vector3 h, Vector3 i, Vector3 j, Vector3 k)
    {
        startingPositions.Add(h);
        startingPositions.Add(i);
        startingPositions.Add(j);
        startingPositions.Add(k);
    }
    void RemoveFromTileList(int h, int i, int j, int k) // Step2
    {
        tempList.AddRange(tileList);
        tempList.Remove(tileList[h]);
        tempList.Remove(tileList[i]);
        tempList.Remove(tileList[j]);
        tempList.Remove(tileList[k]);
         for (int x = 0; x < tempList.Count; x++)
         {
            tempList[x].name = "Tile" + x;
        }
        StartThisNow();
        ////Debug.Log("TempList length is " + tempList.Count); //currently max count is 52  create better formula for counting obstacles and powerups and instatiles
     }
    public void StartThisNow()
    {
        StartCoroutine("SetStartingPositions");
    }
    IEnumerator SetStartingPositions() // Step2
    {
        int pos1 = RandomNum(startingPositions.Count);
        yield return new WaitForEndOfFrame();
        player1StartPos = startingPositions[pos1];
        startingPositions.Remove(startingPositions[pos1]);
        player1.transform.position = player1StartPos;
        player1.GetComponent<Player>().SetRespawnPoint(player1StartPos);
        int pos2 = RandomNum(startingPositions.Count);
        yield return new WaitForEndOfFrame();
        player2StartPos = startingPositions[pos2];
        player2.GetComponent<Player>().SetRespawnPoint(player2StartPos);
        player2.transform.position = player2StartPos;
        StartCoroutine("SpawnObstacles"); // by doing yield return, it will wait for the coroutine to finish before it starts
     }
    IEnumerator SpawnObstacles() // Step3
    {
        for(int i = 0; i<numberOfObstacles; i++) 
        {
            for(int x = 0; x < tileList.Count; x++) 
            {
                tempGo = tempList[RandomNum(tempList.Count)];
                goTile = tempGo.GetComponent<Tile>();
                if (goTile.GetCanPlace()) 
                {
                    goTile.SpawnObstacle();
                    tempList.Remove(tempGo);
                    tileList.Remove(tempGo);
                    break;
                }
            }
        }
        
        yield return new WaitForEndOfFrame();
         RandomInstaLavaTile();  // make it so you can spawn both obstacles and powerups on same platform
        Debug.Log("IGOTHERE");
    }
    void RandomInstaLavaTile() // Step4 Setting up instant lava tiles
    {
        for (int i = 0; i < numberOfInstaTiles; i++)
        {
            int x = Random.Range(0, tempList.Count);
            tempList[x].GetComponent<Tile>().InstaLavaTile();
            tempList.RemoveAt(x);
        }
        if (GameManager.gm.singlePlayer) { return; }
        SpawnPowerUp();
         InvokeRepeating("SpawnPowerUp", 15, 15);
    }
    void SpawnPowerUp() // Step 5 SpawnPowerups 
    {
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            GameObject o = tempList[RandomNum(tempList.Count)];
            o.GetComponent<Tile>().SpawnPowerUp();
        }
        StartCoroutine("RandomLavaTile");
    }
    int RandomNum(int x)
    {
        return Random.Range(0, x);
    }
    public IEnumerator RandomLavaTile()
    {
        yield return new WaitForEndOfFrame();
        Vector3 tile = tileList[RandomNum(tileList.Count)].transform.position;
        int i = RandomNum(2);
        GroupedLava(tile, i);
    }
    public void GroupedLava(Vector3 v, int i)
    {
        if (i == 1)
        {
            Collider2D[] hit = Physics2D.OverlapAreaAll(new Vector3(v.x - 10, v.y - .2f, v.z), new Vector3(v.x + 10, v.y + .2f, v.z), 256);//layermask 256 is Tile layer;
                                                                                                                                           //c = hit;
            foreach (Collider2D cd in hit)
            {
                Tile t = cd.GetComponent<Tile>();
                if (!t.GetObstacle() && !t.GetNullify() && !t.attackAhead)
                {
                    t.StartLava();
                 }
            }
        }
        if (i == 0)
        {
            Collider2D[] hit = Physics2D.OverlapAreaAll(new Vector3(v.x - .2f, v.y - 20, v.z), new Vector3(v.x + .2f, v.y + 20, v.z), 256);//layermask 256 is Tile layer;
                                                                                                                                           //c = hit;
            foreach (Collider2D cd in hit)
            {
                Tile t = cd.GetComponent<Tile>();
                if (!t.GetObstacle() && !t.nullify && !t.attackAhead)
                {
                    t.StartLava();
                }
            }
        }
    }
}
