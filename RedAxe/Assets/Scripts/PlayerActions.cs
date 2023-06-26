using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    private Camera cam;
    private GameObject car;
    private GameObject movement;
    private RCC_CarControllerV3 rccCarControllerV3;
    
    public LayerMask targetLayerMask;
    public RCC_Camera rccCamera;
    public GameObject rccCameraObject;
    public GameObject interactionUI;
    public Image cursor;

    private void Start()
    {
        cam = Camera.main;
        movement = transform.GetChild(0).gameObject;
        StartCoroutine(Player());
    }

    IEnumerator Player()
    {
        yield return null;
        while (true)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = cam.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out var hit, 1, targetLayerMask))
            {
                if (hit.collider.CompareTag("Car"))   
                {
                    interactionUI.SetActive(true);
                    car = hit.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        rccCarControllerV3 = car.GetComponent<RCC_CarControllerV3>();
                        GetInTheCar();
                        interactionUI.SetActive(false);
                        yield break;
                    }
                }
                else if (hit.collider.CompareTag("NPC"))
                {
                    interactionUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<NPC>().StartInteraction();
                        interactionUI.SetActive(false);
                        yield break;
                    }
                }
                else
                {
                    interactionUI.SetActive(false);
                }
            }
            else
            {
                interactionUI.SetActive(false);
            }
            yield return null;
        }
    }

    IEnumerator Car()
    {
        yield return null;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetOutTheCar();
                yield break;
            }
            yield return null;
        }
    }

    private void GetInTheCar()
    {
        if (car)
        {
            cursor.enabled = false;
            
            rccCarControllerV3.enabled = true;
            rccCameraObject.SetActive(true);
            rccCamera.cameraTarget.playerVehicle = rccCarControllerV3;
            
            movement.SetActive(false);
            StartCoroutine(Car());
        }
    }
    private void GetOutTheCar()
    {
        if (car)
        {
            cursor.enabled = true;
            
            rccCameraObject.SetActive(false);
            rccCarControllerV3.enabled = false;
            
            movement.SetActive(true);
            movement.transform.position = car.transform.position + new Vector3(2, 0, 2);
            StartCoroutine(Player());
        }
    }
}
