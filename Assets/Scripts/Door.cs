using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Yarn.Unity;

public class Door : Interactable
{
    public bool isLocked;
    public string keyName;
    private BoxCollider2D doorCollider;
    private DialogueRunner dialogueRunner;
    private PlayerManager playerManager;
    private void Awake()
    {
        doorCollider = gameObject.GetComponent<BoxCollider2D>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public override void Interact()
    {
        if (isLocked)
        {
            if (playerManager.playerInventory.inventory.Contains(keyName))
            {
                doorCollider.enabled = !doorCollider.enabled;
                if (interactMessage == "Open Door") { interactMessage = "Close Door"; }
                else if (interactMessage == "Close Door") { interactMessage = "Open Door"; }
            }
            else
            {
                dialogueRunner.StartDialogue("LockedDoor");
            }
        }
        else
        {
            doorCollider.enabled = !doorCollider.enabled;
            if(interactMessage == "Open Door") { interactMessage = "Close Door"; }
            else if (interactMessage == "Close Door") { interactMessage = "Open Door"; }
        }
    }
}
