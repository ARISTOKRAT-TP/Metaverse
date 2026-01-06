using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Fusion;

public class TalkStatus : MonoBehaviour
{
    private Recorder recorder;
    private void Awake()
    {


        if (recorder == null)
        {
            recorder = GetComponent<Recorder>();
        }
    }
    void Update()
    {
        if (UserStats.instance) {
        if (recorder.VoiceDetector.Detected)
        {
            UserStats.instance.isSpeaking = true;
        }
        else
        {
            UserStats.instance.isSpeaking = false;
        }
        }
    }
}
