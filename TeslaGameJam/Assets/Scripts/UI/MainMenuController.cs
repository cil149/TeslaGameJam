using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public RectTransform MainMenuPanel, OptionsPanel, CreditsPanel, ExitPrompt;
 
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
    }
public void GotoExitPrompt(){
    
        MainMenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(false);
        ExitPrompt.gameObject.SetActive(true);
    }

    
public void GotoCredits(){
    
        MainMenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(true);
        ExitPrompt.gameObject.SetActive(false);
    }

    
public void GotoMainMenu(){
    
        MainMenuPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(false);
        ExitPrompt.gameObject.SetActive(false);
    }



}
