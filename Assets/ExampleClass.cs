using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ExampleClass : MonoBehaviour
{
    public float bpm = 140.0F;
    public int numBeatsPerSegment = 16;
    public double startDelay = 2.0f;
    private double nextEventTime;
    private AudioSource audioSource;
    private bool running = false;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        nextEventTime = AudioSettings.dspTime + startDelay;
        running = true;
    }
    void Update()
    {
        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time > nextEventTime)
        {
            audioSource.PlayScheduled(nextEventTime);
            //audioSource.Play();
            Debug.Log("Scheduled source " + 0 + " to start at time " + nextEventTime);
            nextEventTime += 60.0F / bpm * numBeatsPerSegment;
        }
    }
}