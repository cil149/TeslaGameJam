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

    public Material mat; 

	// Use this for initialization
	void Start () {
        actScale = minScale;

    }

    // Update is called once per frame
    void LateUpdate() {
        if (towerState.isOn) {
            SphereMax.SetActive(true);
            SphereGrown.SetActive(true);
            actScale += Time.deltaTime*4;

            mat.color = Color.Lerp(
                new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f),
                new Color(mat.color.r, mat.color.g, mat.color.b, 0f),
                (actScale-minScale)/maxScale);


            if (actScale > maxScale)
            {
                actScale = minScale;
            }
            Vector3 s = new Vector3(actScale, actScale, actScale);
            SphereGrown.transform.localScale = s;
            

        }else
        {
            SphereMax.SetActive(false);
            SphereGrown.SetActive(false);
        }
    }
}
