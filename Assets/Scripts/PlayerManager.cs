using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerManager : CharacterManager
{
    InputManager inputManager;
    LedgeGrab ledgeGrab;

    public Vector3 ledgeClimbPositionOffset;
    public bool isSneaking;
    public bool canCombo;
    public bool isInDialogue;

    public Interactable nearestInteractable;

    public PlayerInventory playerInventory;
    public InteractPopupUI interactPopupUI;
    private DialogueRunner dialogueRunner;

    protected override void Awake()
    {
        inputManager = GetComponent<InputManager>();
        ledgeGrab = GetComponentInChildren<LedgeGrab>();
        playerInventory = GetComponent<PlayerInventory>();
        interactPopupUI = FindObjectOfType<InteractPopupUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        isSneaking = false;
        canCombo = false;
        base.Awake();
    }
    protected override void Update()
    {
        // check to see if player is in a dialogue
        if(dialogueRunner.CurrentNodeName != null) {
            body.bodyType = RigidbodyType2D.Static;
            isInteracting = true; 
        }
        else {
            body.bodyType = RigidbodyType2D.Dynamic;
            isInteracting = false; 
        }

        inputManager.TickInput(Time.deltaTime);


        // update animator
        //HandleAnimator(body.velocity.x);
        HandleAnimator(inputManager.movement.x);

        ledgeGrab.SetDirection(facingRight);

        CheckForInteractables();

        base.Update();
    }
    private void LateUpdate()
    {
        // reset all the input values
        if (!isUpsideDown) { inputManager.jumpInput = false; }
        inputManager.interactInput = false;
    }

    public void HandleAnimator(float movement)
    {
        if (isInteracting) {
            animator.SetBool("Running", false);
            return; 
        }

        //animator.SetBool("Wall Hang", isLedgeHanging);

        if (isLedgeHanging)
        {
            if (facingRight)
            {
                animator.SetFloat("X", 1f);
            }
            else
            {
                animator.SetFloat("X", -1f);
            }
        }
        else
        {
            if (isGrounded || isFalling || isUpsideDown) {
                if (isUpsideDown) { movement *= -1f; }
                if (Mathf.Abs(movement) > 0.01f) { animator.SetFloat("X", movement); }
            }
            
            animator.SetBool("Grounded", isGrounded);
            //animator.SetBool("Falling", isFalling);
            animator.SetBool("Running", (movement != 0f));
            animator.SetBool("Ghost", isUpsideDown);


            if (movement > .01f)
            {
                //animator.SetFloat("X", movement);
                if (!facingRight && isGrounded && !isSneaking)
                {
                    //animator.SetTrigger("Turn");
                }
            }
            else if (movement < -.01f)
            {
                //animator.SetFloat("X", movement);
                if (facingRight && isGrounded && !isSneaking)
                {
                    //animator.SetTrigger("Turn");
                }
            }
        }

    }
    public void PlayJumpAnimation()
    {
        animator.SetBool("Grounded", false);
        animator.SetBool("Jumping", true);
    }

    public void SetCanClimb()
    {
        canClimb = true;
        if (!isLedgeHanging) { isLedgeHanging = true; }
    }

    public void SetCanCombo()
    {
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo = false;
        //animator.SetBool("Attack Combo", false);
    }

    public void HandleSneak()
    {
        isSneaking = !isSneaking;
        //animator.SetBool("Sneaking", isSneaking);
        //animator.SetTrigger("Sneak");
    }
    public void HandleLedgeClimb()
    {
        if (!canClimb) { return; }
        isLedgeHanging = false;
        canClimb = false;
        body.bodyType = RigidbodyType2D.Static;
        this.transform.position = (this.transform.position + ledgeGrab.transform.localPosition);
        //animator.SetBool("Climb Up", true);
        //animator.SetBool("Wall Hang", false);
        ledgeGrab.Drop();
    }

    public void SetPostionLedgeClimb()
    {
        Vector3 posOffset = ledgeClimbPositionOffset;

        if (!facingRight)
        {
            posOffset.x *= -1f;
        }

        this.transform.position = this.transform.position + posOffset;
    }
    public void HandleLedgeDrop()
    {
        isLedgeHanging = false;
        //animator.SetBool("Climb Up", false);
        //animator.SetBool("Wall Hang", false);
        ledgeGrab.Drop();
    }
    public void HandleInteract()
    {
        if(nearestInteractable == null || Vector2.Distance(this.transform.position, nearestInteractable.transform.position) > nearestInteractable.interactRange) { return; }

        // trigger the interaction
        nearestInteractable.Interact();
    }

    public void CheckForInteractables()
    {
        foreach(Interactable interactable in FindObjectsOfType<Interactable>())
        {
            if(nearestInteractable == null || Vector2.Distance(this.transform.position, interactable.transform.position) <= Vector2.Distance(this.transform.position, nearestInteractable.transform.position))
            {
                nearestInteractable = interactable;
            }
        }

        if (nearestInteractable == null || Vector2.Distance(this.transform.position, nearestInteractable.transform.position) > nearestInteractable.interactRange) { return; }

        // display interact message
        interactPopupUI.SetCurrentInteraction(nearestInteractable);
        //Debug.Log(nearestInteractable.interactMessage);
    }

    public void ToggleInDialogue(bool state)
    {
        isInDialogue = state;
    }
}
