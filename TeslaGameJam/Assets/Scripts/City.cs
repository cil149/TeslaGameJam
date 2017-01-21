using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

	[SerializeField]
	private float _addGold;

	[SerializeField]
	private float _subGold;

	private bool _isActive;
	private bool _isActivateBefore;

	public float addGold { get { return _addGold; } set { _addGold = value; } }
	public float subGold { get { return _subGold; } set { _subGold = value; } }
	public bool isActive { get { return _isActive; } set { _isActive = value; } }
	public bool isActivateBefore { get { return _isActivateBefore; } set { _isActivateBefore = value; } }

	// Use this for initialization
	void Start () {
		isActive = false;
		isActivateBefore = false;
		EnergyManager.instance.RegisterCity (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
