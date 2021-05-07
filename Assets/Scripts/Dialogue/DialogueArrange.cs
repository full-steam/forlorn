using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueArrange : Dialogue
{
    public List<string> answers;
    public List<string> options;
    public GameObject arrangementPanel;
    public SentenceHolder sentenceHolder;
    public Button arrangementButton;
    public ArrangementOptionsHolder optionsHolder;

    protected override void Start()
    {
        runner.AddCommandHandler("arrange_sentence", ArrangeSentence);

        foreach (string answer in answers) answer.ToLower();
    }

    public override void StartDialogue()
    {
        optionsHolder.SetOptions(options);
        base.StartDialogue();
    }

    private void ArrangeSentence(string[] parameters, Action onComplete)
    {
        arrangementPanel.SetActive(true);
        arrangementButton.onClick.AddListener(delegate { ProcessAnswer(onComplete); });
    }

    private void ProcessAnswer(Action onComplete)
    {
        string sent = sentenceHolder.GetSentence();

        if (!string.IsNullOrWhiteSpace(sent))
        {
            bool isCorrect = false;

            // lowercase
            sent = sent.ToLower();

            foreach (string answer in answers)
            {
                if (sent == answer)
                {
                    isCorrect = true;
                    break;
                }
            }

            if (isCorrect)
            {
                arrangementPanel.SetActive(false);
                arrangementButton.onClick.RemoveAllListeners();
                onComplete();
            }
            else sentenceHolder.Reset();
        }
    }
}
