using TMPro;
using UnityEngine;

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

    public void Init(string option, ArrangementOptionsHolder holder)
    {
        Word = option;
        optionsHolder = holder;
        sentenceHolder = holder.sentenceHolder;
        text.text = Word;
        isInSent = false;
    }

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