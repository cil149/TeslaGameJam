using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    private float _distMax;
    [SerializeField]
    private bool _isInitial;
    [SerializeField]
    private bool _isFinal;

    private bool _isOn;

    [SerializeField]
    private float _cost;

    [SerializeField]
    private Renderer _torus_rend;

    public bool isInitial { get { return _isInitial; } set { _isInitial = value; } }
    public bool isFinal { get { return _isFinal; } set { _isFinal = value; } }
    public float distMax { get { return _distMax; } set { _distMax = value; } }
    public bool isOn { get { return _isOn; } set { _isOn = value; } }
	public float cost { get { return _cost; } set { _cost = value; } }


    Material matOn;
    Material matOff;



    void Awake()
    {
        Material[] mats = _torus_rend.materials;

        matOff = mats[0];
        matOn = mats[1];
    }

    void OnEnable()
    {
       // EnergyManager.instance.RegisterTower(this);
    }
    // Use this for initialization
    void Start()
    {
        EnergyManager.instance.RegisterTower(this);
    }

    void OnDisable()
    {
        EnergyManager.instance.UnregisterTower(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn && _torus_rend.material != matOn)
        {

            _torus_rend.material = matOn;
        }
        else if (!isOn && _torus_rend.material != matOff)
        {
            _torus_rend.material = matOff;
        }

    }


}
