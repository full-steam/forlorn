using System;
using System.Collections;
using UnityEngine;

public class CameraFadeManager : MonoBehaviour
{
    static private CameraFadeManager instance;
    static public CameraFadeManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CameraFadeManager>();

            return instance;
        }
    }

    [SerializeField]
    [Tooltip("How fast should the texture be faded out?")]
    private float fadeTime = 1.0f;

    [SerializeField]
    [Tooltip("Choose the color, which will fill the screen.")]
    private Color fadeColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

    [SerializeField]
    [Tooltip("How long will the screen stay black during FadeIn?")]
    private float blackScreenDuration = 1.0f;

    private float alpha = 1.0f;
    private Texture2D texture;

    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private float currentTime = 0;

    [Serializable]
    public class Options
    {
        public float fadeTime;
        public float blackScreenDuration;
        public Color fadeColor;
    }

    private void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    private void startFading(bool isFadingIn, bool isFadingOut, Options options = null)
    {
        currentTime = 0;

        if (options != null)
        {
            if (options.fadeTime <= 0f) fadeTime = options.fadeTime;

            if (options.blackScreenDuration <= 0f) blackScreenDuration = options.blackScreenDuration;

            if (options.fadeColor != null) fadeColor = options.fadeColor;
        }

        this.isFadingIn = isFadingIn;
        this.isFadingOut = isFadingOut;
    }

    public void FadeIn(Options options = null)
    {
        Debug.Log("Fade in called.");
        alpha = 1.0f;
        startFading(true, false, options);
    }

    public void FadeOut(Options options = null)
    {
        alpha = 0.0f;
        startFading(false, true, options);
    }

    public void FadeOutAndIn(Options fadeOutOptions = null, Options fadeInOptions = null, float delay = 0f)
    {
        StartCoroutine(CFadeOutAndIn(fadeOutOptions, fadeInOptions, delay));
    }

    public void FadeOutAndIn(Action onComplete)
    {
        StartCoroutine(CFadeOutAndIn(onComplete));
    }

    public void FadeInAndOut(Options fadeOutOptions = null, Options fadeInOptions = null, float delay = 0f)
    {
        StartCoroutine(CFadeInAndOut(fadeOutOptions, fadeInOptions, delay));
    }

    public void OnGUI()
    {
        if (isFadingIn || isFadingOut)
        {
            showBlackScreen();
        }
    }

    private void showBlackScreen()
    {
        if (isFadingIn && alpha <= 0.0f)
        {
            isFadingIn = false;

            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

        if (isFadingIn && blackScreenDuration > 0)
        {
            blackScreenDuration -= Time.deltaTime;

            return;
        }

        if (isFadingOut && alpha >= 1.0f) return;

        calculateTexture();
    }

    private void calculateTexture()
    {
        currentTime += Time.deltaTime;

        if (isFadingIn) alpha = 1.0f - currentTime / fadeTime;
        else alpha = currentTime / fadeTime;

        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    private IEnumerator CFadeOutAndIn(Options fadeOutOptions = null, Options fadeInOptions = null, float delay = 0f)
    {
        FadeOut(fadeOutOptions);

        float timer = 0f;

        while (timer < fadeTime + blackScreenDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        FadeIn(fadeInOptions);

        yield return 0;
    }

    private IEnumerator CFadeOutAndIn(Action onComplete)
    {
        FadeOut();

        float timer = 0f;

        while (timer < fadeTime + blackScreenDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < 0f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        FadeIn();

        onComplete();

        yield return 0;
    }

    private IEnumerator CFadeInAndOut(Options fadeOutOptions = null, Options fadeInOptions = null, float delay = 0f)
    {
        FadeIn(fadeInOptions);

        float timer = 0f;

        while (timer < fadeTime + blackScreenDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        FadeOut(fadeOutOptions);

        yield return 0;
    }
}