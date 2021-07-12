using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferredAudioSettings : SessionPersistentSettings
{

    public float masterVolume;
    public PreferredAudioSettings()
    {
        this.masterVolume = -60f;
    }
    protected override AudioSettings GetDefault<AudioSettings>()
    {
        return new AudioSettings();
    }
}
