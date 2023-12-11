using TMPro;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    private float lapTime;

    private bool lapStarted;
    private bool lapFinished;

    [SerializeField] private TextMeshProUGUI lapTimeUI;
    [SerializeField] private TextMeshProUGUI bestLapTime;

    // Start is called before the first frame update
    void Start()
    {
        lapFinished = false;
        lapFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        //runs the code in the void timer
        Timer();
    }

    private void Timer()
    {
        // makes the timer go up if you have started a lap
        if (lapStarted == true)
        {
            lapTime += Time.deltaTime;
        }

        if (lapFinished == true)
        {
            PlayerPrefs.SetFloat("Laptime", lapTime);
            lapTime = 0;
            //float besttime = PlayerPrefs.GetFloat("Laptime");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lapFinished = true;
        lapStarted = false;
    }

    private void OnTriggerExit(Collider other)
    {
        lapStarted = true;
        lapFinished = false;
    }
}
