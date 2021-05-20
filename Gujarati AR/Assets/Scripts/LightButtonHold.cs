using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightButtonHold : MonoBehaviour    //Script for the touch hold of the light button
{
    private RectTransform buttonArea;       //Rect of the button area
    public float holdDuration = 1.0f;       //The amount of time needed to hold
    public GameObject lightMenu;            //The colour selection menu

    [SerializeField]
    private LightSpawner lightSpawner;      //Controller that spawns the lights

    private float remainingDuration;        //The amount of time left that the user must hold down to activate the menu

    

    [SerializeField]
    private InGameMenuManager menuManager;  //Controls the menus

    private void Awake()
    {
        buttonArea = GetComponent<RectTransform>();
        remainingDuration = holdDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)        //When more than 0 touches are registered
        {
            Touch touchInfo = Input.GetTouch(0);       //Work with only the first of the touches
            if (touchInfo.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(buttonArea, touchInfo.position))
            {   //If the touch is stationary, and the touch is within the button rect
                remainingDuration -= Time.deltaTime;    //Start counting the remaining duration down

                if (remainingDuration <= 0) menuManager.OpenLightMenu(true);  //If remaining duration reaches 0, open menu
            }

            if(touchInfo.phase == TouchPhase.Ended && RectTransformUtility.RectangleContainsScreenPoint(buttonArea, touchInfo.position))
            {   //If the touch has ended before the countdown, and touch is within the button rect
                ResetHoldTimer();                                        //Reset hold timer
                if (remainingDuration > 0) lightSpawner.SpawnLight();    //If touch ended before timer, consider it a press and spawn a light
            }
        }
    }

    public void ResetHoldTimer()        //Resets the remaining time
    {
        remainingDuration = holdDuration;
    }
}
