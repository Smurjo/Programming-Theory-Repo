using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferredAudioSettings : SessionPersistentSettings// INHERITANCE
{

    public float masterVolume;
    public PreferredAudioSettings()//constructor with no parameters demanded
                                //due to use of generic type in SessionPersistentSettings
    {
        this.masterVolume = -60f;
    }
    protected override AudioSettings GetDefault<AudioSettings>()// POLYMORPHISM
    {
        return new AudioSettings();
    }
}
