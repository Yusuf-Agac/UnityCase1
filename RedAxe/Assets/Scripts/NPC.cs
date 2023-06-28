using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public CarAttributes carAttributes;

    private FirstPersonMovement playerMovement;
    private PlayerActions playerActions;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        playerMovement = GameObject.FindWithTag("Movement").GetComponent<FirstPersonMovement>();
        playerActions = playerMovement.transform.parent.GetComponent<PlayerActions>();
        canvasRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        CanvasTagLookPlayer();
    }
    
    public void StartInteraction()
    {
        playerActions.DisablePlayer(true, carAttributes, gameObject);
    }

    private void CanvasTagLookPlayer()
    {
        canvasRectTransform.LookAt(playerMovement.transform);
        canvasRectTransform.Rotate(0, 180, 0);
    }
}
