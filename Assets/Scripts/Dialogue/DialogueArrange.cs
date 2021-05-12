using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to handle the sentence arrangement mechanic.
/// </summary>
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
        base.Start();
        
        runner.AddCommandHandler("arrange_sentence", ArrangeSentence);

        foreach (string answer in answers) answer.ToLower();
    }

    /// <summary>
    /// Begins the sentence arrangement mechanic.
    /// </summary>
    /// <param name="parameters">Unused. Parameters sent from Yarn Programs.</param>
    /// <param name="onComplete">Action to be called once the arrangement is complete.</param>
    private void ArrangeSentence(string[] parameters, Action onComplete)
    {
        optionsHolder.SetOptions(options);
        arrangementPanel.SetActive(true);
        arrangementButton.onClick.AddListener(delegate { ProcessAnswer(onComplete); });
    }

    /// <summary>
    /// Processes the answer sentence.
    /// </summary>
    /// <param name="onComplete">Action to be called if the arrangement is correct.</param>
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
            // TODO: implement feedback if incorrect
            else sentenceHolder.Reset();
        }
    }
}
