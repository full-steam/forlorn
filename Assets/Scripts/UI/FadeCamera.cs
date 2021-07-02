using UnityEngine;

public class FadeCamera : MonoBehaviour
{
    [Tooltip("How fast should the texture be faded out?")]
    public float fadeTime;

    [Tooltip("How long will the screen stay black?")]
    public float blackScreenDuration;

    [Tooltip("Choose the color, which will fill the screen.")]
    public Color fadeColor;

    private float _alpha = 1.0f;
    private Texture2D _texture;

    private float _passedBlackScreenTime;

    private void Start()
    {
        _texture = new Texture2D(1, 1);
        _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
        _texture.Apply();
    }


    public void OnGUI()
    {
        // If the texture is no more visible, we are done.
        if (_alpha <= 0.0f)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

        if (_passedBlackScreenTime < blackScreenDuration)
        {
            _passedBlackScreenTime += Time.deltaTime;
            return;
        }

        calculateTexture();
    }

    private void calculateTexture()
    {
        _alpha -= Time.deltaTime / fadeTime;
        _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
        _texture.Apply();
    }
}