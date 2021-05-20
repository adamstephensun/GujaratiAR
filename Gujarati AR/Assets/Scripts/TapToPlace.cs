using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class TapToPlace : MonoBehaviour
{
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();  //Contains all the raycast hits

    private ARRaycastManager raycastMan;                    //AR Manager for raycast
    ARReferencePointManager referencePointMan;      //AR manager for reference points. ref points increase accuracy of ar 
    List<ARReferencePoint> referencePoints;
    ARPlaneManager planeMan;

    private void Awake()
    {
        raycastMan = GetComponent<ARRaycastManager>();
        referencePointMan = GetComponent<ARReferencePointManager>();
        planeMan = GetComponent<ARPlaneManager>();
        referencePoints = new List<ARReferencePoint>();
    }

    public void ClearReferencePoints()  //Clears all the reference points in referencePointMan
    {
        foreach (var referencePoint in referencePoints)
        {
            referencePointMan.RemoveReferencePoint(referencePoint);
        }
        referencePoints.Clear();
    }

    bool GetTouchPosition(out Vector2 touchPosition)    //Checks if there is any touches
    {
        if(Input.touchCount > 0)    //If there is touches
        {
            touchPosition = Input.GetTouch(0).position; //Set the touchPos to the touch location
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosition)) return;   //If there are no touches, leave loop

        if(!EventSystem.current.IsPointerOverGameObject(0))
        {
            if (raycastMan.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))   //If touch impacts a plane
            {
                var hitPose = hits[0].pose; //Raycasts are sorted by distance, so first one is the closest
                TrackableId planeId = hits[0].trackableId;  //Gets the ID of the impacted plane
                var referencePoint = referencePointMan.AttachReferencePoint(planeMan.GetPlane(planeId), hitPose);

                if (referencePoint != null)
                {
                    ClearReferencePoints();
                    referencePoints.Add(referencePoint);
                }
            }
        }
    }
}
