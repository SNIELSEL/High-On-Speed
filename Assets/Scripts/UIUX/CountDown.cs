using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject[] images;
    public AudioSource audioPlayer;
    private float randomStartTime;

    public TimeAttack timer;
    public GameObject Car;
    public bool beginCountDown;
    public void Update()
    {
        if (beginCountDown)
        {
            beginCountDown = false;
            StartCountDown();
        }
    }

    public void StartCountDown()
    {
        randomStartTime = Random.Range(2, 5);

        StartCoroutine(CountingDown());
    }

    public void DisableImages()
    {
        images[0].SetActive(false);
        images[1].SetActive(false);
        images[2].SetActive(false);
        images[3].SetActive(false);
        images[4].SetActive(false);
        images[5].SetActive(false);
        images[6].SetActive(false);
    }

    public IEnumerator CountingDown()
    {
        DisableImages();
        images[7].SetActive(true);
        images[0].SetActive(true);
        yield return new WaitForSeconds(1);
        audioPlayer.Play();
        Debug.Log("hi");
        yield return new WaitForSeconds(1);
        DisableImages();
        images[1].SetActive(true);
        yield return new WaitForSeconds(1);
        DisableImages();
        images[2].SetActive(true);
        yield return new WaitForSeconds(1);
        DisableImages();
        images[3].SetActive(true);
        yield return new WaitForSeconds(1);
        DisableImages();
        images[4].SetActive(true);
        yield return new WaitForSeconds(1);
        DisableImages();
        images[5].SetActive(true);
        yield return new WaitForSeconds(randomStartTime);
        DisableImages();
        images[6].SetActive(true);
        Car.GetComponent<CarAceleration>().enabled = true;
        timer.startRace = true;
        yield return new WaitForSeconds(4);
        images[7].SetActive(false);
        DisableImages();

    }
}
