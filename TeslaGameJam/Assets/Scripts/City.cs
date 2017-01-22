using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

	[SerializeField]
	private float _addGold;

	[SerializeField]
	private float _subGold;

	[SerializeField]
	private Renderer _torus_rend;

	[SerializeField]
	private int _type;

	private bool _isActive;
	private bool _isActivateBefore;

	public float addGold { get { return _addGold; } set { _addGold = value; } }
	public float subGold { get { return _subGold; } set { _subGold = value; } }
	public bool isActive { get { return _isActive; } set { _isActive = value; } }
	public bool isActivateBefore { get { return _isActivateBefore; } set { _isActivateBefore = value; } }
	public int type { get { return _type; } set { _type = value; } }

	public Material matOn1;
	public Material matOn2;
	public Material matOff;


	// Use this for initialization
	void Start () {
		isActive = false;
		isActivateBefore = false;
		EnergyManager.instance.RegisterCity (this);
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) {
		case 1:
			if (isActive && _torus_rend.material != matOn1)
				_torus_rend.material = matOn1;
			break;
		case 2:
			if (isActive && _torus_rend.material != matOn2)
				_torus_rend.material = matOn2;
			break;
		default :
			_torus_rend.material = matOff;
			break;
		}		
	}
}
