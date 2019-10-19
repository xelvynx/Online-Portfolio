using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    void Update() { transform.Translate(Vector2.down * Time.deltaTime * (7+GameManager.gm.addSpeed)); }

}
