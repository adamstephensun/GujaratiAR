using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour      //Manages object selection menus
{
    
    private TextMeshProUGUI objName;            //The name of the object
    private TextMeshProUGUI description;        //Object description
    private Image image;                        //Thumbnail for the object
    public InGameMenuManager menuController;   //Reference to the menu manager. Used to close the menu when a user selects an object
    public SpawnObjectOnPlane objSpawner;      //Reference to the AR object spawner. Used to change the current selected object

    [SerializeField]
    private GameObject objButtons;              //UI buttons from the obj select screen

    private int currentID;                      //ID of the current viewed object
    private GameObject buttons;                 //The buttons on the object details screen

    public Object[] objects;                    //Array of object details

    private TextMeshProUGUI obj0Text;
    private TextMeshProUGUI obj1Text;
    private TextMeshProUGUI obj2Text;
    private TextMeshProUGUI obj3Text;

    void Start()
    {
        objName = GameObject.Find("ObjName").GetComponent<TextMeshProUGUI>();
        description = GameObject.Find("ObjDesc").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find("ObjImage").GetComponent<Image>();
        buttons = GameObject.Find("ObjDetailButtons");

        obj0Text = GameObject.Find("obj0Text").GetComponent<TextMeshProUGUI>();
        obj1Text = GameObject.Find("obj1Text").GetComponent<TextMeshProUGUI>();
        obj2Text = GameObject.Find("obj2Text").GetComponent<TextMeshProUGUI>();
        obj3Text = GameObject.Find("obj3Text").GetComponent<TextMeshProUGUI>();

        obj0Text.text = objects[0].objName;
        obj1Text.text = objects[1].objName;
        obj2Text.text = objects[2].objName;
        obj3Text.text = objects[3].objName;

        OpenDetailsMenu(false);
    }

    public void OpenObjDetails(int id)      //Opens the object detail screen
    {
        if(id < objects.Length) {      //Checks that the passed id is valid
            Debug.Log("OBJdetails opened:  ID = " + id+ "   name = "+objects[id].objName);
            OpenDetailsMenu(true);                                     //Opens the menu

            objName.text = objects[id].objName;                 //Sets the name to the selected obj name
            description.text = objects[id].objDescription;      //Sets the description to the selected obj description
            image.sprite = objects[id].icon;                    //Sets the icon to the selected obj icon
            currentID = id;                                     //Stores the ID for when the user selects the object
        }
        else Debug.Log("Object out of bounds");
    }

    public void SelectObj()             //Button to select the object for viewing in AR
    {
        objSpawner.ChangeCurrentPrefab(currentID);      //Changes the chosen prefab AR spawner 
        Debug.Log("Using prefab ID: " + currentID);

        OpenDetailsMenu(false);                                //Closes all menus
        menuController.OpenObjMenu(false);              
    }

    public void OpenDetailsMenu(bool val)                  //Opens and closes the obj details menu
    {
        objName.gameObject.SetActive(val);
        description.gameObject.SetActive(val);
        image.gameObject.SetActive(val);

        buttons.SetActive(val);

        objButtons.SetActive(!val);
    }

}
