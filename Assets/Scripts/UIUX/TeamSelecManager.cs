using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamSelecManager : MonoBehaviour
{
    public string[] teamNames;
    public TextMeshProUGUI nameText;
    private int currentTeamSelected;

    public void SelectTeam(int teamNumber)
    {
        currentTeamSelected = teamNumber;
        PlayerPrefs.SetInt("Team",teamNumber);

        nameText.text = teamNames[teamNumber];
    }
}
