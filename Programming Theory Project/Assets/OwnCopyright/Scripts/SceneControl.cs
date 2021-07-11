using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneControl", menuName = "ScriptableObjects/SceneControl")]
public class SceneControl : ScriptableObject
{
    [SerializeField] private int startSceneIndex=0;
    [SerializeField] private int mainSceneIndex=1;
    public void LoadMainScene()
    {
     Scene currentScene=   SceneManager.GetSceneAt(1);
        SceneManager.LoadScene(mainSceneIndex,LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
    }
    public void LoadStartScene()
    {
        Scene currentScene = SceneManager.GetSceneAt(2);
        SceneManager.LoadScene(startSceneIndex, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
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
