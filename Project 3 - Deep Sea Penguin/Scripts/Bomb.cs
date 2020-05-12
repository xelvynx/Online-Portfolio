using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {


    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * (GameManager.gm.bombSpeed + GameManager.gm.addSpeed));
    }

}
