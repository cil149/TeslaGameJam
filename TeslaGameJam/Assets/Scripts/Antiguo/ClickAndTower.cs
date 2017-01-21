using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-Ya funcionan las torres: tienen un componente que se llama towerMech que es donde llevan la logica
 * Solo ese componente tiene collider y es desde donde se lanzan los rayos. (asi no bloquea la base de la torre el rayo)
 * Hay 2 prefabs, uno para torre Inicial y otro para torre Repetidora (aunque solo cambia un bool). Carpeta prefabs
 * Cualquier obstaculo (con cualquier layout) entre dos torres hace que no llegue la onda.
 * 
 * Este script esta puesto en la bola gigante que he usado de tierra.
 * Tiene un layout como parametro que debe ser el que tenga la superficie que si admita torres. Es el de tierra que es el 10 (mas abajo esta hardcodeado porque no me salio de otra manera)
 * Lo que hace es crear torres al clickar
 * Habria que adaptarlo para la tierra que ha hecho angel. (no he podido usar esa porque el mando no me funcionaba)
 * 
 * Ademas he intentado hacer un mar con un layout que es mar, aunque no se usa, podria ser igualmente de tipo default,
 * lo importante es que es de tipo al layout  tierra, que es el que busca este script.
 * 
 * El mar genera una zona a su alrededor que no se deberia ser inclickable pero lo es. Es decir que alrededor del mar
 * por alguna razon no deja poner torres.
 * 
 * 
 * */


public class ClickAndTower : MonoBehaviour {
    public LayerMask layerTierra;
    public GameObject towerPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
            
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                if (hit.transform.gameObject.layer == 10/*layerTierra.value*/)
                {
                    

                    GameObject towerNew = Instantiate(towerPrefab, hit.point, Quaternion.identity);
                    towerNew.gameObject.transform.up = hit.normal;
                }
            }

        }

    }
}
