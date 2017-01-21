using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {
    
         List<Tower> listTower;
         List<Tower> listIniT;
         List<Tower> listFinT;
         List<Tower> listRecorridas;
         List<Tower> listPendientes;
	bool victory;
    // Use this for initialization
    void Start () {
        listTower = new List<Tower>();
        listIniT = new List<Tower>();
        listFinT = new List<Tower>();

        listRecorridas = new List<Tower>();
        listPendientes = new List<Tower>();
		victory = false;
	}
	
	// Update is called once per frame
	void Update () {
		SwapOff ();
        EnergyUpdate();
		victory = checkWinCondition ();
    }

	void SwapOff(){
		foreach(Tower t in listTower){
			t.isOn = false;
		}
	}

   	void EnergyUpdate()
    {
        listRecorridas.Clear();
        Tower actTower;
        listPendientes.AddRange(listIniT);
        RaycastHit hit;
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

    void RegisterTower(Tower t)
    {
        
        if (t.isInitial)
        {
            listIniT.Add(t);
        }else if (t.isFinal)
        {
            listFinT.Add(t);
			listTower.Add(t);
		}else
			listTower.Add(t);
    }

    void UnregisterTower(Tower t)
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
}
