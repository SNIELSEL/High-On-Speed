using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;

public class PropEnabler : MonoBehaviour
{
    public GameObject[] particleObject;
    public GameObject[] probObject;

    public GameObject exhaust;

    private ParticleSystem individualParticle;
    private GameObject individualProp;

    private int enabledProps;
    private float oneSecDelay;
    private float loopDisableDelay;
    private bool propsEnabled;
    private bool usingIndividualEnabler;
    private bool usingLoopParticle;

    public void EnableParticleAndProp(int number)
    {
        if (particleObject[enabledProps].GetComponent<ParticleSystem>().main.loop == true)
        {
            usingLoopParticle = true;
        }

        if (!propsEnabled)
        {
            propsEnabled = true;
            enabledProps = number;
            loopDisableDelay = 5;
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

    public void IdividualPropEnabler(GameObject prop)
    {
        prop.SetActive(true);
        individualProp = prop;
    }

    public void IdividualParticleEnabler(ParticleSystem particle)
    {
        if(particle.main.loop == true)
        {
            usingLoopParticle = true;
        }

        if (!propsEnabled)
        {
            propsEnabled = true;
            oneSecDelay = 1;
            loopDisableDelay = 5;
            individualParticle = particle;
            usingIndividualEnabler = true;
            particle.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (propsEnabled)
        {
            loopDisableDelay -= Time.deltaTime;
            oneSecDelay -= Time.deltaTime;

            if(oneSecDelay <= 0 && !usingIndividualEnabler && !usingLoopParticle)
            {
                if (!particleObject[enabledProps].GetComponent<ParticleSystem>().isPlaying)
                {
                    DisableParticleAndProp();
                }
            }

            if(oneSecDelay <= 0 && usingIndividualEnabler && !usingLoopParticle)
            {
                if (!individualParticle.isPlaying)
                {
                    DisableIndividualParticleAndProp();
                }
            }

            if (loopDisableDelay <= 0 && usingLoopParticle)
            {
                if (usingIndividualEnabler)
                {
                    DisableIndividualParticleAndProp();
                }
                else if (!usingIndividualEnabler)
                {
                    DisableParticleAndProp();
                }
            }
        }
    }
    public void DisableIndividualParticleAndProp()
    {
        individualParticle.gameObject.SetActive(false);
        individualProp.gameObject.SetActive(false);

        usingLoopParticle = false;
        propsEnabled = false;
        usingIndividualEnabler = false;
    }

    public void DisableParticleAndProp()
    {
        particleObject[enabledProps].SetActive(false);
        probObject[enabledProps].SetActive(false);

        usingLoopParticle = false;
        propsEnabled = false;
    }
}
