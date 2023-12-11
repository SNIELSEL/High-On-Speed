using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropEnabler : MonoBehaviour
{
    public GameObject[] particleObject;
    public GameObject[] probObject;

    public GameObject exhaust;

    private int enabledProps;
    private bool propsEnabled;
    private float oneSecDelay;

    public virtual void EnableParticleAndProp(int number)
    {
        if (!propsEnabled)
        {
            propsEnabled = true;
            enabledProps = number;
            oneSecDelay = 1;

            if (particleObject[number] != null)
            {
                particleObject[number].SetActive(true);

                if(particleObject[number].name == exhaust.name)
                {
                    particleObject[number+ 1].SetActive(true);
                }
            }

            if (probObject[number] != null)
            {
                probObject[number].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (propsEnabled)
        {
            oneSecDelay -= Time.deltaTime;

            if(oneSecDelay <= 0)
            {
                if (!particleObject[enabledProps].GetComponent<ParticleSystem>().isPlaying)
                {
                    DisableParticleAndProp();
                }
            }
        }
    }

    public void DisableParticleAndProp()
    {
        particleObject[enabledProps].SetActive(false);
        probObject[enabledProps].SetActive(false);

        propsEnabled = false;
    }
}
