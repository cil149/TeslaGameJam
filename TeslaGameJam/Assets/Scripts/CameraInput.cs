using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour {

    public GameObject _Camera;
    public GameObject _MyPlanet;


    public GameObject _towerPrefab;

    GameObject _towerPrefab_Instance = null;
    Tower towerPrefab_Tower
    {
        get
        {
            if (_towerPrefab_Instance)
            {
                return _towerPrefab_Instance.transform.GetChild(2).GetComponent<Tower>();
            }
            return null;
        }
    }
	// Use this for initialization
	void Start () {
		
	}


    bool ButtonADown = false;

	// Update is called once per frame
    void Update()
    {

        if (_towerPrefab_Instance == null)
        {
            _towerPrefab_Instance = Instantiate(_towerPrefab) as GameObject;
            towerPrefab_Tower.isInEditMode = true;
        }


        //RaycastHit hit;

        //if (Physics.Raycast(_Camera.transform.position, _MyPlanet.transform.position - _Camera.transform.position, out hit, 1 << 10))
        //{
        //    if (hit.collider.tag == "Planet")
        //    {
        //        _towerInstance.transform.position = hit.point;
        //        _towerInstance.transform.up = hit.normal;
        //    }

        //}




        RaycastHit[] hit = Physics.RaycastAll(_Camera.transform.position, _MyPlanet.transform.position - _Camera.transform.position, 1 << 10);
        for (int i = 0; i < hit.Length; i++ )
        {
            if (hit[i].collider.tag == "Planet")
            {
                _towerPrefab_Instance.transform.position = hit[i].point;
                _towerPrefab_Instance.transform.up = hit[i].normal;
            }

        }



        if (InputController.instance.AButton > 0f && !ButtonADown)
        {
            if (towerPrefab_Tower && towerPrefab_Tower.CanPlaceHere)
            {
                towerPrefab_Tower.isInEditMode = false;
                _towerPrefab_Instance = Instantiate(_towerPrefab) as GameObject;
                towerPrefab_Tower.isInEditMode = true;
                
            }
            ButtonADown = true;
        }
        else if (InputController.instance.AButton == 0f )
        {
            ButtonADown = false;
        }

    }


    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_Camera.transform.position, _MyPlanet.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_Camera.transform.position, _towerInstance.transform.position);
    }
    */
}
