using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour {

    public GameObject _Camera;
    public GameObject _MyPlanet;


    public GameObject _towerPrefab;

    GameObject _towerInstance = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(_towerInstance == null){
            _towerInstance = Instantiate(_towerPrefab) as GameObject;
        }

        RaycastHit hit;
        if (Physics.Raycast(_Camera.transform.position, _MyPlanet.transform.position, out hit))
        {
            Debug.Log("COLLISION");
            _towerInstance.transform.position = hit.transform.position;

        }

        

	}



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_Camera.transform.position, _MyPlanet.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_Camera.transform.position, _towerInstance.transform.position);
    }
}
