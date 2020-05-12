using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUp : MonoBehaviour {


    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime*(8+GameManager.gm.addSpeed));
    }
}
