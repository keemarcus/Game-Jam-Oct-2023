using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rift : Interactable
{
    public Rift riftPair;
    public override void Interact()
    {
        Debug.Log("Rifting player");

        // find the player
        PlayerManager player = FindObjectOfType<PlayerManager>();

        // calculate the players new position
        Vector2 newPosition = (Vector2) player.transform.position + (Vector2) (riftPair.transform.position - this.transform.position) + ((this.transform.position - player.transform.position) * new Vector2(1f, 2.1f));

        // move them there
        //player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.transform.position = newPosition;

        // flip the gravity scale and y scale for the player
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale = player.gameObject.GetComponent<Rigidbody2D>().gravityScale * -1;
        player.transform.localScale *= new Vector2(1f, -1f);
        player.isUpsideDown = !player.isUpsideDown;
        if (player.isUpsideDown)
        {
            FindObjectOfType<Camera>().gameObject.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        }
        else
        {
            FindObjectOfType<Camera>().gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        //player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
