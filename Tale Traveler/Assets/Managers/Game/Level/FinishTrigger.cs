using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToUnload;
    [SerializeField] private SceneField[] scenesToLoad;

    public Dialogue dialogue;
    private bool triggerFinish = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueManager.Inst.StartDialogue(dialogue);
            triggerFinish = true;
        }
    }

    private void Update()
    {
        if (!DialogueManager.Inst.inDialogue && triggerFinish)
        {
            GameManager.LoadScenes(scenesToLoad);
            GameManager.UnloadScenes(scenesToUnload);
        }
    }
}
