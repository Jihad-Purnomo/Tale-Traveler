using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToUnload;
    [SerializeField] private SceneField[] scenesToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Cutscene and wait
            GameManager.LoadScenes(scenesToLoad);
            GameManager.UnloadScenes(scenesToUnload);
        }
    }
}
