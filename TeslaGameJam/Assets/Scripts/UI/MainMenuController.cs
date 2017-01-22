using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public RectTransform MainMenuPanel, OptionsPanel, CreditsPanel, ExitPrompt;

    public GameObject firstSelectedOnMainMenus, firstSelectedOnOptions, firstSelectedOnCredits, firstSelectedOnExit;

    public Toggle InvertX, InvertY, enableAudio, enableMusic;
	// Use this for initialization
	void Start () {
		InvertX.isOn = OptionManager.instance._InverseXAxis;
        InvertY.isOn = OptionManager.instance._InverseYAxis;
        enableAudio.isOn = OptionManager.instance._EnableSound;
        enableMusic.isOn = OptionManager.instance._EnableMusic;
	}


    public void Update()
    {
        OptionManager.instance.EnableInverseXAxis(InvertX.isOn);
        OptionManager.instance.EnableInverseYAxis(InvertY.isOn);
        OptionManager.instance.EnableMusic(enableMusic.isOn);
        OptionManager.instance.EnableSound(enableAudio.isOn);
    }

    public void GotoPlay(){
        SceneManager.LoadScene("Tuto1", LoadSceneMode.Single);
    }
    
public void GotoOptions(){
        MainMenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(true);
        CreditsPanel.gameObject.SetActive(false);
        ExitPrompt.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(firstSelectedOnOptions);
    }
public void GotoExitPrompt(){

    MainMenuPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(false);
        ExitPrompt.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(firstSelectedOnExit);
    
    }

    
public void GotoCredits(){
    
        MainMenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(true);
        ExitPrompt.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(firstSelectedOnCredits);
    
    }

    
public void GotoMainMenu(){
    
        MainMenuPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(false);
        ExitPrompt.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(firstSelectedOnMainMenus);
    
    }



}
