using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void OnCollisionStay(Collision collision)
    {
        Instantiate(particleSystem, collision.transform.position, collision.transform.rotation);
    }
}
