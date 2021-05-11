using TMPro;
using UnityEngine;

/// <summary>
/// Component handling the logic and data of options in the sentence arrangement mechanic.
/// </summary>
public class ArrangementOption : MonoBehaviour
{
    public string Word { get; private set; }

    private ArrangementOptionsHolder optionsHolder;
    private SentenceHolder sentenceHolder;
    private TMP_Text text;
    private bool isInSent;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Pseudo-constructor.
    /// </summary>
    /// <param name="option">The option to display.</param>
    /// <param name="holder">Reference to the arrangement options holder.</param>
    public void Init(string option, ArrangementOptionsHolder holder)
    {
        Word = option;
        optionsHolder = holder;
        sentenceHolder = holder.sentenceHolder;
        text.text = Word;
        isInSent = false;
    }

    /// <summary>
    /// If the option is not in the sentence, it is put into the sentence.
    /// If the option is in the sentence, it is returned to the options holder.
    /// </summary>
    public void Toggle()
    {
        if (isInSent)
        {
            sentenceHolder.RemoveWord(this);
            optionsHolder.AddOption(Word);
            isInSent = false;
        }
        else
        {
            sentenceHolder.AddWord(this);
            isInSent = true;
        }
    }
}