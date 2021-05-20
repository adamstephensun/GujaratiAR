using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject inv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 touchDelta = Input.GetTouch(0).deltaPosition;
            inv.GetComponent<Transform>().Translate(-touchDelta.x, 0, 0);
            Debug.Log("Touch: " + touchDelta);
        }

    }
}
