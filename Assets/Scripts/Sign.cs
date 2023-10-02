using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    public string signText;
    public override void Interact()
    {
        Debug.Log("Reading sign : " + signText);
    }
}
