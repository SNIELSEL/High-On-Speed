using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject[] exhausts;
    public void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }

    public void PlayExhaust()
    {
        for (int i = 0; i < exhausts.Length; i++)
        {
            exhausts[i].GetComponent<ParticleSystem>().Play();
        }
    }
}
