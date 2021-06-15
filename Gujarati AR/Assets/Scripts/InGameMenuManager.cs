using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class InGameMenuManager : MonoBehaviour
{
    private GameObject pauseMenu;   //Contains all gameobjects in the pausemenu
    private GameObject objectMenu;  //" " object select menu
    private GameObject lightMenu;   //" " light colour menu
    private GameObject buttons;     //" " all UI buttons

    private Animation sceneTransAnimation;

    private SpawnObjectOnPlane objSpawner;  //Reference to the obj spawner, used to disable placement in menus

    private static string sceneTransEnter = "SceneTransEnter";
    private static string sceneTransExit = "SceneTransExit";

    public GameObject planeVisualiser;
    private bool isPlaneVisible;

    private void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        objectMenu = GameObject.Find("ObjectSelectMenu");
        lightMenu = GameObject.Find("LightMenu");
        objSpawner = GameObject.Find("AR Session Origin").GetComponent<SpawnObjectOnPlane>();

        buttons = GameObject.Find("Buttons");

        sceneTransAnimation = GameObject.Find("SceneTransition").GetComponent<Animation>();

        pauseMenu.SetActive(false);
        objectMenu.SetActive(false);
        lightMenu.SetActive(false);

        sceneTransAnimation.Play(sceneTransEnter);
        isPlaneVisible = true;
        
    }

    public void QuitGame()          //Closes the game
    {
        Debug.Log("Main Menu Loaded");

        sceneTransAnimation.Play(sceneTransExit);
        StartCoroutine(exitScene());
    }

    IEnumerator exitScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void OpenPauseMenu(bool val)     //Toggles the pause menu with a bool
    {
        ShowButtons(!val);
        pauseMenu.SetActive(val);
        objSpawner.CanPlace(!val);
    }

    public void OpenObjMenu(bool val)       //Toggles the object selection menu with a bool
    {
        ShowButtons(!val);
        objectMenu.SetActive(val);
        objSpawner.CanPlace(!val);
    }

    public void OpenLightMenu(bool val)     //Toggles the light colour menu with a bool
    {
        ShowButtons(!val);
        lightMenu.SetActive(val);
        objSpawner.CanPlace(!val);
    }

    public void ShowButtons(bool val)       //Toggle the UI buttons
    {
        buttons.SetActive(val);
    }

    public void TogglePlaneVisualisation()
    {
        if(isPlaneVisible)
        {
            isPlaneVisible = false;
            planeVisualiser.GetComponent<MeshRenderer>().enabled = isPlaneVisible;
            planeVisualiser.GetComponent<LineRenderer>().enabled = isPlaneVisible;
        }
        else
        {
            isPlaneVisible = true;
            planeVisualiser.GetComponent<MeshRenderer>().enabled = isPlaneVisible;
            planeVisualiser.GetComponent<LineRenderer>().enabled = isPlaneVisible;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
