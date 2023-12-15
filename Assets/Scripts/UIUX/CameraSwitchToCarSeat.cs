using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchToCarSeat : MonoBehaviour
{
    private Vector3 startLoc;
    private Vector3 menuPanelStartPos;
    public GameObject carSeatLoc;
    public GameObject menuPanel;
    public GameObject carMenuPanelLoc;
    private bool inCar;

    public void Start()
    {
        startLoc = transform.position;
        menuPanelStartPos = menuPanel.transform.position;
    }

    public void SwitchSeat()
    {
        if (!inCar)
        {
            menuPanel.transform.position = carMenuPanelLoc.transform.position;
            transform.position = carSeatLoc.transform.position;
            inCar = true;
        }
        else
        {
            menuPanel.transform.position = menuPanelStartPos;
            transform.position = startLoc;
            inCar = false;
        }
    }
}
