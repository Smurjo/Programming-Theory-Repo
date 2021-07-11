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
        SceneManager.LoadScene(mainSceneIndex);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(startSceneIndex);
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
