using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Components")]
    public Rigidbody2D body;
    //protected Animator animator;

    [Header("Character State Bools")]
    public bool isDead;
    public bool isInteracting;
    public bool isGrounded;
    public bool isFalling;
    public bool canClimb;
    public bool facingRight;
    public bool isLedgeHanging;

    [Header("Grounded Detection")]
    public float groundDetectionDistance;
    public LayerMask groundLayer;
    public Transform groundDetectionCastTransform;
    private float groundedTime;
    private float groundedTimer;

    [Header("Character Stats")]
    public float maxHP;
    public float currentHP;

    protected virtual void Awake()
    {
        isDead = false;
        isInteracting = false;
        facingRight = false;
        isLedgeHanging = false;
        canClimb = false;
        //animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        isFalling = HandleFallingDetection();
        isGrounded = HandleGroundedDetection(Time.deltaTime);
        facingRight = HandleDirection();

        // check to see if the character fell off the map
        if (isFalling && this.transform.position.y <= -2f)
        {
            isDead = true;
        }

        // update animator variables
        //animator.SetBool("Is Dead", isDead);
    }
    public void SetHP(float health)
    {
        currentHP = health;
    }
    public void DamageCharacter(float incomingDamage)
    {
        currentHP = Mathf.Clamp(currentHP - incomingDamage, 0f, maxHP);
    }
    public bool HandleDirection()
    {
        if (body.velocity.x > .01f)
        {
            return true;
        }
        else if (body.velocity.x < -.01f)
        {
            return false;
        }
        else
        {
            return facingRight;
        }
    }
    public virtual void DestroyAnimationEvent()
    {
        Destroy(this.gameObject);
    }
    #region Grounded And Falling Detection
    private bool HandleGroundedDetection(float delta)
    {
        // check to see if character is on the ground
        if (!Physics2D.BoxCast(groundDetectionCastTransform.position + (Vector3.down * groundDetectionDistance), new Vector3(this.GetComponent<BoxCollider2D>().size.x - .01f, 0.01f, 0f), 0f, Vector2.down, 0.01f, groundLayer)) { return false; }
        else
        {
            // make sure character has the right rigidbody type
            if (!isInteracting && !isLedgeHanging && body.bodyType != RigidbodyType2D.Dynamic) { body.bodyType = RigidbodyType2D.Dynamic; }

            // check to see if we've been on the ground long enough
            if (groundedTimer <= 0f) { return true; }
            else
            {
                // tick the grounded timer
                groundedTimer -= delta;
                return false;
            }
        }
    }

    private bool HandleFallingDetection()
    {
        if (isGrounded || Mathf.Abs(body.velocity.y) < .01f) { return false; }
        else
        {
            groundedTimer = groundedTime;
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        // draw ground detection gizmo
        Gizmos.DrawWireCube(groundDetectionCastTransform.position + (Vector3.down * groundDetectionDistance), new Vector3(this.GetComponent<BoxCollider2D>().size.x - .01f, 0.01f, 0f));
    }
    #endregion
}
