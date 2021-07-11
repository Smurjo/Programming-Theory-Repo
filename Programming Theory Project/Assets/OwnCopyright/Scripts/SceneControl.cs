using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneControl", menuName = "ScriptableObjects/SceneControl")]
public class SceneControl : ScriptableObject
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
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
