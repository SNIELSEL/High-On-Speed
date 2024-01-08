using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointContainer : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    
    // makes it so that all the waypoints go into a list
    void Awake()
    {
        foreach(Transform transform in GetComponentsInChildren<Transform>())
        {
            waypoints.Add(transform);
        }
        waypoints.Remove(waypoints[0]);
    }


    void Update()
    {
        
    }
}
