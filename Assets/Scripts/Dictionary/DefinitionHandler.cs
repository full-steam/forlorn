using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefinitionHandler : MonoBehaviour
{
    private TMP_Text engText;
    private TMP_Text indText;
    private TMP_Text posText;
    private string pronunciationID;
    private Button button;
    private ButtonHandler buttonHandler;

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
