using TMPro;
using UnityEngine;

public class DictionaryItem : MonoBehaviour
{
    private Word word;
    private TMP_Text text;
    private DefinitionHandler definitionPanel;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void Init(Word word, DefinitionHandler definitionPanel)
    {
        this.word = word;
        this.definitionPanel = definitionPanel;
        text.text = word.eng;
    }

    public void Define()
    {
        definitionPanel.SetWord(word);
    }
}