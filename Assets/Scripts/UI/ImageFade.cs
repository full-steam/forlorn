using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum FadeAction
{
    FadeIn,
    FadeOut,
    FadeInAndOut,
    FadeOutAndIn
}

public enum FadeStart
{
    Start,
    Enable,
    Manual
}

public class ImageFade : MonoBehaviour
{
    [Tooltip("The Fade Type.")]
    [SerializeField] private FadeAction fadeType;

    [Tooltip("the image you want to fade, assign in inspector")]
    [SerializeField] private Image img;

    [SerializeField] private FadeStart fadeStart;
    [SerializeField] private float fadeTime = 1.0f;

    public void Start()
    {
        if (fadeStart == FadeStart.Start) StartFade();
    }

    public void OnEnable()
    {
        if (fadeStart == FadeStart.Enable) StartFade();
    }

    public void StartFade()
    {
        if (fadeType == FadeAction.FadeIn)
        {

            StartCoroutine(FadeIn());

        }

        else if (fadeType == FadeAction.FadeOut)
        {

            StartCoroutine(FadeOut());

        }

        else if (fadeType == FadeAction.FadeInAndOut)
        {

            StartCoroutine(FadeInAndOut());

        }

        else if (fadeType == FadeAction.FadeOutAndIn)
        {

            StartCoroutine(FadeOutAndIn());

        }
    }

    // fade from transparent to opaque
    IEnumerator FadeIn()
    {

        // loop over 1 second
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

    }

    // fade from opaque to transparent
    IEnumerator FadeOut()
    {
        // loop over 1 second backwards
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }
    }

    IEnumerator FadeInAndOut()
    {
        // loop over 1 second
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        //Temp to Fade Out
        yield return new WaitForSeconds(1);

        // loop over 1 second backwards
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }
    }

    IEnumerator FadeOutAndIn()
    {
        // loop over 1 second backwards
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        //Temp to Fade In
        yield return new WaitForSeconds(1);

        // loop over 1 second
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }
    }
}