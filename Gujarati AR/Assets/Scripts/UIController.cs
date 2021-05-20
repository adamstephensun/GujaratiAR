using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class UIController : MonoBehaviour
{
    UnityEngine.XR.ARFoundation.ARReferencePointManager refMan;

    // Start is called before the first frame update
    void Start()
    {
        refMan = GameObject.Find("AR Session Origin").GetComponent<UnityEngine.XR.ARFoundation.ARReferencePointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeModel()
    {
        
    }
}
