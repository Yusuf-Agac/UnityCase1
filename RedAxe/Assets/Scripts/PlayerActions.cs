using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Camera cam;
    private GameObject car;
    private GameObject movement;
    
    public LayerMask targetLayerMask;
    
    public RCC_Camera rccCamera;
    public GameObject rccCameraObject;
    private RCC_CarControllerV3 rccCarControllerV3;

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
                    car = hit.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        rccCarControllerV3 = car.GetComponent<RCC_CarControllerV3>();
                        GetInTheCar();
                        yield break;
                    }
                }
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
            rccCameraObject.SetActive(false);
            rccCarControllerV3.enabled = false;
            
            movement.SetActive(true);
            movement.transform.position = car.transform.position + new Vector3(2, 0, 2);
            StartCoroutine(Player());
        }
    }
}
