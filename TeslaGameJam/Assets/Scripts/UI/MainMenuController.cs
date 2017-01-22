using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public RectTransform MainMenuPanel, OptionsPanel, CreditsPanel, ExitPrompt;

    public GameObject firstSelectedOnMainMenus, firstSelectedOnOptions, firstSelectedOnCredits, firstSelectedOnExit;


	// Use this for initialization
	void Start () {
		
	}
	
    public void GotoPlay(){

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
