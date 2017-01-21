using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatsCounter : MonoBehaviour
{

    public float bpm = 140.0F;
    public double startDelay = 2.0f;
    public float musicGap = 0.1f;

    private static Action waveAction;

    public GameObject[] points;
    public List<GameObject> pointsHolder;
    public GameObject[] pointObjects;

    private static bool inTakt = false;
    private double nextEventTime;
    private double fourthBeatEventTime;
    private AudioSource audioSource;
    private bool running = false;
    private int beats = 0;
    private int taktPoint = 0;
    private bool thirdBeat = false;
    private int section = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        nextEventTime = AudioSettings.dspTime + startDelay;
        audioSource.PlayScheduled(nextEventTime);

        running = true;
    }
    void Update()
    {
        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time > (nextEventTime + musicGap))
        {
            if (beats > points.Length - 1)
            {
                beats = 0;
                for (int i = 0; i < pointsHolder.Count; i++)
                {
                    Destroy(pointsHolder[i]);
                }
                pointsHolder.Clear();
            }

            if (((beats + 1) % 4) == 0)          //такт
            {
                taktPoint = 1;
                if(section == 3)
                {
                    waveAction.Invoke();
                    section = 0;
                }
                else
                {
                    section++;
                }
            }
            else
            {
                taktPoint = 0;
                thirdBeat = false;
            }

            //audioSource.pitch = taktPoint == 1 ? 3 : 2;
            //Debug.Log("Scheduled source " + 0 + " to start at time " + nextEventTime);
            nextEventTime += 60.0F / bpm;

            if (((beats + 2) % 4) == 0)             //третий удар
            {
                fourthBeatEventTime = (nextEventTime + musicGap);
                thirdBeat = true;
            }

            GameObject currentPoitBeat = Instantiate(pointObjects[taktPoint], points[beats].transform.position, points[beats].transform.rotation, this.transform);
            pointsHolder.Add(currentPoitBeat);

            beats++;
        }

        if (thirdBeat && time > (fourthBeatEventTime - (60f / bpm) * 0.4f) && time < (fourthBeatEventTime + (60f / bpm) * 0.6f))
        {
            inTakt = true;
        }
        else
        {
            inTakt = false;
        }
    }

    public static bool InTakt
    {
        get { return inTakt; }
    }

    public static Action WaveAction
    {
        get { return waveAction; }
        set { waveAction = value; }
    }
}
