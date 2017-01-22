using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesPlatform : MonoBehaviour {

    private float counter = 0f;
    private float delay = 1f;
    private bool isDanger = true;
    private bool kill = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        counter += Time.deltaTime;
        if(counter >= delay)
        {
            counter = counter - delay;
            isDanger = !isDanger;
        }

        Vector2 playerPos = new Vector2(PlayerBehaviuor.Instance.gameObject.transform.position.x, PlayerBehaviuor.Instance.gameObject.transform.position.y);
        Vector2 platformPos = new Vector2(transform.position.x, transform.position.y);

        if(playerPos == platformPos && isDanger && !kill)
        {
            StartCoroutine(PlayerBehaviuor.Instance.PlayerDeath());
            kill = true;
        }
    }
}
