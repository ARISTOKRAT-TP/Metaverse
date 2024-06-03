using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using TMPro;
using Random=UnityEngine.Random;
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
     public float size1,size2,size3,size4,size5 = 1.0f;
    public XRLever toggle;
    public float durationWater;
    public Toggle checkboxStep2;
    public Toggle checkboxStep3;
    public Toggle checkboxStep4;
    public Toggle checkboxStep5;
    public Toggle checkboxStep6;
    public Toggle checkboxStep7;
    public Toggle checkboxStep8;
    


    // [SerializeField] private_float _speed = 1f;
    // private Vector3 _newPosition;

    // private void Move() 
    // {
    //     _newPosition = Vector3(transform.position.x, transform.position.y, transform.position.z + _speed);
    //     transform.position = _newPosition;
    // }


    // Start is called before the first frame update
    void Start()
    {
        temperaturevalue = Random.Range(20, 25);
        temperature.text = "Температура воды:" + temperaturevalue.ToString() + " C";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fakeOnClick()
    {
        if(checkboxStep4.isOn == true && checkboxStep3.isOn == true)
        {
            checkboxStep5.isOn = true;
        }
    }

    public void ChangeScalePipes()
    {
        Vector3 newSize = transform.localScale;
        Vector3 newSize2 = transform.localScale;
        Vector3 newSize3 = transform.localScale;
        Vector3 newSize4 = transform.localScale;
        Vector3 newSize5 = transform.localScale;
        // Для верхнего поплавка
        // при ЛАТР * 0.8 * ВН1 < 125

        // Это для нижнего
        // при ЛАТР * 0.8 * ВН1 >= 125
        // ((ЛАТР * ВН1) - 125) * 0.514

        // if (xRKnob.m_AngleIncrement > 0)
        // {
        //     var normalizeAngle = xRKnob.value*180 - -220;
        //     angle = (Mathf.Round(normalizeAngle / 1) * 1) + -220;
        // }

        //************ЧЕКБОКСЫ***************
        if (toggle.value == true && dial.value < 0.15)
        {
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
        //************ЧЕКБОКСЫ***************

        if (dial.value < 0.15 && toggle.value == true) {
            
            size1 = (101f / 1960f) * 1.77f * 1.77f * latr.value * 300 * (1-xRKnob.value) + 19.5f;
            size2 = (81f / 1960f) * 1.43f * 1.43f * latr.value * 300 * (1-xRKnob.value) + 19.5f;
            size3 = (-35f / 1960f) * 1.13f * 1.13f * latr.value * 300 * (1-xRKnob.value) + 19.5f;
            size4 = (2.5f / 1960f) * 1.54f * 1.54f * latr.value * 300 * (1-xRKnob.value) + 19.5f;
            size5 = (13f / 1960f) * 2.69f * 2.69f * latr.value * 300 * (1-xRKnob.value) + 19.5f;

            if (latr.value * 300 * 0.8 * (1 - xRKnob.value) < 125 && latr.value * 300 * 0.8 * (1 - xRKnob.value) > 0)
            {
                Debug.Log(latr.value * 300 * 0.8 * (1 - xRKnob.value));
                popl_up.transform.position = new Vector3(-14.3615999f, 1.4429f + ((latr.value * 2.3f * 0.8f * (1 - xRKnob.value)) * 0.3099f), 8.84440041f);
                ///// ШАГ - 0.0024792 (0.3099)
                // popl_up.transform.position.y = (latr.value * 300 * 0.8) // от 1.4429f до 1.7528f
                // popl_up.transform.position += new Vector3(0, 0.0001f, 0);
                // (1.7528f - 1.4429f) / (latr.value * 300 * 0.8 * (1 - xRKnob.value))
                // Vector3(-14.3615999, 1.44289994, 8.84440041)
            } // else popl_down.transform.position = new Vector3(-14.3632f, 1.4223f + ((latr.value * 2.3f * 0.8f * (1 - xRKnob.value) - 125) * 0.514f), 8.8415f);
            else if ((latr.value * 300 * 0.8 * (1 - xRKnob.value)) >= 125)
            {
                popl_down.transform.position = new Vector3(-14.3615999f, 1.4429f + (((latr.value) * 2.3f * 0.8f * (1 - xRKnob.value)) * 0.3229f) - 0.3f, 8.84440041f);
            }
            audioSource.volume = latr.value * 0.35f;
            newSize.y = size1/19.5f;
            newSize2.y = size2/19.5f;
            newSize3.y = size3/19.5f;
            newSize4.y = size4/19.5f;
            newSize5.y = size5/19.5f;
            pipe1.transform.localScale = newSize;
            pipe2.transform.localScale = newSize2;
            pipe3.transform.localScale = newSize3;
            pipe4.transform.localScale = newSize4;
            pipe5.transform.localScale = newSize5;
            textMesh.text = Math.Round(size1, 2).ToString() + "cm";
            textMesh2.text = Math.Round(size2, 2).ToString() + "cm";
            textMesh3.text = Math.Round(size3, 2).ToString() + "cm";
            textMesh4.text = Math.Round(size4, 2).ToString() + "cm";
            textMesh5.text = Math.Round(size5, 2).ToString() + "cm";
            // scaleChange = (1.0f, -0.005f, 1.0f); ((((xRKnob.value*300/1.77)*(xRKnob.value*300/1.77))/1960)*0.00118*980*0.47)*146.238101799;
            Debug.Log(size1);
        } else {
            size1 = 19.5f;
            size2 = 19.5f;
            size3 = 19.5f;
            size4 = 19.5f;
            size5 = 19.5f;
            newSize.y = size1/19.5f;
            newSize2.y = size2/19.5f;
            newSize3.y = size3/19.5f;
            newSize4.y = size4/19.5f;
            newSize5.y = size5/19.5f;
            pipe1.transform.localScale =  newSize;
            pipe2.transform.localScale =  newSize2;
            pipe3.transform.localScale =  newSize3;
            pipe4.transform.localScale =  newSize4;
            pipe5.transform.localScale =  newSize5;
            textMesh.text = Math.Round(size1, 2).ToString() + "cm";
            textMesh2.text = Math.Round(size2, 2).ToString() + "cm";
            textMesh3.text = Math.Round(size3, 2).ToString() + "cm";
            textMesh4.text = Math.Round(size4, 2).ToString() + "cm";
            textMesh5.text = Math.Round(size5, 2).ToString() + "cm";
        }
    }

}
