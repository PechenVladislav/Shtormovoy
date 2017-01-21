using System.Collections;
using System;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{

    [SerializeField]
    private int waveSpeed = 1;
    [SerializeField]
    private int waveStep = 1;

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
        //transform.position += Vector3.up * waveSpeed;
        StartCoroutine(WaveMove());
        print("Wave Up!");
    }

    private IEnumerator WaveMove()
    {
        float destinationY = transform.position.y + waveStep;
        GetComponent<Animator>().Play("WaveFading");

        while (destinationY - transform.position.y > 0.01f)
        {
            float moveY = Mathf.Lerp(transform.position.y, destinationY, waveSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, moveY, transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, destinationY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print(1);
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(PlayerBehaviuor.Instance.PlayerDeath());
        }
        if (col.gameObject.tag == "Floor")
        {
            print(2);
            col.gameObject.GetComponent<Assets.Floor>().breakPlatform();
            col.gameObject.GetComponent<Animator>().Play("PlatformFading");
        }
    }
}
