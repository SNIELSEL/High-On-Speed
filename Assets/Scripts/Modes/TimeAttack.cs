using TMPro;
using UnityEngine;

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

    [Header("Text Mesh Pro's")]
    [SerializeField] private TextMeshProUGUI lapTimeUI;
    [SerializeField] private TextMeshProUGUI bestLapTimeUI;
    [SerializeField] private TextMeshProUGUI currentLapUi;
    [SerializeField] private TextMeshProUGUI bestLapUI;

    [SerializeField] private bool startRace;
    [SerializeField] private bool fillInArray;

    private bool lapStarted;
    private bool lapFinished;
    private bool firstStart;
    private bool firstLap;

    private float lapDecimal;
    private float bestLapDecimal;

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

            Debug.Log("End Lap " + currentLap);
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

            Debug.Log("Start Lap " + currentLap);
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
        Time.timeScale = 0;

        times = new string[currentLap];
        for (int i = 0; i < currentLap; i++)
        {
            times[i] = PlayerPrefs.GetString("Laptime" + i.ToString());
        }
    }
}
