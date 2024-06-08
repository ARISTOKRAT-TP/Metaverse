using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Fusion;

public class ScaleChange : NetworkBehaviour
{
    [SerializeField]
    [Tooltip("Values to change scale")]
    public GameObject[] pipes;
    public GameObject popl_up, popl_down;
    public XRKnob xRKnob;
    public XRKnob dial;
    public XRKnob latr;
    public TextMeshProUGUI[] textsMesh;
    public TextMeshProUGUI temperature;
    public AudioSource audioSource;
    private int temperaturevalue;
    public float[] sizes = new float[5];
    public XRLever toggle;
    public float durationWater;
    public Toggle[] checkboxes;

    void Start()
    {
        if (pipes == null || pipes.Length == 0 || popl_up == null || popl_down == null ||
            xRKnob == null || dial == null || latr == null || textsMesh == null || 
            textsMesh.Length == 0 || temperature == null || audioSource == null || toggle == null ||
            checkboxes == null || checkboxes.Length == 0)
        {
            Debug.LogError("One or more components are not set in the inspector");
            return;
        }
        temperaturevalue = Random.Range(20, 25);
        temperature.text = "Температура воды:" + temperaturevalue.ToString() + " C";
    }

   public void fakeOnClick()
    {
        if (checkboxes[2].isOn == true && checkboxes[1].isOn == true)
        {
            Rpc_FakeOnClick();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void Rpc_FakeOnClick()
    {
        checkboxes[3].isOn = true;
    }

    public void ChangeScalePipes()
    {
        if (toggle.value == true && dial.value < 0.15)
        {
            Rpc_ChangeScale(dial.value, latr.value, xRKnob.value, toggle.value);
        }
        else
        {
            Rpc_ResetScale();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void Rpc_ChangeScale(float dialValue, float latrValue, float xRKnobValue, bool toggleValue)
    {
        Vector3[] newSize = new Vector3[pipes.Length];
        
        // Обновление состояния чекбоксов
        UpdateCheckboxes(dialValue, latrValue, xRKnobValue, toggleValue);

        if (dialValue < 0.15 && toggleValue == true)
        {
            sizes[0] = (101f / 1960f) * 1.77f * 1.77f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            sizes[1] = (81f / 1960f) * 1.43f * 1.43f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            sizes[2] = (-35f / 1960f) * 1.13f * 1.13f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            sizes[3] = (2.5f / 1960f) * 1.54f * 1.54f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            sizes[4] = (13f / 1960f) * 2.69f * 2.69f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;

            if (latrValue * 300 * 0.8 * (1 - xRKnobValue) < 125 && latrValue * 300 * 0.8 * (1 - xRKnobValue) > 0)
            {
                popl_up.transform.position = new Vector3(-14.3615999f, 1.4429f + ((latrValue * 2.3f * 0.8f * (1 - xRKnobValue)) * 0.3099f), 8.84440041f);
            }
            else if ((latrValue * 300 * 0.8 * (1 - xRKnobValue)) >= 125)
            {
                popl_down.transform.position = new Vector3(-14.3615999f, 1.4429f + (((latrValue) * 2.3f * 0.8f * (1 - xRKnobValue)) * 0.3229f) - 0.3f, 8.84440041f);
            }
            audioSource.volume = latrValue * 0.35f;

            for (int i = 0; i < pipes.Length; i++)
            {
                newSize[i] = new Vector3(pipes[i].transform.localScale.x, sizes[i] / 19.5f, pipes[i].transform.localScale.z);
                pipes[i].transform.localScale = newSize[i];
                textsMesh[i].text = Math.Round(sizes[i], 2).ToString() + "cm";
            }
        }
        else
        {
            Rpc_ResetScale();
        }

        Rpc_UpdateTextMeshes(sizes);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void Rpc_ResetScale()
    {
        Vector3[] newSize = new Vector3[pipes.Length];
        for (int i = 0; i < pipes.Length; i++)
        {
            sizes[i] = 19.5f;
            newSize[i] = new Vector3(pipes[i].transform.localScale.x, sizes[i] / 19.5f, pipes[i].transform.localScale.z);
            pipes[i].transform.localScale = newSize[i];
            textsMesh[i].text = Math.Round(sizes[i], 2).ToString() + "cm";
        }

        Rpc_UpdateTextMeshes(sizes);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void Rpc_UpdateTextMeshes(float[] updatedSizes)
    {
        for (int i = 0; i < updatedSizes.Length; i++)
        {
            textsMesh[i].text = Math.Round(updatedSizes[i], 2).ToString() + "cm";
        }
    }

    private void UpdateCheckboxes(float dialValue, float latrValue, float xRKnobValue, bool toggleValue)
    {
        if (toggleValue == true && dialValue < 0.15)
        {
            checkboxes[0].isOn = true;

            if (latrValue > 0)
            {
                checkboxes[1].isOn = true;
            }
            else if (checkboxes[4].isOn == false)
            {
                checkboxes[1].isOn = false;
                checkboxes[2].isOn = false;
                checkboxes[3].isOn = false;
                checkboxes[4].isOn = false;
            }

            if (1 - xRKnobValue > 0 && checkboxes[1].isOn == true)
            {
                checkboxes[2].isOn = true;
            }
            else if (1 - xRKnobValue <= 0 && checkboxes[3].isOn == true && checkboxes[2].isOn == true)
            {
                checkboxes[4].isOn = true;
            }
            else
            {
                checkboxes[2].isOn = false;
                checkboxes[3].isOn = false;
                checkboxes[4].isOn = false;
            }

            if (checkboxes[3].isOn == true && checkboxes[4].isOn == true && latrValue == 0)
            {
                checkboxes[5].isOn = true;
            }
        }
        else if (dialValue < 0.65 && dialValue > 0.45)
        {
            if (checkboxes[5].isOn == true)
            {
                checkboxes[6].isOn = true;
            }
            else
            {
                for (int i = 0; i < checkboxes.Length; i++)
                {
                    checkboxes[i].isOn = false;
                }
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            // Обновление размеров труб на владельце состояния
            Vector3[] newSize = new Vector3[pipes.Length];
            for (int i = 0; i < pipes.Length; i++)
            {
                newSize[i] = new Vector3(pipes[i].transform.localScale.x, sizes[i] / 19.5f, pipes[i].transform.localScale.z);
                pipes[i].transform.localScale = newSize[i];
                textsMesh[i].text = Math.Round(sizes[i], 2).ToString() + "cm";
            }
        }
    }
}