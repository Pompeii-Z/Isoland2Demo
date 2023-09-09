using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;

    public float typingSpeed = 0.1f;

    private Tween currentTween;

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
    }

    private void ShowDialogue(string dialogue)
    {
        dialogueText.text = string.Empty;

        if (string.IsNullOrEmpty(dialogue))
        {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);

        if (currentTween != null && currentTween.IsActive())
            return;

        currentTween = dialogueText.DOText(dialogue, typingSpeed * dialogue.Length)
                     .SetEase(Ease.Linear);
    }
}
