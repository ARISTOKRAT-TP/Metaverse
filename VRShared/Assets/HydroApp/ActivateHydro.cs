using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;

public class ActivateHydro : NetworkBehaviour
{
    public GameObject lamp;
    public GameObject lamprend;
    public GameObject audioSource;
    public XRKnob dial;
    public XRLever toggle;
    public Toggle checkbox;

    void Start()
    {
        // Здесь можно добавить инициализацию, если требуется
    }

    void Update()
    {
        // Обновления, если требуется
    }

    public void ActivateMeh()
    {
        if (toggle.value == true)
        {
            if (dial.value < 0.15f)
            {
                Rpc_ActivateMeh(true, true, Color.red * 100, true);
            }
            else if (dial.value < 0.65f && dial.value > 0.45f)
            {
                Rpc_ActivateMeh(false, false, Color.red, false);
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_ActivateMeh(bool checkboxState, bool lampState, Color emissionColor, bool audioState)
    {
        // Убедитесь, что обновления состояния происходят корректно
        checkbox.isOn = checkboxState;
        lamp.SetActive(lampState);
        Renderer renderer = lamprend.GetComponent<Renderer>();
        if (renderer != null && renderer.materials.Length > 1)
        {
            renderer.materials[1].SetColor("_EmissionColor", emissionColor);
        }
        audioSource.SetActive(audioState);
    }
}