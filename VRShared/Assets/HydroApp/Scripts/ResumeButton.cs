using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject handmenu;

    public void ResumeButtonPressed()
    {
        handmenu.SetActive(false);
    }
}
