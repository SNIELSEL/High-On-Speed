using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject[] images;
    private float randomStartTime;

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
    }

    public IEnumerator CountingDown()
    {
        DisableImages();
        images[0].SetActive(true);
        yield return new WaitForSeconds(2);
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
        images[0].SetActive(true);
        yield return new WaitForSeconds(3);
        DisableImages();

    }
}
