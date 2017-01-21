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


    public Material matOn;
    public Material matOff;


    private bool _isInEditMode;
    public bool isInEditMode
    {
        get { return _isInEditMode; }
        set
        {
            _isInEditMode = value;

            if (!_isInEditMode) EnergyManager.instance.RegisterTower(this);
        }
    }

    public bool CanPlaceHere
    {
        get
        {
            if (isInEditMode)
            {
                
                foreach(Collider c in Physics.OverlapSphere(transform.position, 1.089f, 1 << 8) )
                {
                    if (!c.GetComponent<Tower>()._isInEditMode) return false;
                }
                
                if (CameraInput.instance.CheckIfLandIsOnFoot())
                {
                    return true;
                }
            }
            return false;
        }
    }


    void Awake()
    {
        //Material[] mats = _torus_rend.materials;

        //matOff = mats[0];
        //matOn = mats[1];
    }

    void OnEnable()
    {
       // EnergyManager.instance.RegisterTower(this);
    }
    // Use this for initialization
    void Start()
    {
        if(_isInitial )EnergyManager.instance.RegisterTower(this);
    }

    void OnDisable()
    {
        EnergyManager.instance.UnregisterTower(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (isInEditMode)
        {
            return;
        }

        if (isOn && _torus_rend.material != matOn)
        {

            _torus_rend.material = matOn;
        }
        else if (!isOn && _torus_rend.material != matOff)
        {
            _torus_rend.material = matOff;
        }

    }


    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position , 1.089f);
        Gizmos.color = Color.green;
    }
    */
}
