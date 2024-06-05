using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    private Image fadeImage;
    [SerializeField] private float fadeTime;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void ScreenFadeOut()
    {
        StartCoroutine(fading());
    }

    IEnumerator fading()
    {
        fadeImage.CrossFadeAlpha(1f, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
        fadeImage.CrossFadeAlpha(0f, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
    }
}
