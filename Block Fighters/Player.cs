using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    SpriteRenderer m_spriteRenderer;
    public BoxCollider2D boxCollider;
    public float blueN = 0;
    public float yellowN = 0;
    public float redN = 0;
    public GameObject reticle;
    public bool bluePress = false;
    public bool redPress = false;
    public bool yellowPress = false;
    private static Player _instance;
    static public Player play
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Player>();
            }

            return _instance;
        }
    }
    public void Start()
    {
        //m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!bluePress && !yellowPress && !redPress)
        {
            reticle.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (bluePress)
        {
            reticle.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (redPress)
        {
            reticle.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (yellowPress)
        {
            reticle.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //m_spriteRenderer.color = Color.Lerp(m_spriteRenderer.color, Color.blue,.1f);
            BlueColor(true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            BlueColor(false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //m_spriteRenderer.color = Color.Lerp(m_spriteRenderer.color, Color.blue,.1f);
            YellowColor(true);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            YellowColor(false);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //m_spriteRenderer.color = Color.Lerp(m_spriteRenderer.color, Color.blue,.1f);
            RedColor(true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            RedColor(false);
        }
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{

    //    if ((bluePress == true && other.gameObject.tag == "Enemy1")
    //        || (Input.GetKeyDown(KeyCode.X) && gameObject.tag == "Enemy2") || (Input.GetKeyUp(KeyCode.X) && gameObject.tag == "Enemy2"))


    //    {
    //        GameManager.gm.Score(1);
    //        Destroy(gameObject);
    //    }

    //}

    public void BlueColor(bool b)
    {
        bluePress = b;
        Debug.Log("Color Blue is Pressed");
    }
    public void YellowColor(bool b)
    {
        yellowPress = b;
    }
    public void RedColor(bool b)
    {
        redPress = b;
    }
}
