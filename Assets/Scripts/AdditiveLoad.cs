using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveLoad : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
