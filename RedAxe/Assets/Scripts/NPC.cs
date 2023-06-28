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

    private void Awake()
    {
        playerMovement = GameObject.FindWithTag("Movement").GetComponent<FirstPersonMovement>();
        playerActions = playerMovement.transform.parent.GetComponent<PlayerActions>();
    }

    public void StartInteraction()
    {
        playerActions.DisablePlayer(true, carAttributes, gameObject);
    }
}
