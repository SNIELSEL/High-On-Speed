using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointContainer : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints;
    
    // makes it so that all the waypoints go into a list
    void Awake()
    {
        foreach(GameObject gameObject in GetComponentsInChildren<GameObject>())
        {
            waypoints.Add(gameObject);
        }
        waypoints.Remove(waypoints[0]);
    }


    void Update()
    {
        
    }
}
