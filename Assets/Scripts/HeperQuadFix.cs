using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeperQuadFix : MonoBehaviour {

    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.sortingOrder = 9;

    }
}
