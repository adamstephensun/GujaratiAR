using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private GameObject infoMenu;
    private GameObject settingsMenu;
    private GameObject mainMenu;
    private GameObject quitConfirmMenu;

    private Animation sceneTransAnimation;

    private Slider musicVolSlider;

    private static string sceneTransEnter = "SceneTransEnter";
    private static string sceneTransExit = "SceneTransExit";

    private void Awake()
    {
        infoMenu = GameObject.Find("InfoMenu");
        settingsMenu = GameObject.Find("SettingsMenu");
        mainMenu = GameObject.Find("MainMenu");
        quitConfirmMenu = GameObject.Find("QuitConfirm");
        musicVolSlider = GameObject.Find("MusicVolSlider").GetComponent<Slider>();

        sceneTransAnimation = GameObject.Find("SceneTransition").GetComponent<Animation>();
        
        mainMenu.SetActive(true);
        infoMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitConfirmMenu.SetActive(false);

        sceneTransAnimation.Play(sceneTransEnter);
    }

    public void StartGame()
    {
        sceneTransAnimation.Play(sceneTransExit);

        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("ARScene");
    }

    public void OpenInfo(bool val)
    {
        infoMenu.SetActive(val);
        mainMenu.SetActive(!val);
    }

    public void OpenSettings(bool val)
    {
        settingsMenu.SetActive(val);
        mainMenu.SetActive(!val);
    }

    public void OpenQuitConfirmMenu(bool val)
    {
        quitConfirmMenu.SetActive(val);
    }

    public void MusicVolChanged()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
