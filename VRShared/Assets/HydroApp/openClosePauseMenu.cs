using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class openClosePauseMenu : MonoBehaviour
{
    public GameObject tmpObject;
    void Awake()
    {
        Debug.Log("Updating");
        tmpObject.SetActive(true);
        // tmpObject.SetActive(false);
    }
}
