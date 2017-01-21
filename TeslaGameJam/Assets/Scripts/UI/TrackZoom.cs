using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackZoom : MonoBehaviour {
    MouseOrbitImproved m;
    Slider slider; 
	// Use this for initialization
	void Start () {
        m = CameraInput.instance.gameObject.GetComponent<MouseOrbitImproved>();
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = Mathf.Clamp((m.distance - m.distanceMin) / m.distanceMax, 0.2f, 1);
	}
}
