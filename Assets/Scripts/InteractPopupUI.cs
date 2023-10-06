using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractPopupUI : MonoBehaviour
{
    public string interactionTextBeginning;
    public float hoverDistance;
    private PlayerManager player;
    private Interactable currentInteraction;
    private TMP_Text popupText;
    private void Awake()
    {
        currentInteraction = null;
        player = FindObjectOfType<PlayerManager>();
        popupText = GetComponent<TMP_Text>();
        SetInactive();
    }
    private void Update()
    {
        if (currentInteraction == null) { return; }

        if (Vector2.Distance(player.transform.position, currentInteraction.transform.position) >  currentInteraction.interactRange)
        {
            currentInteraction = null;
            SetInactive();
        }
    }
    public void SetCurrentInteraction(Interactable interactable)
    {
        currentInteraction = interactable;
        popupText.text = interactionTextBeginning + currentInteraction.interactMessage;
        this.transform.position = Camera.main.WorldToScreenPoint((Vector2) currentInteraction.transform.position + (Vector2.up * currentInteraction.transform.localScale.y * hoverDistance * player.gameObject.GetComponent<Rigidbody2D>().gravityScale));
        SetActive();
    }
    public void SetActive()
    {
        if(popupText.enabled) { return; }
        popupText.enabled = true;
    }

    public void SetInactive()
    {
        if (!popupText.enabled) { return; }
        popupText.enabled = false;
    }
}
