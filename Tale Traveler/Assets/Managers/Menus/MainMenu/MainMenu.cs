using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToLoad;
    [SerializeField] private SceneField[] scenesToUnload;

    [SerializeField] private ObjectSound MainMenuMusic;

    [SerializeField] private GameObject emptyButton;
    [SerializeField] private GameObject bookButton;

    private Animator bookAnim;

    private GameObject currentSelected;
    private bool bookOpened = false;

    private void Awake()
    {
        bookAnim = bookButton.GetComponent<Animator>();
    }

    private void Start()
    {
        Input.ChangeActionMap("UI");

        Input.SelectUI(bookButton);
        currentSelected = bookButton;
        currentSelected.transform.GetChild(0).gameObject.SetActive(true);

        AudioManager.Inst.PlayAudio(MainMenuMusic, transform);
    }

    private void Update()
    {
        if (Input.eventSystem.currentSelectedGameObject != currentSelected)
        {
            currentSelected.transform.GetChild(0).gameObject.SetActive(false);
            currentSelected = Input.eventSystem.currentSelectedGameObject;
            currentSelected.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (bookOpened && Input.MenuCancel)
        {
            StartCoroutine(CloseBook());
        }
    }

    IEnumerator OpenBook()
    {
        currentSelected.transform.GetChild(0).gameObject.SetActive(false);
        bookAnim.Play("BookOpen");
        yield return new WaitForSeconds(1); 
        bookOpened = true;
        Input.SelectUI(emptyButton);
    }

    IEnumerator CloseBook()
    {
        currentSelected.transform.GetChild(0).gameObject.SetActive(false);
        bookAnim.Play("BookClose");
        yield return new WaitForSeconds(1);
        bookOpened = false;
        Input.SelectUI(bookButton);
    }

    public void ActivateBook()
    {
        StartCoroutine(OpenBook());
    }

    public void BeginStory()
    {
        AudioManager.Inst.StopAudio(MainMenuMusic);

        GameManager.LoadScenes(scenesToLoad);
        GameManager.UnloadScenes(scenesToUnload);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
