using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        BeatsCounter.WaveAction += WaveUp;
    }

    private void OnDisable()
    {
        BeatsCounter.WaveAction -= WaveUp;
    }

    private void WaveUp()
    {
        transform.position += Vector3.up;
        print("Wave Up!");
    }
}
