using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToStart;

    private void Start()
    {
        LoadScenes(scenesToStart);
    }

    public static void LoadScenes(SceneField[] scenes)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == scenes[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
            }
        }
    }

    public static void UnloadScenes(SceneField[] scenes)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenes[i]);
        }
    }
}
