using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {


    public static EnergyManager instance;

		private List<Tower> listTower;
		private List<Tower> listIniT;
		private List<Tower> listFinT;
		[SerializeField]
		private List<Tower> listFinArrv;
		private List<Tower> listRecorridas;
		private List<Tower> listPendientes;
		private List<City> listCity;
		private List<City> visitCity;
		private bool end;
		private int estado;
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

        public  LayerMask marLayer; //Para ignorar

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
		listFinArrv = new List<Tower> ();
        listRecorridas = new List<Tower>();
        listPendientes = new List<Tower>();

		listCity = new List<City> ();
		visitCity = new List<City> ();
        marLayer = ~marLayer;
    }
    void Start () {

		end = false;
		stars = 0;
		estado = 0;
	}
	
	// Update is called once per frame
	void Update () {
        IniUpdate();
        EnergyUpdate();
		checkCity ();
		end = checkWinCondition ();
		if (end){
			if(estado==1){
				checkStars ();
				////victoria
			}else{
				//////Derrota
			}
		}
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
                    if (Physics.Raycast(actTower.transform.position, t.transform.position - actTower.transform.position, out hit, actTower.distMax, marLayer))
                    {
                        if (hit.collider.gameObject == t.gameObject)
                        {
                            
                            TowerHit(t, actTower);
                            /* if (t.isFinal) {
								if (t.type == actTower.type  && !listFinArrv.Contains(t)) {
									listFinArrv.Add (t);
                                    
                                }
							} else {
								if (t.type == 0) {
									t.type = actTower.type;
									listPendientes.Add (t);
								} else if (t.type == actTower.type)
									listPendientes.Add (t);
								//t.isOn = true;
							}*/
                        }else
                        {
                            checkClouds(t, actTower);
                        }
                    }
                }
            }
			foreach (City c in listCity) {
				if (Physics.Raycast (actTower.transform.position, c.transform.position - actTower.transform.position, out hit, actTower.distMax, marLayer)) {
					if (c.gameObject == hit.collider.gameObject) {
						visitCity.Add (c);
						if (c.type == 0) {
							c.type = actTower.type;
						}
					}
				}
			}
			listRecorridas.Add (actTower);
			listPendientes.Remove (actTower);
        }
    }


    private void TowerHit(Tower targetTower, Tower actTower)
    {
        targetTower.isOn = true; // Marcos, con esto se encienden las torres objetivo con su esfera
        if (targetTower.isFinal)
        {
            if (targetTower.type == actTower.type && !listFinArrv.Contains(targetTower))
            {
                listFinArrv.Add(targetTower);

            }
        }
        else
        {
            if (targetTower.type == 0)
            {
                targetTower.type = actTower.type;
                listPendientes.Add(targetTower);
            }
            else if (targetTower.type == actTower.type)
                listPendientes.Add(targetTower);
            //t.isOn = true;
        }
    }



    private void checkClouds(Tower targetTower, Tower actTower)
    {
        RaycastHit hit;
        Physics.Raycast(actTower.transform.position, targetTower.transform.position - actTower.transform.position, out hit, actTower.distMax);
        if (hit.collider.gameObject.GetComponent<CloudScript>()) //nubes // si ha chocado con nubes
        {

            RaycastHit hit2;
            //y desde la antena objetivo tambien choca con nube (Bug si se ponen dos nubes en la trayectoria de un mismo rayo? )
            if (Physics.Raycast(targetTower.transform.position, (targetTower.transform.position - actTower.transform.position) * -1, out hit2, actTower.distMax))
            {
                
                CloudScript cloud = hit.collider.gameObject.GetComponent<CloudScript>();
                float coefDownSignal;
                if (cloud)// (choca con nube tambien)
                {



                    coefDownSignal = cloud.signalDownCoef;
                    float distCloud = actTower.distMax - Vector3.Distance(hit.point, hit2.point) * coefDownSignal;
                    //Si con las restricciones de la distancia, sigue llegando a la antena, entonces puede atravesar la nube
                    //RaycastHit hitFinal;
                    
                    if (distCloud >= Vector3.Distance(targetTower.transform.position, actTower.transform.position))
                    //if (Physics.Raycast(actTower.transform.position, t.transform.position - actTower.transform.position, out hit, actTower.distMax,8))//solo a otras torres
                    {
                        // listPendientes.Add(targetTower);
                        // targetTower.isOn = true;
                        TowerHit(targetTower, actTower);

                        /* if (hit.collider.gameObject == t.gameObject)
                         {

                         }*/ //Aqui habria algunas posibilidades con varios objetos entre medias en que habria bugs.
                             //listPendientes.Add(t);
                             //t.isOn = true;
                    }
                }
            }
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
				//ActGold (c.subGold);
				c.isActive = false;
				c.type = 0;
			}
		}
		visitCity.Clear ();
	}


    bool checkWinCondition()
    {
		if (listFinArrv.Count == listFinT.Count) {
			estado = 1;
			return true;
		} else if (totalGold <= 0)
			return true;
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
        //if gold es negativo perder?
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


