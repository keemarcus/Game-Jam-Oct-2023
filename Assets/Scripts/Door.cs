using System.Collections;
using System.Collections.Generic;
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
            }
            else
            {
                dialogueRunner.StartDialogue("LockedDoor");
            }
        }
        else
        {
            doorCollider.enabled = !doorCollider.enabled;
        }
    }
}
