using TMPro;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    [Header("lap time")]
    [SerializeField] private float lapTime;
    [SerializeField] private float bestTime;

    [Header("Text Mesh Pro's")]
    [SerializeField] private TextMeshProUGUI lapTimeUI;
    [SerializeField] private TextMeshProUGUI bestLapTime;

    private bool lapStarted;
    private bool lapFinished;

    // Start is called before the first frame update
    void Start()
    {
        lapFinished = false;
        lapFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        lapTimeUI.text = lapTime.ToString();

        //runs the code in the void timer
        Timer();
    }

    private void Timer()
    {
        // makes the timer go up if you have started a lap
        if (lapStarted == true)
        {
            lapTime += 1 * Time.deltaTime;
        }

        // restarts your lap after finishing and saving your best time
        if (lapFinished == true)
        {
            PlayerPrefs.SetFloat("Laptime", lapTime);
            lapTime = 0;

            // sets the Best laptime to the Fastest Laptime When you go faster than the current fastest laptime
            if (lapTime < bestTime && lapTime > 0)
            {
                 bestTime = PlayerPrefs.GetFloat("Laptime");
            }
        }
    }

    // gets called every time you enter a trigger
    private void OnTriggerEnter(Collider enter)
    {
        // Makes a new lap start if you go past the start finish line collider
        if (gameObject.tag == ("StartFinish"))
        {
            lapFinished = true;
            lapStarted = false;

            Debug.Log("End Lap");
        }
    }

    // gets called every time you exit a trigger
    private void OnTriggerExit(Collider exit)
    {
        // ends the last lap so the timer can restart
        if (gameObject.tag == ("StartFinish"))
        {
            lapFinished = false;
            lapStarted = true;

            Debug.Log("Start Lap");
        }
    }
}
