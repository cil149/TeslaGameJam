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

        _InverseXAxis = PlayerPrefs.GetInt("InvX", 0) == 1;
        _InverseYAxis = PlayerPrefs.GetInt("InvY", 0) == 1;
        _EnableMusic = PlayerPrefs.GetInt("EnaMus", 0) == 1;
        _EnableSound = PlayerPrefs.GetInt("EnaSou", 0) == 1;


	}

    float actual = 0; 
    public void Update()
    {
        actual += Time.deltaTime;

        if (actual > 2f)
        {
            PlayerPrefs.Save();
            actual -= 2f;
        }
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

        PlayerPrefs.SetInt("EnaMus", _EnableMusic? 1: 0);
    }


    public void EnableSound(bool b)
    {
        _EnableSound = b;

        PlayerPrefs.SetInt("EnaSou", _EnableSound ? 1 : 0);
    }

    public void EnableInverseXAxis(bool b)
    {
        _InverseXAxis = b;

        PlayerPrefs.SetInt("InvX", _InverseXAxis ? 1 : 0);
    }


    public void EnableInverseYAxis(bool b)
    {
        _InverseYAxis = b;

        PlayerPrefs.SetInt("InvY", _InverseYAxis ? 1 : 0);
    }
}
