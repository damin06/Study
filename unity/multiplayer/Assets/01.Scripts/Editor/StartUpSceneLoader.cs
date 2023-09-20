using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class StartUpSceneLoader
{
    static StartUpSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadStartUpScene;
    }

    private static void LoadStartUpScene(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            if(EditorSceneManager.GetActiveScene().buildIndex != 0) 
            {
                EditorSceneManager.LoadScene(0);
            }
        }
    }
}
