using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteTheparticle());
    }

    private IEnumerator DeleteTheparticle()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
