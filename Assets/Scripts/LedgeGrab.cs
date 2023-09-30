using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    [Header("Ledge Grab Stats")]
    public float grabRadius;
    public LayerMask groundLayer;

    private PlayerManager player;
    public bool canDetect;
    public float dropTimer;
    private GameObject currentLedge;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
        canDetect = true;
    }
    void Update()
    {
        if (!player.isSneaking && canDetect && dropTimer <= 0f)
        {
            player.isLedgeHanging = Physics2D.OverlapCircle(this.transform.position, grabRadius, groundLayer);
        }
        else
        {
            player.isLedgeHanging = false;
            if (dropTimer > 0f)
            {
                dropTimer -= Time.deltaTime;
            }
        }

        if (player.isLedgeHanging && player.isGrounded)
        {
            player.HandleLedgeClimb();
        }
    }

    public void Drop()
    {
        player.canClimb = false;
        dropTimer = .1f;
    }

    public void SetDirection(bool facingRight)
    {
        if (facingRight)
        {
            if (this.transform.localPosition.x < 0f)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x * -1f, this.transform.localPosition.y, 0f);
            }
        }
        else
        {
            if (this.transform.localPosition.x > 0f)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x * -1f, this.transform.localPosition.y, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetect = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetect = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, grabRadius);
    }
}
