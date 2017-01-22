using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioMixer mixer; 


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}


    public void Update()
    {
        mixer.SetFloat("SoundVolume", OptionManager.instance._EnableSound ? 0 : -80);
        mixer.SetFloat("MusicVolume", OptionManager.instance._EnableMusic ? 0 : -80);
        
    }
}
