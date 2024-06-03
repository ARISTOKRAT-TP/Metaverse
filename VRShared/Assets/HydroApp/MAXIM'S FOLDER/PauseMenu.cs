using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem.XR;

public class PauseMenu : MonoBehaviour
{
    public GameObject wristUI;
    private bool activeWristUI = false;
    public XRController controller;
  public InputActionReference menuButtonReference;
    void Start()
    {
        // DisplayWristUI();
    }

    // public void PauseButtonPressed(InputAction.CallbackContext m_IsTrackedAction)
    // {
    //     if(m_IsTrackedAction.performed)
    //     {
    //         DisplayWristUI();
    //     }
    // }

    // public void DisplayWristUI()
    // {
    //     if(activeWristUI)
    //     {
    //         wristUI.SetActive(false);
    //         activeWristUI = false;
    //         Time.timeScale = 1;
    //     } 
    //     else if (!activeWristUI)
    //     {
    //         wristUI.SetActive(true);
    //         activeWristUI = true;
    //         Time.timeScale = 0;
    //     }
    // }
    private void OnEnable()
    {
        if (menuButtonReference != null && menuButtonReference.action != null)
        {
            menuButtonReference.action.Enable();
            menuButtonReference.action.started += TogglePauseMenu;
            Debug.Log("Subscribed to menu button action.");
        }
        else
        {
            Debug.LogError("Menu button action reference is not set or action is null.");
        }
    }

    private void OnDisable()
    {
        if (menuButtonReference != null && menuButtonReference.action != null)
        {
            menuButtonReference.action.started -= TogglePauseMenu;
            Debug.Log("Unsubscribed from menu button action.");
        }
    }

    private void TogglePauseMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Menu button pressed: " + context);
        activeWristUI = !activeWristUI;
        wristUI.SetActive(activeWristUI);
        Debug.Log("Pause menu state: " + activeWristUI);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
