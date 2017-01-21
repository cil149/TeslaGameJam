using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    private float _distMax;
    private bool _isInitial;
    private bool _isFinal;
    private bool _isOn;
	private float _cost;
    public bool isInitial { get { return _isInitial; } set { _isInitial = value; } }
    public bool isFinal { get { return _isFinal; } set { _isFinal = value; } }
    public float distMax { get { return _distMax; } set { _distMax = value; } }
    public bool isOn { get { return _isOn; } set { _isOn = value; } }
	public float cost { get { return _cost; } set { _cost = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
