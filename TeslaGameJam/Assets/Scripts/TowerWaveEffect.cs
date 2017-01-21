using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWaveEffect : MonoBehaviour {

    public GameObject SphereMax;
    public GameObject SphereGrown;
    public Tower towerState;
        

    public float maxScale = 10;
    public float minScale=1;
    private float actScale;
	// Use this for initialization
	void Start () {
        actScale = minScale;

    }

    // Update is called once per frame
    void Update() {
        if (towerState.isOn) {
            SphereMax.active = true;
            SphereGrown.active = true;
            actScale += Time.deltaTime*4;
            if (actScale > maxScale)
            {
                actScale = minScale;
            }
            Vector3 s = new Vector3(actScale, actScale, actScale);
            SphereGrown.transform.localScale = s;
            

        }else
        {
            SphereMax.active = false;
            SphereGrown.active = false;
        }
    }
}
