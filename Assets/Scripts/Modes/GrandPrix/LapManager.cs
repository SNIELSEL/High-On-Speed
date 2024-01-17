using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int currentLap;
    [SerializeField] private int maxLap;

    [SerializeField] private bool raceFinished;

    private void OnTriggerEnter(Collider finishLine)
    {
        if (currentLap < maxLap)
        {
            currentLap ++;
        }

        if (currentLap == maxLap)
        {
            raceFinished = true;
        }
    }

}
