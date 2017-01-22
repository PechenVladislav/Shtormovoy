using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpaceRelaod : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.LoadLevel(1);
        }
	}
}
