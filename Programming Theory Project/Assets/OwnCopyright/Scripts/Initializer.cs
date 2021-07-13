using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;// ENCAPSULATION
    [SerializeField] private GameObject audioGO;// ENCAPSULATION
    [SerializeField] private SceneControl sceneControl;// ENCAPSULATION
    [SerializeField] private AudioManager audioManager;// ENCAPSULATION
    // Start is called before the first frame update
    void Start()
    {
        audioManager.AudioGO = audioGO;
        audioManager.Initialize();// ABSTRACTION
        sceneControl.Initialize();// ABSTRACTION
        gameEvents.raiseApplicationStartEvent();
    }
    void OnDisable()
    {
        audioManager.CleanUp();// ABSTRACTION
        sceneControl.CleanUp();// ABSTRACTION
    }

}
