using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    [SerializeField]
    private float startDelay;

    private Animator animator;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        StartCoroutine(StartHeatyAnimation());
    }
	
	private IEnumerator StartHeatyAnimation()
    {
        yield return new WaitForSeconds(startDelay);
        animator.SetTrigger("Start");
    }
}
