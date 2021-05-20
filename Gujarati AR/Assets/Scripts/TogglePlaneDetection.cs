using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class TogglePlaneDetection : MonoBehaviour     //Class to toggle the plane detection in the scene
{
    private ARPlaneManager planeManager;
    [SerializeField]
    private Text toggleButtonText;

    private bool isBlue = false;
    private Color defaultPlaneCol;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        defaultPlaneCol = planeManager.planePrefab.GetComponent<MeshRenderer>().material.color;
        isBlue = false;
    }

    public void Toggle()
    {
        planeManager.enabled = !planeManager.enabled;
        
        string toggleButtonMessage = "";

        if(planeManager.enabled)
        {
            toggleButtonMessage = "Disable Planes";
            SetAllPlanesActive(true);
        }
        else
        {
            toggleButtonMessage = "Enable Planes";
            SetAllPlanesActive(false);
        }
        toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    public void ChangePlaneColour()
    {
        if (!isBlue) planeManager.planePrefab.GetComponent<MeshRenderer>().material.color = Color.blue;
        else planeManager.planePrefab.GetComponent<MeshRenderer>().material.color = defaultPlaneCol;
        isBlue = !isBlue;
    }
}
