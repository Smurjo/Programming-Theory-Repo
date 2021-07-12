using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioManager", menuName = "ScriptableObjects/AudioManager")]
public class AudioManager : ScriptableObject
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioWeights[] audioWeights;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private List<AudioSource> audioSources;

    private GameObject m_AudioGO = null;
    private PreferredAudioSettings preferredAudioSettings = null;

    public GameObject AudioGO
    {
        private get
        {
            return m_AudioGO;
        }
        set
        {
            m_AudioGO = value;
            StartAudioSources();
        }
    }
    private double pauseForSyncSeconds = 0.1f;
    public void Initialize()
    {
        gameEvents.audioFinishedPlayingEvent += SyncAudioClips;
        gameEvents.audioVolumeChangedEvent += OnVolumeChanged;
        gameEvents.mainSceneLoadRequestedEvent += setMainAudioWeights;
        gameEvents.menuSceneLoadRequestedEvent += setStartAudioWeights;
        gameEvents.playerWantsToBuyEvent += setBuyAudioWeights;
        gameEvents.playerWantsToSellEvent += setSellAudioWeights;
        preferredAudioSettings = new PreferredAudioSettings();
        preferredAudioSettings = preferredAudioSettings.FromFile<PreferredAudioSettings>();
        //Debug.Log("AudioManager: Initialize, preferredAudioSettings.masterVolume" + preferredAudioSettings.masterVolume);
        masterMixer.SetFloat("MasterVolume", preferredAudioSettings.masterVolume);
    }

    public void CleanUp()
    {
        gameEvents.audioFinishedPlayingEvent -= SyncAudioClips;
        gameEvents.audioVolumeChangedEvent -= OnVolumeChanged;
        gameEvents.mainSceneLoadRequestedEvent -= setMainAudioWeights;
        gameEvents.menuSceneLoadRequestedEvent -= setStartAudioWeights;
        gameEvents.playerWantsToBuyEvent -= setBuyAudioWeights;
        gameEvents.playerWantsToSellEvent -= setSellAudioWeights;
        if (audioSources != null)//just in case something is left over from the SO
        {
            audioSources = null;
        }
    }

    public void setStartAudioWeights()
    {
        setAudioWeights(0);//startAudioWeights
    }
    public void setMainAudioWeights()
    {
        setAudioWeights(1);//mainAudioWeights
    }
    public void setBuyAudioWeights()
    {
        setAudioWeights(2);//buyAudioWeights
    }
    public void setSellAudioWeights()
    {
        setAudioWeights(3);//sellAudioWeights
    }
    public bool IsAudioPlaying()
    {
        if (audioSources == null)
            return false;
        else if (audioSources[0] == null)
            return false;
        else
            return audioSources[0].isPlaying;
    }
    private void setAudioWeights(int weightIndex)
    {
        if (audioSources != null)
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                audioSources[i].volume = audioWeights[weightIndex].clipVolumes[i];
                //Debug.Log("AudioManager setAudioWeights volume " + i + ": " + audioSources[i].volume);
            }
        }
    }
    public void StartAudioSources()
    {
        if (audioSources == null)
        {
            audioSources = new List<AudioSource>();
        }
        //Debug.Log("AudioManager: StartAudioSources, audioSources.Count " + audioSources.Count);

        AudioMixerGroup[] mixerGroups = masterMixer.FindMatchingGroups("Master/MusicGroup");
        for (int i = 0; i < audioClips.Length; i++)
        {
            AudioSource audioSource = m_AudioGO.AddComponent<AudioSource>() as AudioSource;
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            audioSource.clip = audioClips[i];
            audioSources.Add(audioSource);
        }
        setStartAudioWeights();
        SyncAudioClips();
       // Debug.Log("AudioManager: StartAudioSources, audioSources.Count " + audioSources.Count);
    }

    public void OnVolumeChanged(float volume)
    {
        // Debug.Log("AudioManager: audioVolumeChangedEvent event received, newVolume " + volume);
        if (volume < -40)//to switch sound completely off at lower end of the scale
        {
            volume = -80;
        }
        masterMixer.SetFloat("MasterVolume", volume);
        preferredAudioSettings.masterVolume = volume;
        preferredAudioSettings.ToFile();
    }

    public void SyncAudioClips()
    {//if playing more clips from the same theme
        if (audioSources != null)
        {
            double audioSyncTime = AudioSettings.dspTime + pauseForSyncSeconds;
            foreach (var audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.PlayScheduled(audioSyncTime); //play scheduled to keep both audio sources synchronized
                }
            }
        }
    }
}

