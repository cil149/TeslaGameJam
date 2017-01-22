using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour {

    public static OptionManager instance;

    public bool _InverseXAxis, _InverseYAxis, _EnableMusic, _EnableSound; 

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        instance = this;
	}

    public void defaultValues()
    {
        _InverseXAxis = false;
        _InverseYAxis = false;
        _EnableMusic = false;
        _EnableSound = false; 

    }

    public void EnableMusic()
    {
        _EnableMusic = true;
    }

    public void DisableMusic()
    {
        _EnableMusic = false;
    }

    public void EnableSound()
    {
        _EnableSound = true;
    }
    public void DisableSound()
    {
        _EnableSound = false;
    }

    public void EnableInverseXAxis()
    {
        _InverseXAxis = true;
    }

    public void DisableInverseXAxis()
    {
        _InverseXAxis = false;
    }

    public void EnableInverseYAxis()
    {
        _InverseYAxis = true;
    }
    public void DisableInverseYAxis()
    {
        _InverseYAxis = false;
    }
}
