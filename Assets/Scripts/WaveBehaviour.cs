using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{

    [SerializeField]
    private int waveSpeed = 1;

    // Use this for initialization
    void Start()
    {

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
        transform.position += Vector3.up * waveSpeed;
        print("Wave Up!");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(PlayerBehaviuor.Instance.PlayerDeath());
        }
    }
}
