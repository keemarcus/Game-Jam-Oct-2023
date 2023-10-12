using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueInteractable : Interactable
{
    public string dialogueStartNode;
    private DialogueRunner dialogueRunner;
    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    public override void Interact()
    {
        if (dialogueRunner.IsDialogueRunning) { return; }

        // start the dialogue associated with the sign
        dialogueRunner.StartDialogue(dialogueStartNode);
    }
}
