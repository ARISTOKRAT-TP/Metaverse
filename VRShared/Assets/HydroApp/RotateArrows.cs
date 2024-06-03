using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class RotateArrows : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The object that is visually grabbed and manipulated")]
    Transform m_Voltage = null;
    [SerializeField]
    Transform m_Power = null;
    [SerializeField]
    Transform m_Press1 = null;
    [SerializeField]
    Transform m_Press2 = null;
     public XRKnob xRKnob;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetKnobRotation()
        {
        // if (xRKnob.m_AngleIncrement > 0)
        // {
        //     var normalizeAngle = xRKnob.value*180 - -220;
        //     angle = (Mathf.Round(normalizeAngle / 1) * 1) + -220;
        // }

        if (m_Voltage != null)
            m_Voltage.localEulerAngles = new Vector3(((xRKnob.value * (-90)) + 53), 0.0f, 0.0f);
        if (m_Power != null)
            m_Power.localEulerAngles = new Vector3(((xRKnob.value * (-270)) + 125), 0.0f, 0.0f);
        if (m_Press1 != null)
            m_Press1.localEulerAngles = new Vector3(((xRKnob.value * (240)) - 115), 0.0f, 0.0f);
        if (m_Press2 != null)
            m_Press2.localEulerAngles = new Vector3(((xRKnob.value * (-240)) + 115), 0.0f, 0.0f);
    }
}
