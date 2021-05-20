using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    private Transform cam;      //Stores the position of the camera in world space

    [SerializeField]
    private GameObject lightPrefab;     //The light object 
    private List<GameObject> lightList = new List<GameObject>();    //List of all the lights in the scene

    [SerializeField]
    private ColourSliderController lightColourController;       //Reference to the colour slider menu controller

    void Start()
    {
        cam = GameObject.Find("AR Camera").GetComponent<Transform>();   //Gets the transform of the AR cam
    }

    public void SpawnLight()
    {
        Color col = lightColourController.GetColour();              //Get the colour and intensity set in the colour picker menu
        float intensity = lightColourController.GetIntensity();     

        GameObject light =  Instantiate(lightPrefab, cam.position, Quaternion.identity);   //Instantiate a light on the cameras position. Quat identity gives the light no rotation
        light.GetComponent<LightConfig>().UpdateColAndIntensity(col, intensity);    //Update the colour and intensity

        lightList.Add(light);   //Add the light to the list of lights
    }

    public void ClearLights()       //Destroys all lights in the list and clears it
    {
        for(int i = 0; i< lightList.Count;i++) Destroy(lightList[i]);
        lightList.Clear();
    }
}
