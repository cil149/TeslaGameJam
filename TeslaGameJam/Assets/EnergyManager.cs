using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {
    
         List<Tower> listTower;
         List<Tower> listIniT;
         List<Tower> listFinT;
         List<Tower> listRecorridas;
         List<Tower> listPendientes;

    // Use this for initialization
    void Start () {
        listTower = new List<Tower>();
        listIniT = new List<Tower>();
        listFinT = new List<Tower>();

        listRecorridas = new List<Tower>();
        listPendientes = new List<Tower>();
    }
	
	// Update is called once per frame
	void Update () {
        EnergyUpdate();




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

    }



    void RegisterTower(Tower t)
    {
        listTower.Add(t);
        if (t.isInitial)
        {
            listIniT.Add(t);
        }else if (t.isFinal)
        {
            listFinT.Add(t);
        }
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
