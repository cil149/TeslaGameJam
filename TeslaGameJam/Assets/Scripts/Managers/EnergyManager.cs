using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {


    public static EnergyManager instance;

		private List<Tower> listTower;
		private List<Tower> listIniT;
		private List<Tower> listFinT;
		private List<Tower> listRecorridas;
		private List<Tower> listPendientes;
		private bool victory;

		[SerializeField]
		private float totalGold;

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
                    }
                }
            }
            listRecorridas.Add(actTower);
            listPendientes.Remove(actTower);
        }
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

  public  void RegisterTower(Tower t)
    {
        
		if (t.isInitial) {
			listIniT.Add (t);
		} else if (t.isFinal) {
			listFinT.Add (t);
			listTower.Add (t);
		} else {
			listTower.Add (t);
			ActGold ();
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

	void ActGold(){
		foreach (Tower t in listTower) {
			totalGold-=t.cost;
		}
	}

	void checkStars(){
		if (totalGold > 750 )
			stars = 3;
		else if (totalGold > 500)
			stars = 2;
		else if (totalGold > 250)
			stars = 1;
		else if (totalGold > 50)
			stars = 0;
	}
}


