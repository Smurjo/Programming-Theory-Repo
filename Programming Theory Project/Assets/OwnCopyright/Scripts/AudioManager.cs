using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioManager", menuName = "ScriptableObjects/AudioManager")]
public class AudioManager : ScriptableObject
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] GameObject AudioSourcePrefab;
    [SerializeField] AudioMixer masterMixer;

    [SerializeField] AudioSource[] audioSources;
    private double pauseForSyncSeconds = 0.1f;

    public void StartAudioSources(GameObject audioGO)
    {
        if (audioSources == null)
        {
            audioSources = new AudioSource[audioClips.Length];
        }
        audioSources[0] = audioGO.GetComponent<AudioSource>() as AudioSource;
        audioSources[0].clip = audioClips[0];
        for (int i = 1; i < audioClips.Length; i++)
        {
            audioSources[i] = audioGO.AddComponent<AudioSource>() as AudioSource;
            audioSources[i].clip = audioClips[i];
        }
    syncAudioClips();
}

public void OnVolumeChanged(float volume)
{
    // Debug.Log("AudioManager: audioVolumeChangedEvent event received, newVolume " + volume);
    if (volume < -40)//to switch sound completely off at lower end of the scale
    {
        volume = -80;
    }
    masterMixer.SetFloat("MasterVolume", volume);
}

public void syncAudioClips()
{//plays two clips selected from the same theme
    double audioSyncTime = AudioSettings.dspTime + pauseForSyncSeconds;
    foreach (var audioSource in audioSources)
    {
        audioSource.Stop();
        audioSource.PlayScheduled(audioSyncTime); //play scheduled to keep both audio sources synchronized
    }
}
}

