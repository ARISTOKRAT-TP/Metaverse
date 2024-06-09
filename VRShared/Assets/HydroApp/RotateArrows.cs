using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using Fusion;

public class RotateArrows : NetworkBehaviour
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

    // Network variable to synchronize the knob value
    [Networked] private float knobValue { get; set; }

    private void Start()
    {
        // Add listener to knob value change
        xRKnob.m_OnValueChange.AddListener(OnKnobValueChanged);
    }

    private void OnDestroy()
    {
        // Remove listener when the object is destroyed
        xRKnob.m_OnValueChange.RemoveListener(OnKnobValueChanged);
    }

    private void OnKnobValueChanged(float value)
    {
        if (Object.HasStateAuthority)
        {
            knobValue = value;
            RPC_SetKnobRotation(value);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_SetKnobRotation(float value)
    {
        knobValue = value;
        SetKnobRotation();
    }

    private void SetKnobRotation()
    {
        if (m_Voltage != null)
            m_Voltage.localEulerAngles = new Vector3(((knobValue * (-90)) + 53), 0.0f, 0.0f);
        if (m_Power != null)
            m_Power.localEulerAngles = new Vector3(((knobValue * (-270)) + 125), 0.0f, 0.0f);
        if (m_Press1 != null)
            m_Press1.localEulerAngles = new Vector3(((knobValue * (240)) - 115), 0.0f, 0.0f);
        if (m_Press2 != null)
            m_Press2.localEulerAngles = new Vector3(((knobValue * (-240)) + 115), 0.0f, 0.0f);
    }
}