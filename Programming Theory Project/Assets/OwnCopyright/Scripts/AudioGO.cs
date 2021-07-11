using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGO : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    private AudioSource firstAudioSource;
    // Start is called before the first frame update
    void Start()
    {
       // audioManager.StartAudioSources(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstAudioSource == null)
        {
            firstAudioSource = GetComponent<AudioSource>();
            if (firstAudioSource != null)
            {
                audioManager.StartAudioSources(gameObject);
            }  }
        else
        {
            if (!firstAudioSource.isPlaying) audioManager.syncAudioClips();
        }
    }
}
