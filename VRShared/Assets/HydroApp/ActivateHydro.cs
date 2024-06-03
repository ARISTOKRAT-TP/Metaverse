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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ActivateMeh(){
    
        if (toggle.value == true) {

        if (dial.value < 0.15)
        {
            checkbox.isOn = true;
         lamp.SetActive(true);
         lamprend.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red*100);
         audioSource.SetActive(true);
        }
        else if (dial.value < 0.65 && dial.value > 0.45)
        {
            checkbox.isOn = false;
         lamp.SetActive(false);
         lamprend.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);
         audioSource.SetActive(false);
        }
        }
    }
}
