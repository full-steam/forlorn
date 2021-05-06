using TMPro;
using UnityEngine;

public class DictionaryItem : MonoBehaviour
{
    public Word Word { get; private set; }
    private TMP_Text text;
    private DefinitionHandler definitionPanel;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void Init(Word word, DefinitionHandler definitionPanel)
    {
        Word = word;
        this.definitionPanel = definitionPanel;
        text.text = word.eng;
    }

    public void Define()
    {
        definitionPanel.SetWord(Word);
    }
}