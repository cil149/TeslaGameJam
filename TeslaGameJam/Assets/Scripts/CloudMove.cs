using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
    Transform trans;
   public LayerMask layerTierra;
   public float velocity;
   public float maxTimeToChangeDir=10;
   public float minTimeToChangeDir = 3;

   private float timeWithDir=0;
    private Vector3 dir;
   private float timeToChangeDir=0;
	// Use this for initialization
	void Start () {

        trans = gameObject.transform;
        dir= new Vector3(0, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {

        timeWithDir += Time.deltaTime;
        if( timeWithDir>= timeToChangeDir) //la primera vez entra porque timeToChangeDir es 0
        {
            timeWithDir = 0;
            timeToChangeDir = Random.Range(minTimeToChangeDir, maxTimeToChangeDir);
            dir.x = Random.Range(-1.0f, 1.0f);
            dir.z = Random.Range(-1.0f, 1.0f);

        }

        trans.Translate(Time.deltaTime* velocity*dir.x, 0, Time.deltaTime * velocity * dir.z);

        RaycastHit hit;
        if(Physics.Raycast(trans.position, (trans.up)*-1, out hit, 1000, layerTierra)){
            trans.up = hit.normal;
            trans.position = hit.point + hit.normal * 0.5f;

        }

    }
}
