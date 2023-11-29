using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropEnabler : MonoBehaviour
{
    public GameObject[] particleObject;
    public GameObject[] probObject;

    public void EnableParticleAndProp(int number)
    {
        particleObject[number].SetActive(true);
        probObject[number].SetActive(true);
    }

    public void DisableParticleAndProp(int number)
    {
        particleObject[number].SetActive(false);
        probObject[number].SetActive(false);
    }
}
