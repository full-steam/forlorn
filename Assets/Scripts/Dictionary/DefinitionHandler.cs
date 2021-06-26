using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonHandler))]
public class DefinitionHandler : MonoBehaviour
{
    public TMP_Text engText;
    public TMP_Text indText;
    public TMP_Text posText;
    public Button button;
    private ButtonHandler buttonHandler;
    private string pronunciationID;

    private void Awake()
    {
        buttonHandler = GetComponent<ButtonHandler>();

        button.onClick.AddListener(PlayPronunciation);
    }

    /// <summary>
    /// Sets the currently displayed dictionary entry.
    /// </summary>
    /// <param name="word">Dictionary entry to be displayed.</param>
    public void SetWord(Word word)
    {
        engText.text = word.eng;
        indText.text = word.ind;
        posText.text = "Kata " + word.pos.ToString();
        pronunciationID = word.pronunciationAudioClipID;
    }

    /// <summary>
    /// Plays the currently defined pronunciation audio.
    /// </summary>
    private void PlayPronunciation()
    {
        buttonHandler.PlaySound(pronunciationID);
    }
}
