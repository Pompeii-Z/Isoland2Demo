using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
     private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyAction()
    {
        if(isDone)
            dialogueController.ShowDialogueFinsh();
        else //Empty 对话内容A
            dialogueController.ShowDialogueEmpty();
    }

    protected override void OnClickedAction()
    {
        //Finsh 对话内容B
        dialogueController.ShowDialogueFinsh();
    }
}
