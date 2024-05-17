using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToLoad;
    [SerializeField] private SceneField[] scenesToUnload;

    [SerializeField] private GameObject firstSelected;

    private void Start()
    {
        Input.SelectUI(firstSelected);
    }

    public void StartLevel()
    {
        GameManager.LoadScenes(scenesToLoad);
        GameManager.UnloadScenes(scenesToUnload);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
