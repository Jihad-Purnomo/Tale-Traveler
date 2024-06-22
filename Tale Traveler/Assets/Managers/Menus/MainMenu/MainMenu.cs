using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject prologueButton;

    [SerializeField] private GameObject controlImage;
    [SerializeField] private GameObject creditImage;

    [SerializeField] private GameObject titleScreen;
    [SerializeField] private float fadeOutTime;

    private Image[] titleScreenImages;
    private TextMeshProUGUI[] titleScreenText;

    [SerializeField] private Image titleScreenConfirmButton;
    [SerializeField] private Image titleScreenCancelButton;

    [SerializeField] private Sprite gamepadConfirm;
    [SerializeField] private Sprite gamepadCancel;

    [SerializeField] private Sprite keyboardConfirm;
    [SerializeField] private Sprite keyboardCancel;

    private Animator bookAnim;

    private GameObject currentSelected;
    private bool bookOpened = false;
    private bool creditOpened = false;
    private bool controlOpened = false;
    private bool inTitleScreen = true;

    private void Awake()
    {
        bookAnim = bookButton.GetComponent<Animator>();
    }

    private void Start()
    {
        Input.ChangeActionMap("UI");

        titleScreenImages = titleScreen.GetComponentsInChildren<Image>();
        titleScreenText = titleScreen.GetComponentsInChildren<TextMeshProUGUI>();

        Input.SelectUI(emptyButton);
        currentSelected = emptyButton;
        currentSelected.transform.GetChild(0).gameObject.SetActive(true);

        AudioManager.Inst.PlayAudio(MainMenuMusic, transform);
    }

    private void Update()
    {
        if (Input.PlayerInput.currentControlScheme == "Gamepad")
        {
            titleScreenConfirmButton.sprite = gamepadConfirm;
            titleScreenCancelButton.sprite = gamepadCancel;
        }
        else
        {
            titleScreenConfirmButton.sprite = keyboardConfirm;
            titleScreenCancelButton.sprite = keyboardCancel;
        }

        if (inTitleScreen)
        {
            if (Input.MenuSubmit)
            {
                StartCoroutine(RemoveTitleScreen());
            }

            if (Input.MenuCancel)
            {
                QuitApp();
            }
        }
        
        if (!inTitleScreen && !bookOpened && !creditOpened && !controlOpened && Input.MenuCancel)
        {
            StartCoroutine(AppearTitleScreen());
        }

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

        if (controlOpened && Input.MenuCancel)
        {
            CloseControl();
        }

        if (creditOpened && Input.MenuCancel)
        {
            CloseCredit();
        }
    }

    IEnumerator RemoveTitleScreen()
    {
        for (int i = 0; i < titleScreenImages.Length; i++)
        {
            titleScreenImages[i].CrossFadeAlpha(0f, fadeOutTime, true);
        }
        for (int i = 0; i < titleScreenText.Length; i++)
        {
            titleScreenText[i].CrossFadeAlpha(0f, fadeOutTime, true);
        }

        yield return new WaitForSeconds(fadeOutTime);
        inTitleScreen = false;
        Input.SelectUI(bookButton);
    }

    IEnumerator AppearTitleScreen()
    {
        for (int i = 0; i < titleScreenImages.Length; i++)
        {
            titleScreenImages[i].CrossFadeAlpha(1f, fadeOutTime, true);
        }
        for (int i = 0; i < titleScreenText.Length; i++)
        {
            titleScreenText[i].CrossFadeAlpha(1f, fadeOutTime, true);
        }
        yield return new WaitForSeconds(fadeOutTime);
        inTitleScreen = true;
        Input.SelectUI(emptyButton);
    }

    IEnumerator OpenBook()
    {
        currentSelected.transform.GetChild(0).gameObject.SetActive(false);
        bookAnim.Play("BookOpen");
        yield return new WaitForSeconds(1); 
        bookOpened = true;
        Input.SelectUI(prologueButton);
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

    public void OpenControl()
    {
        controlOpened = true;
        Input.DisableAction(Input.navigateAction);
        controlImage.SetActive(true);
    }

    public void CloseControl()
    {
        controlOpened = false;
        Input.EnableAction(Input.navigateAction);
        controlImage.SetActive(false);
    }

    public void OpenCredit()
    {
        creditOpened = true;
        Input.DisableAction(Input.navigateAction);
        creditImage.SetActive(true);
    }

    public void CloseCredit()
    {
        creditOpened = false;
        Input.EnableAction(Input.navigateAction);
        creditImage.SetActive(false);
    }

    public void BeginStory()
    {
        AudioManager.Inst.StopAudio(MainMenuMusic);

        Input.ChangeActionMap("Gameplay");

        GameManager.LoadScenes(scenesToLoad);
        GameManager.UnloadScenes(scenesToUnload);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
