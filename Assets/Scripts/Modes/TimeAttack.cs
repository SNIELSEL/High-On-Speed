using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class TimeAttack : MonoBehaviour
{
    [Header("lap time")]
    [SerializeField] private float lapTime;
    [SerializeField] private float bestTime;
    [SerializeField] private int currentLapMinutes;
    [SerializeField] private int bestLapMinutes;
    [SerializeField] private int currentLap;
    [SerializeField] private int bestLap;
    [SerializeField] private string[] times;
    [SerializeField] private float[] timesFloat;

    [Header("Text Mesh Pro's")]
    [SerializeField] private TextMeshProUGUI lapTimeUI;
    [SerializeField] private TextMeshProUGUI bestLapTimeUI;
    [SerializeField] private TextMeshProUGUI currentLapUi;
    [SerializeField] private TextMeshProUGUI bestLapUI;

    [Header("Multiplayer")]
    [SerializeField] private PlayFabManager multiplayer;

    [Header("Debugging")]
    [SerializeField] private bool startRace;
    [SerializeField] private bool fillInArray;

    private bool lapStarted;
    private bool lapFinished;
    private bool firstStart;
    private bool firstLap;
    private bool finishedTimeTrial;

    private float lapDecimal;
    private float bestLapDecimal;
    private int convertedText;
    private int convertedMinute;
    private int emptyArrays;

    private string convertedTime;

    public List<float> sortedFloats;

    public RivalAICarController carController;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        LoadBestTime();

        if (startRace)
        {
            firstStart = false;
            lapStarted = true;
        }

        firstLap = true;
        firstStart = true;
        lapFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fillInArray)
        {
            fillInArray = false;
            FinishedTimeTrial();
        }

        ConvertSecondToMinutes();

        // makes the timer go up if you have started a lap
        if (lapStarted == true)
        {
            lapTime += 1 * Time.deltaTime;
            lapDecimal = (lapTime % 1) * 1000;
        }

        if (carController != null && firstStart)
        {
            if (carController.raceStart)
            {
                firstStart = false;
                lapStarted = true;
            }
        }

        if(currentLapMinutes == 0)
        {
            lapTimeUI.text = lapTime.ToString("F0") + "." + lapDecimal.ToString("F0");
        }
        else
        {
            lapTimeUI.text = currentLapMinutes.ToString() + ":" + lapTime.ToString("F0") + "." + lapDecimal.ToString("F0");
        }

        if(currentLap == 11 && !finishedTimeTrial)
        {
            finishedTimeTrial = true;
            fillInArray = true;
        }
    }

    private void Timer()
    {
        // restarts your lap after finishing and saving your best time
        if (lapFinished == true)
        {
            SaveLap();
            // sets the Best laptime to the Fastest Laptime When you go faster than the current fastest laptime
            if (lapTime < bestTime && lapTime > 0 && currentLapMinutes <= bestLapMinutes)
            {
                SetBestTime();
            }

            lapTime = 0;
        }
    }

    // gets called every time you enter a trigger
    private void OnTriggerEnter(Collider enter)
    {
        // Makes a new lap start if you go past the start finish line collider
        if (enter.tag == ("StartFinish") && !firstLap)
        {
            lapFinished = true;
            lapStarted = false;
        }
        else if (enter.tag == ("StartFinish") && firstLap)
        {
            firstLap = false;
        }

        Timer();
    }

    // gets called every time you exit a trigger
    private void OnTriggerExit(Collider exit)
    {
        // ends the last lap so the timer can restart
        if (exit.tag == ("StartFinish") && !firstLap)
        {
            lapFinished = false;
            lapStarted = true;

            currentLap++;
            currentLapUi.text = "Lap:" + currentLap.ToString();

            currentLapMinutes = 0;
        }

        Timer();
    }

    public void ConvertSecondToMinutes()
    {
        if(lapTime >= 60)
        {
            currentLapMinutes++;
            lapTime = 0;
        }
    }

    public void SaveLap()
    {
        PlayerPrefs.SetString("Laptime" + currentLap, currentLapMinutes.ToString("F0") + ":" + lapTime.ToString("F0") + "." + lapDecimal.ToString("F0"));
        PlayerPrefs.SetString("LaptimeFloat" + currentLap, currentLapMinutes.ToString("F0") + lapTime.ToString("F0") + lapDecimal.ToString("F0"));
    }

    public void SetBestTime()
    {
        bestLap = currentLap;
        bestTime = lapTime;
        bestLapMinutes = currentLapMinutes;
        bestLapDecimal = (bestTime % 1) * 1000;

        if (bestLapMinutes == 0)
        {
            bestLapUI.text = "Bestlap:" + bestLap.ToString();
            bestLapTimeUI.text = bestTime.ToString("F0") + "." + bestLapDecimal.ToString("F0");
        }
        else
        {
            bestLapUI.text = "Bestlap:" + bestLap.ToString();
            bestLapTimeUI.text = bestLapMinutes.ToString() + ":" + bestTime.ToString("F0") + "." + bestLapDecimal.ToString("F0");
        }

        PlayerPrefs.SetInt("BestLap", bestLap);
        PlayerPrefs.SetInt("BestTimeMinute", bestLapMinutes);
        PlayerPrefs.SetFloat("BestTimeSeconds", bestTime);

        convertedTime = bestLapMinutes.ToString("F0") + bestTime.ToString("F0") + bestLapDecimal.ToString("F0");
        convertedText = int.Parse(convertedTime);

        multiplayer.SendLeaderBoard(convertedText);
    }

    public void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTimeSeconds");
        bestLapMinutes = PlayerPrefs.GetInt("BestTimeMinute");
        bestLap = PlayerPrefs.GetInt("BestLap");
        bestLapDecimal = (bestTime % 1) * 1000;

        if (bestLapMinutes != 0)
        {
            bestLapUI.text = "Bestlap:" + bestLap.ToString();
            bestLapTimeUI.text = bestLapMinutes.ToString() + ":" + bestTime.ToString("F0") + "." + bestLapDecimal.ToString("F0");
        }
        else
        {
            bestLapUI.text = "Bestlap:" + bestLap.ToString();
            bestLapTimeUI.text = bestTime.ToString("F0") + "." + bestLapDecimal.ToString("F0");
        }

        if (PlayerPrefs.GetFloat("BestTimeSeconds") == 0 || PlayerPrefs.GetFloat("BestTimeSeconds") == 10000)
        {
            bestTime = 10000;
            bestLapMinutes = 1000;
            PlayerPrefs.SetFloat("BestTimeSeconds", 10000);
            PlayerPrefs.SetInt("BestTimeMinute", bestLapMinutes);
            bestLapTimeUI.text = "NoTime";
        }
    }

    public void FinishedTimeTrial()
    {
        emptyArrays = 0;
        Time.timeScale = 0;

        times = new string[currentLap - 1];
        timesFloat = new float[currentLap -1];
        for (int i = 0; i < currentLap; i++)
        {
            if(i != 0)
            {
                emptyArrays = i - 1;
            }

            times[emptyArrays] = PlayerPrefs.GetString("Laptime" + i.ToString());

            float.TryParse(PlayerPrefs.GetString("LaptimeFloat" + i.ToString()), out timesFloat[emptyArrays]);

            timesFloat[emptyArrays] = timesFloat[emptyArrays] / 1000f;

            Loop(i);

            timesFloat[emptyArrays] = convertedMinute + timesFloat[emptyArrays];
            
            sortedFloats.Add(timesFloat[emptyArrays]);

            if(i == currentLap)
            {
                sortedFloats.Sort();
            }
        }
    }

    void Loop(int i)
    {
        if (timesFloat[emptyArrays] > 100)
        {
            timesFloat[emptyArrays] -= 100;
            convertedMinute += 60;

            Loop(i);
        }
    }
}
