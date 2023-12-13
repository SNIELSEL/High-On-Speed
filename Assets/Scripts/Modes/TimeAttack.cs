using TMPro;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    [SerializeField] private float lapTime;

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

        if (lapFinished == true)
        {
            PlayerPrefs.SetFloat("Laptime", lapTime);
            lapTime = 0;
            //float besttime = PlayerPrefs.GetFloat("Laptime");
        }
    }

    private void OnTriggerEnter(Collider enter)
    {
        if (gameObject.tag == ("StartFinish"))
        {
            lapFinished = true;
            lapStarted = false;

            Debug.Log("End Lap");
        }
    }

    private void OnTriggerExit(Collider exit)
    {

        if (gameObject.tag == ("StartFinish"))
        {
            lapFinished = false;
            lapStarted = true;

            Debug.Log("Start Lap");
        }
    }
}
