using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneControl", menuName = "ScriptableObjects/SceneControl")]
public class SceneControl : ScriptableObject
{
    [SerializeField] private int startSceneIndex = 0;
    [SerializeField] private int mainSceneIndex = 1;
    [SerializeField] private GameEvents gameEvents;

    public void Initialize()
    {
        gameEvents.applicationStartEvent += LoadStartScene;// POLYMORPHISM?
        gameEvents.mainSceneLoadRequestedEvent += LoadMainScene;// POLYMORPHISM?
        gameEvents.menuSceneLoadRequestedEvent += LoadStartScene;// POLYMORPHISM?
        gameEvents.applicationExitEvent += Exit;// POLYMORPHISM?
    }

    public void CleanUp()
    {
        gameEvents.applicationStartEvent -= LoadStartScene;
        gameEvents.mainSceneLoadRequestedEvent -= LoadMainScene;
        gameEvents.menuSceneLoadRequestedEvent -= LoadStartScene;
        gameEvents.applicationExitEvent -= Exit;
    }


    public void LoadMainScene()
    {
        Scene currentScene = SceneManager.GetSceneAt(1);
        SceneManager.LoadSceneAsync(mainSceneIndex, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
    }
    public void LoadStartScene()
    {
        if (SceneManager.sceneCount > 1) //in case of coming back from the game,
                                         //else the one and only scene is the audio scene
        {
            Scene currentScene = SceneManager.GetSceneAt(1);
            SceneManager.UnloadSceneAsync(currentScene);
        }
        SceneManager.LoadSceneAsync(startSceneIndex, LoadSceneMode.Additive);
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
