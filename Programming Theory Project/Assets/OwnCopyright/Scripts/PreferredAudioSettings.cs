using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferredAudioSettings : SessionPersistent
{

    public float masterVolume;
    public PreferredAudioSettings()
    {
        this.masterVolume = 1f;
    }
    protected override AudioSettings GetDefault<AudioSettings>()
    {
        return new AudioSettings();
    }
}
