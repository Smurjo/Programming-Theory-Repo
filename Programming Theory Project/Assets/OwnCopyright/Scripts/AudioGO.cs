using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGO : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] GameEvents gameEvents;

    // Update is called once per frame
    void Update()
    {
        if (!audioManager.IsAudioPlaying())
            gameEvents.raiseAudioFinishedPlayingEvent();
    }
}
