﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {


    public static EnergyManager instance;

		private List<Tower> listTower;
		private List<Tower> listIniT;
		private List<Tower> listFinT;
		private List<Tower> listRecorridas;
		private List<Tower> listPendientes;
		private List<City> listCity;
		private List<City> visitCity;
		private bool victory;
		
		[SerializeField]
		private float _totalGold;

		public float totalGold { get { return _totalGold; } set { _totalGold = value; } }

		[SerializeField]
		private float _threeStar;

		public float threeStar { get { return _threeStar; } set { _threeStar = value; } }
		
		[SerializeField]
		private float _twoStar;

		public float twoStar { get { return _twoStar; } set { _twoStar = value; } }

		[SerializeField]
		private float _OneStar;
		
		public float OneStar { get { return _OneStar; } set { _OneStar = value; } }


		private int stars;

   //public  LayerMask towerIgnore;

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization

    private void OnEnable()
    {
        listTower = new List<Tower>();
        listIniT = new List<Tower>();
        listFinT = new List<Tower>();

        listRecorridas = new List<Tower>();
        listPendientes = new List<Tower>();

		listCity = new List<City> ();
		visitCity = new List<City> ();
        //towerIgnore = ~towerIgnore;
    }
    void Start () {

		victory = false;
		stars = 0;
	}
	
	// Update is called once per frame
	void Update () {
        IniUpdate();
        EnergyUpdate();
		checkCity ();
		victory = checkWinCondition ();
		if (victory)
			checkStars ();
    }

	void IniUpdate(){
		foreach(Tower t in listTower){
			t.isOn = false;
		}

        foreach (Tower t in listIniT)
        {
            t.isOn = true;
        }
    }

   	void EnergyUpdate()
    {
        listRecorridas.Clear();
        Tower actTower;
        listPendientes.AddRange(listIniT);
        RaycastHit hit;
        //LayerMask 

        while (listPendientes.Count != 0)
        {
            actTower = listPendientes[0];
            foreach (Tower t in listTower)
            {
                if (!listRecorridas.Contains(t))
				{
                    if (Physics.Raycast(actTower.transform.position, t.transform.position - actTower.transform.position, out hit, actTower.distMax))
                    {
                        if (hit.collider.gameObject == t.gameObject)
                        {
                            listPendientes.Add(t);
							t.isOn = true;
                        }
                        else if (hit.collider.gameObject.GetComponent<CloudScript>()) //nubes // si ha chocado con nubes
                        {
                            RaycastHit hit2;
                            //y desde la antena objetivo tambien choca con nube (Bug si se ponen dos nubes en la trayectoria de un mismo rayo? )
                            if (Physics.Raycast(t.transform.position, (t.transform.position - actTower.transform.position) * -1, out hit2, actTower.distMax))
                            {
                                CloudScript cloud = hit.collider.gameObject.GetComponent<CloudScript>();
                                float coefDownSignal;
                                if (cloud)// (choca con nube tambien)
                                {
                                    coefDownSignal = cloud.signalDownCoef;
                                    float distCloud = actTower.distMax - Vector3.Distance(hit.point, hit2.point) * coefDownSignal;
                                    //Si con las restricciones de la distancia, sigue llegando a la antena, entonces puede atravesar la nube
                                    if (Physics.Raycast(actTower.transform.position, t.transform.position - actTower.transform.position, out hit, distCloud, 8))
                                    {
                                        listPendientes.Add(t);
                                        t.isOn = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        
		    foreach (City c in listCity) {
			    if (Physics.Raycast (actTower.transform.position, c.transform.position - actTower.transform.position, out hit, actTower.distMax)) {
				    if (c.gameObject == hit.collider.gameObject) {
					    visitCity.Add (c);
				    }
                    
                    else if (hit.collider.gameObject.GetComponent<CloudScript>() ) //nubes // si ha chocado con nubes
                    {
                        RaycastHit hit2;
                        //y desde la antena objetivo tambien choca con nube (Bug si se ponen dos nubes en la trayectoria de un mismo rayo? )
                        if (Physics.Raycast(c.transform.position, (c.transform.position - actTower.transform.position) * -1, out hit2, actTower.distMax))
                        {
                            CloudScript cloud = hit.collider.gameObject.GetComponent<CloudScript>();
                            float coefDownSignal;
                            if (cloud) { // (choca con nube tambien)
                                coefDownSignal = cloud.signalDownCoef;
                                float distCloud = actTower.distMax - Vector3.Distance(hit.point, hit2.point)* coefDownSignal;
                                //Si con las restricciones de la distancia, sigue llegando a la antena, entonces puede atravesar la nube
                                if (Physics.Raycast(actTower.transform.position, c.transform.position - actTower.transform.position, out hit, distCloud, 8))
                                {
                                    visitCity.Add(c);
                                }
                            }
                        }
                    }
                }
		    }
		listRecorridas.Add (actTower);
		listPendientes.Remove (actTower);
        }
    }

	void checkCity(){
		foreach (City c in listCity) {
			if (visitCity.Contains (c)) {
				if (!c.isActivateBefore){
					c.isActivateBefore = true;
					ActGold (c.addGold);
				}
				c.isActive = true;
			}else if(c.isActive){
				ActGold (c.subGold);
				c.isActive = false;
			}
		}
		visitCity.Clear ();
	}


    bool checkWinCondition()
    {
		int finishTowerOn=0;
		foreach (Tower t in listFinT) {
			if (t.isOn)
				finishTowerOn++;
		}
		if (finishTowerOn == listFinT.Count) {
			return true;
		}
		return false;
    }

  public void RegisterTower(Tower t)
    {
        
		if (t.isInitial) {
			listIniT.Add (t);
		} else if (t.isFinal) {
			listFinT.Add (t);
			listTower.Add (t);
		} else {
			listTower.Add (t);
			ActGold (t.cost);
		}
    }

   public void UnregisterTower(Tower t)
    {
        listTower.Remove(t);

        if (t.isInitial)
        {
            listIniT.Remove(t);
        }
        else if (t.isFinal)
        {
            listFinT.Remove(t);
        }
    }

	public void RegisterCity(City c){
		listCity.Add (c);
	}

	public void unRegisterCity(City c){
		listCity.Remove (c);
	}

	void ActGold(float gold){
			totalGold-=gold;
	}

	void checkStars(){
		if (totalGold > threeStar )
			stars = 3;
		else if (totalGold > twoStar)
			stars = 2;
		else if (totalGold > OneStar)
			stars = 1;
		else if (totalGold > 50)
			stars = 0;
	}
}


