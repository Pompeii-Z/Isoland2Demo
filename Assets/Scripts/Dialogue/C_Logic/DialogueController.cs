using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    public DialogueData_SO dialogueFinsh;

    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinshStack;

    private bool isTalking;

    private void Awake()
    {
        FillDialogueStack();
    }

    /// <summary>
    /// 使用栈 存储对话内容
    /// </summary>
    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinshStack = new Stack<string>();

        for (int i = dialogueEmpty.dialogueList.Count - 1; i > -1; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinsh.dialogueList.Count - 1; i > -1; i--)
        {
            dialogueFinshStack.Push(dialogueFinsh.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
    }
    public void ShowDialogueFinsh()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueFinshStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        if (data.TryPop(out string result))//TryPop (bool)尝试返回顶部元素把结果给到result,并且会移除该顶部元素。
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.Pause);
        }
        else//Stack data中为空，则重新存储至栈中
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }

}
