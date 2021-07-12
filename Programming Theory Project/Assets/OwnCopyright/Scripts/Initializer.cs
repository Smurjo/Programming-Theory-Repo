using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] GameEvents gameEvents;
    [SerializeField] GameObject audioGO;
    [SerializeField] SceneControl sceneControl;
    [SerializeField] AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.AudioGO = audioGO;
        audioManager.Initialize();
        sceneControl.Initialize();
        gameEvents.raiseApplicationStartEvent();
    }
    void OnDisable()
    {
        audioManager.CleanUp();
        sceneControl.CleanUp();
    }

}
