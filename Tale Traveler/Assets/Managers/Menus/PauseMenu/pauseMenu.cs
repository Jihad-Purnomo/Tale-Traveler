using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToLoad;
    [SerializeField] private SceneField[] scenesToUnload;

    [SerializeField] private GameObject SwitchMap;

    private void OnEnable()
    {
        Input.ChangeActionMap("UI");
        Input.SelectUI(SwitchMap);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Input.ChangeActionMap("GamePlay");
    }

    public void QuitToMenu()
    {
        GameManager.LoadScenes(scenesToLoad);
        GameManager.UnloadScenes(scenesToUnload);
    }
}
