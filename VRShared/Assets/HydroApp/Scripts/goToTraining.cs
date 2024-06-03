using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToTraining : MonoBehaviour
{
    public void toTrainingScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
