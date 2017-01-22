using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackMoney : MonoBehaviour {
    public Text text;
    public EnergyManager energy;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = energy.totalGold.ToString();
	}
}
