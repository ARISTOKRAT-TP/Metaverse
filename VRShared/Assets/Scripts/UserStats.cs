using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using Fusion.Addons.ConnectionManagerAddon;

public class UserStats : NetworkBehaviour
{
    private ChangeDetector _changes;
    [Networked(), OnChangedRender(nameof(UpdateUserName))] 
    public NetworkString<_32> UserName {get; set;}
    [SerializeField] TextMeshPro userNameLabel;

    public static UserStats instance;

    public Image speakingIndicator;
    [Networked(),OnChangedRender(nameof(UpdateSpeakingIndicator))]
    public NetworkBool isSpeaking {get; set;}   
    
    private void Start()
{
    if (this.HasStateAuthority)
    {
        UserName = ConnectionManager.instance._userName; // дописать
        if (instance == null) {instance = this;}
    }
}
    void UpdateSpeakingIndicator()
    {
        bool _isSpeaking = isSpeaking;
        Image _speakingIndicator = speakingIndicator;
        if (_isSpeaking) {
        _speakingIndicator.enabled = true;
        }
        else {
            _speakingIndicator.enabled = false;
        }

    }
 public override void Spawned()
    {
        UpdateUserName();
    }
    void UpdateUserName()
    {
        userNameLabel.text = UserName.Value;
    }
}
