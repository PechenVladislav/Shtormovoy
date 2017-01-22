using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperParticles : MonoBehaviour {

    [SerializeField]
    private ParticleSystem part1;

    [SerializeField]
    private ParticleSystem part2;

    [SerializeField]
    private float startDelay = 0f;

    // Use this for initialization
    void Start () {
        StartCoroutine(ParticlDelay());
	}
	
	private IEnumerator ParticlDelay()
    {
        yield return new WaitForSeconds(startDelay);
        part1.Play();
        part2.Play();
    }
}
