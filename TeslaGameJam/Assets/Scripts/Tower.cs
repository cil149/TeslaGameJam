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
	[SerializeField]
    private bool _isOn;

    [SerializeField]
    private float _cost;

    [SerializeField]
    private Renderer _torus_rend;

	[SerializeField]
	private int _type;


    public bool isInitial { get { return _isInitial; } set { _isInitial = value; } }
    public bool isFinal { get { return _isFinal; } set { _isFinal = value; } }
    public float distMax { get { return _distMax; } set { _distMax = value; } }
    public bool isOn { get { return _isOn; } set { _isOn = value; } }
	public float cost { get { return _cost; } set { _cost = value; } }
	public int type { get { return _type; } set { _type = value; } }

	public Collider sphere;

    public Material matOnt1;
	public Material matOnt2;
    public Material matOff;

    private bool _isInEditMode;
    public bool isInEditMode
    {
        get { return _isInEditMode; }
        set
        {
            _isInEditMode = value;
			type = 0;
			if (!_isInEditMode) {
				this.sphere.enabled = true;
				EnergyManager.instance.RegisterTower (this);

			}
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
					if (!c.GetComponent<Tower> ()._isInEditMode) {
						return false;

					}
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
		if(_isFinal) EnergyManager.instance.RegisterTower(this);
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
		switch(type){
		case 1:
			if ((isOn && _torus_rend.material != matOnt1) || (_isFinal))
				_torus_rend.material = matOnt1;
			break;
		case 2:
			if ((isOn && _torus_rend.material != matOnt2)|| (_isFinal))
				_torus_rend.material = matOnt2;
			break;
		default :
			_torus_rend.material = matOff;
			break;
		}
		//if (!isOn && _torus_rend.material != matOff)
		//	_torus_rend.material = matOff;
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
