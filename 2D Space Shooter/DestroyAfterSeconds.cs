using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

	void Update () {
        Destroy(gameObject, 1f);
	}
}
