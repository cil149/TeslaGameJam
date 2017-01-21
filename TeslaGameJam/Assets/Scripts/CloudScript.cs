using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {
    Transform trans;
   public LayerMask layerTierra;

    public float signalDownCoef; 

   public float velocity;
   public float rnd_maxTimeToChangeDir=10;
   public float rnd_minTimeToChangeDir = 3;
   public bool wpMode = false;
    public int wpAct=0;

    public List<Vector3> wpList;  

   private float rnd_timeWithDir=0;
   private Vector3 dir;
   private float rnd_timeToChangeDir=0;

    
	// Use this for initialization
	void Start () {

        trans = gameObject.transform;
        dir= new Vector3(0, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (wpMode)
        {
            Vector3 posObj = wpList[wpAct];
            Vector3 posObjProy = posObj;
            posObjProy.y = 0;
            Vector3 posTransProy = trans.position;
            posTransProy.y = 0;
            if (Vector3.Distance(posObjProy, posTransProy) <1f)
            {
                wpAct++;
                if (wpAct == wpList.Count) wpAct = 0;

                posObj = wpList[wpAct];
            }

            
            dir = posObj - trans.position;
            dir.y = 0;
            dir.Normalize();
            
            
        }
        else
        {
            rnd_timeWithDir += Time.deltaTime;
            if (rnd_timeWithDir >= rnd_timeToChangeDir) //la primera vez entra porque timeToChangeDir es 0
            {
                rnd_timeWithDir = 0;
                rnd_timeToChangeDir = Random.Range(rnd_minTimeToChangeDir, rnd_maxTimeToChangeDir);
                dir.x = Random.Range(-1.0f, 1.0f);
                dir.z = Random.Range(-1.0f, 1.0f);

            }
        }

        trans.Translate(Time.deltaTime* velocity*dir.x, 0, Time.deltaTime * velocity * dir.z);

        RaycastHit hit;
        if(Physics.Raycast(trans.position, (trans.up)*-1, out hit, 1000, layerTierra)){
            trans.up = hit.normal;
            trans.position = hit.point + hit.normal * 0.5f;

        }

    }
}
