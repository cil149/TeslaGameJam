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

    public void EnableMusic(bool b)
    {
        _EnableMusic = b;
    }


    public void EnableSound(bool b)
    {
        _EnableSound = b;
    }

    public void EnableInverseXAxis(bool b)
    {
        _InverseXAxis = b;
    }


    public void EnableInverseYAxis(bool b)
    {
        _InverseYAxis = b;
    }
}
