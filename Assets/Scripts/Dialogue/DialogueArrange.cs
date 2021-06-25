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
    [TextArea(2, 5)] public string question;
    [TextArea(2, 5)] public string feedback;

    private GameObject arrangementPanel;
    private SentenceHolder sentenceHolder;
    private Button arrangementButton;
    private ArrangementOptionsHolder optionsHolder;

    protected override void Start()
    {
        base.Start();

        arrangementPanel = GameManager.Instance.Blackboard.ArrangementPanel;
        sentenceHolder = GameManager.Instance.Blackboard.SentenceHolder;
        arrangementButton = GameManager.Instance.Blackboard.ArrangementButton;
        optionsHolder = GameManager.Instance.Blackboard.OptionsHolder;
    }

    public override void StartDialogue()
    {
        sentenceHolder.questionText.text = question;
        sentenceHolder.feedbackText.text = feedback;
        runner.AddCommandHandler("arrange_sentence", ArrangeSentence);
        base.StartDialogue();
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
            Debug.Log(sent);

            foreach (string answer in answers)
            {
                Debug.Log(answer);
                if (sent == answer.ToLower())
                {
                    isCorrect = true;
                    break;
                }
            }

            if (isCorrect)
            {
                arrangementPanel.SetActive(false);
                arrangementButton.onClick.RemoveAllListeners();
                sentenceHolder.Reset();
                onComplete();
            }
            else
            {
                sentenceHolder.Reset();
                sentenceHolder.feedbackPanel.SetActive(true);
            }
        }
    }

    protected override void RemoveCommandHandlers()
    {
        runner.RemoveCommandHandler("arrange_sentence");
        base.RemoveCommandHandlers();
    }
}
