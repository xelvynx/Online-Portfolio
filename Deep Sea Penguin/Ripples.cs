using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripples : MonoBehaviour {

    Renderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        Color newColor = renderer.material.color;
        newColor.a = .5f;
        renderer.material.color = newColor;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
