using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    private GameObject spawnedObject;
    private List<GameObject> placedPrefabList = new List<GameObject>();     //List of objects in the scene

    private GameObject spawnedPlatform;
    private List<GameObject> placedPlatformList = new List<GameObject>();   //List of platforms in the scene

    [SerializeField]
    private int maxPrefabCount = 0;     //Max number of prefab spawns 
    private int placedPrefabCount;      //Current number of prefabs spawned
    private int prefabID;               //The current chosen prefab
    private int platformID;             //The current chosen platform
    private bool canPlace;              //Determines if the user can place objects

    [SerializeField]
    private List<GameObject> placeablePrefab = new List<GameObject>();      //List of all the objects
    [SerializeField]
    private List<GameObject> placeablePlatform = new List<GameObject>();    //List of all the platforms

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();            //List of raycast hits

    public Text debugText;
    public Text currentModelDebugText;
    public TextMeshProUGUI currentPlatText;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        prefabID = 0;
        platformID = 0;
        currentPlatText.text = "Platform: None";
        canPlace = true;
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)    //takes only the first touch input, avoids holding down and spawning multiple objects on one press
        {
            touchPosition = Input.GetTouch(0).position;     
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;  //If there are no touches, return

        if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon)){    //If the raycast hits a plane
            if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){        //If the raycast isn't over a UI element
                var hitPose = s_Hits[0].pose;       //Get the pose of the first impact
                if (canPlace){
                    if (placedPrefabCount < maxPrefabCount) SpawnPrefab(hitPose);   //if current num of prefabs is less than max, spawn new
                }
            }
        }
    }

    public void SetPrefabType(int id, GameObject prefabType)    //Changes the prefab (not used)
    {
        placeablePrefab[id] = prefabType;
    }

    public void ChangeCurrentPrefab(int id)             //Change the current selected prefab
    {
        prefabID = id;
        Debug.Log("Prefab ID set to: " + id);
        currentModelDebugText.text = "Model ID:" + id + "/n PrefabCount:"+placeablePrefab.Count;
    }

    public void ChangePlat(bool val)
    {
        if (val) platformID++;
        else platformID--;

        if (platformID < 0) platformID = placeablePlatform.Count - 1;   //If id is under 0, cycle to the end of the list

        if (platformID > placeablePlatform.Count -1 ) platformID = 0;   //if id is over max, cycle to beginning of list

        switch(platformID)
        {
            case 0: //No platform
                currentPlatText.text = "Platform: None";
                break;
            case 1: //red 
                currentPlatText.text = "Platform: Red";
                break;
            case 2: //green
                currentPlatText.text = "Platform: Green";
                break;
            case 3: //yellow
                currentPlatText.text = "Platform: Yellow";
                break;
            default:
                break;
        }
        
    }

    private void SpawnPrefab(Pose hitPose)
    {
        Quaternion newAngle = hitPose.rotation * Quaternion.Euler(Vector3.up * 180);    //Rotates objects 180 degrees to face the camera

        if(prefabID < placeablePrefab.Count && platformID < placeablePlatform.Count)    //Check if the ID is out of range
        {
            spawnedPlatform = Instantiate(placeablePlatform[platformID], hitPose.position, newAngle);           //Spawn the current platform on the raycast hit
            Vector3 spawn = spawnedPlatform.GetComponent<Platform>().getSpawnPosition();                        //Get the spawn point on the platform
            spawnedObject = Instantiate(placeablePrefab[prefabID], spawn, spawnedPlatform.transform.rotation);  //Spawn the current object on the platform
        }
        else        //If ID is out of range, use the object in [0]
        {
            spawnedPlatform = Instantiate(placeablePlatform[0], hitPose.position, newAngle);                    //Spawn the default platform on the raycast hit
            Vector3 spawn = spawnedPlatform.GetComponent<Platform>().getSpawnPosition();                        //Get the spawn point on the platform
            spawnedObject = Instantiate(placeablePrefab[3], spawn, spawnedPlatform.transform.rotation);         //Spawn the default object on the platform
        }
        placedPrefabList.Add(spawnedObject);        //Add the prefab to the list of prefabs
        placedPlatformList.Add(spawnedPlatform);    //Add the platform to the list of platforms
        placedPrefabCount++;                        //Increment the count of current prefabs
    }

    public void CanPlace(bool val)      //Determines if the user can place objects
    {
        canPlace = val;
        Debug.Log("Can place: " + canPlace.ToString());
    }

    public void ResetObjects()      //Removes all spawned prefabs
    {
        for(int i = 0; i < placedPrefabList.Count;i++)Destroy(placedPrefabList[i]);     //Clear object list
        placedPrefabList.Clear();

        for(int i = 0; i< placedPlatformList.Count;i++) Destroy(placedPlatformList[i]); //Clear platform list
        placedPlatformList.Clear();

        placedPrefabCount = 0;
    }

    public void UndoLastObject()
    {
        Destroy(placedPrefabList[placedPrefabList.Count]);
        placedPrefabCount--;
        string debugString = "Count: " + placedPrefabCount;
        debugText.text = debugString;
    }
}
