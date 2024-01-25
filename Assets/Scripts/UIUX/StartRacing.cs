using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRacing : MonoBehaviour
{
    public GameObject[] cars;

    public void Begin()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<RivalAICarController>().raceStart = true;
        }
    }
}
