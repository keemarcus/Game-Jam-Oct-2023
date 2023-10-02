using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactRange;
    public string interactMessage;
    public abstract void Interact();

}
