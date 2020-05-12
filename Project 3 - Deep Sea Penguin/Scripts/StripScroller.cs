using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripScroller : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
