using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTheLabScript : MonoBehaviour
{
    public GameObject handmenu;
    public int sceneIndex;

    public void ExitTheLab()
    {
        handmenu.SetActive(false);
        SceneManager.LoadScene(sceneIndex);
    }
}
