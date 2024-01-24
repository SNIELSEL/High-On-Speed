using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int currentLap;
    [SerializeField] private int maximalLap;

    [SerializeField] private bool raceFinished;

    private void OnTriggerEnter(Collider finishLine)
    {
        if (currentLap < maximalLap)
        {
            currentLap ++;
        }

        if (currentLap == maximalLap)
        {
            raceFinished = true;
        }
    }

}
