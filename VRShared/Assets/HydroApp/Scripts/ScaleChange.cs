
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
    public GameObject pipe1, pipe2, pipe3, pipe4, pipe5, popl_up, popl_down;
    public XRKnob xRKnob;
    public XRKnob dial;
    public XRKnob latr;
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI textMesh2;
    public TextMeshProUGUI textMesh3;
    public TextMeshProUGUI textMesh4;
    public TextMeshProUGUI textMesh5;
    public TextMeshProUGUI temperature;
    public AudioSource audioSource;
    private int temperaturevalue;

    public Toggle checkboxStep2;
    public Toggle checkboxStep3;
    public Toggle checkboxStep4;
    public Toggle checkboxStep5;
    public Toggle checkboxStep6;
    public Toggle checkboxStep7;
    public Toggle checkboxStep8;

    [Networked]
    public float size1 { get; set; }
    [Networked]
    public float size2 { get; set; }
    [Networked]
    public float size3 { get; set; }
    [Networked]
    public float size4 { get; set; }
    [Networked]
    public float size5 { get; set; }

    public XRLever toggle;
    public float durationWater;

    void Start()
    {
        temperaturevalue = Random.Range(20, 25);
        temperature.text = "Температура воды:" + temperaturevalue.ToString() + " C";
    }

    public void ChangeScalePipes()
    {
        if (toggle.value == true && dial.value < 0.15)
        {
            Rpc_ChangeScale(dial.value, latr.value, xRKnob.value, toggle.value);
            checkboxStep2.isOn = true;

            if(latr.value > 0)
            {
                checkboxStep3.isOn = true;
            } else if(checkboxStep6.isOn == false)
            {
                checkboxStep3.isOn = false;
                checkboxStep4.isOn = false;
                checkboxStep5.isOn = false;
                checkboxStep6.isOn = false;
            }
            if(1 - xRKnob.value > 0 && checkboxStep3.isOn == true)
            {
                checkboxStep4.isOn = true;
            } else if(1 - xRKnob.value <= 0 && checkboxStep5.isOn == true && checkboxStep4.isOn == true)
            {
                checkboxStep6.isOn = true;
            } else
            {
                checkboxStep4.isOn = false;
                checkboxStep5.isOn = false;
                checkboxStep6.isOn = false;
            }
            if(checkboxStep5.isOn == true && checkboxStep6.isOn == true && latr.value == 0) {
                checkboxStep7.isOn = true;
            }

        }   else if (dial.value < 0.65 && dial.value > 0.45)
        {
            if(checkboxStep7.isOn == true){
                checkboxStep8.isOn = true;
            } else {
                checkboxStep2.isOn = false;
                checkboxStep3.isOn = false;
                checkboxStep4.isOn = false;
                checkboxStep5.isOn = false;
                checkboxStep6.isOn = false;
                checkboxStep7.isOn = false;
                checkboxStep8.isOn = false;
            }
        }
        else
        {
            Rpc_ResetScale();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void Rpc_ChangeScale(float dialValue, float latrValue, float xRKnobValue, bool toggleValue)
    {
        if (dialValue < 0.15 && toggleValue == true)
        {
            size1 = (101f / 1960f) * 1.77f * 1.77f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            size2 = (81f / 1960f) * 1.43f * 1.43f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            size3 = (-35f / 1960f) * 1.13f * 1.13f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            size4 = (2.5f / 1960f) * 1.54f * 1.54f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            size5 = (13f / 1960f) * 2.69f * 2.69f * latrValue * 300 * (1 - xRKnobValue) + 19.5f;
            UpdateSizes();
        }
        else
        {
            Rpc_ResetScale();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void Rpc_ResetScale()
    {
        size1 = 19.5f;
        size2 = 19.5f;
        size3 = 19.5f;
        size4 = 19.5f;
        size5 = 19.5f;
        UpdateSizes();
    }

    private void UpdateSizes()
    {
        Vector3 newSize1 = new Vector3(pipe1.transform.localScale.x, size1 / 19.5f, pipe1.transform.localScale.z);
        Vector3 newSize2 = new Vector3(pipe2.transform.localScale.x, size2 / 19.5f, pipe2.transform.localScale.z);
        Vector3 newSize3 = new Vector3(pipe3.transform.localScale.x, size3 / 19.5f, pipe3.transform.localScale.z);
        Vector3 newSize4 = new Vector3(pipe4.transform.localScale.x, size4 / 19.5f, pipe4.transform.localScale.z);
        Vector3 newSize5 = new Vector3(pipe5.transform.localScale.x, size5 / 19.5f, pipe5.transform.localScale.z);

        pipe1.transform.localScale = newSize1;
        pipe2.transform.localScale = newSize2;
        pipe3.transform.localScale = newSize3;
        pipe4.transform.localScale = newSize4;
        pipe5.transform.localScale = newSize5;

        textMesh.text = Math.Round(size1, 2).ToString() + "cm";
        textMesh2.text = Math.Round(size2, 2).ToString() + "cm";
        textMesh3.text = Math.Round(size3, 2).ToString() + "cm";
        textMesh4.text = Math.Round(size4, 2).ToString() + "cm";
        textMesh5.text = Math.Round(size5, 2).ToString() + "cm";
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            UpdateSizes();
        }
    }
}