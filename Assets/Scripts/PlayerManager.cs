using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputManager inputManager;
    LedgeGrab ledgeGrab;

    public Vector3 ledgeClimbPositionOffset;
    public bool isSneaking;
    public bool canCombo;

    //public PlayerInventory playerInventory;

    protected override void Awake()
    {
        inputManager = GetComponent<InputManager>();
        ledgeGrab = GetComponentInChildren<LedgeGrab>();
        isSneaking = false;
        canCombo = false;
        base.Awake();
    }
    protected override void Update()
    {
        inputManager.TickInput(Time.deltaTime);

        // update animator
        HandleAnimator(body.velocity.x);

        ledgeGrab.SetDirection(facingRight);

        base.Update();
    }
    private void LateUpdate()
    {
        // reset all the input values
        inputManager.jumpInput = false;
    }

    public void HandleAnimator(float movement)
    {
        if (isInteracting) { return; }

        //animator.SetBool("Wall Hang", isLedgeHanging);

        if (isLedgeHanging)
        {
            if (facingRight)
            {
                //animator.SetFloat("X", 1f);
            }
            else
            {
                //animator.SetFloat("X", -1f);
            }
        }
        else
        {
            //animator.SetBool("Grounded", isGrounded);
            //animator.SetBool("Falling", isFalling);
            //animator.SetBool("Running", (movement != 0f));


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
        //animator.SetTrigger("Jump");
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
}
