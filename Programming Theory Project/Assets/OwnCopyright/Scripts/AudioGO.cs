using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGO : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;// ENCAPSULATION
    [SerializeField] private GameEvents gameEvents;// ENCAPSULATION

    // Update is called once per frame
    void Update()
    {
        if (!audioManager.IsAudioPlaying())// ABSTRACTION
            gameEvents.raiseAudioFinishedPlayingEvent();// ABSTRACTION
    }
}
