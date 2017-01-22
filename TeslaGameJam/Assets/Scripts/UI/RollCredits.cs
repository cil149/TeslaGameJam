using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCredits : MonoBehaviour {
    public RectTransform creditsText;
    public bool uping = false;

    public float speed = 2f;
	// Use this for initialization
	void OnEnable () {
        creditsText.anchoredPosition = Vector2.zero;

        StartCoroutine(waitFewSeconds());

        //uping = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (uping)
        {
            creditsText.anchoredPosition = creditsText.anchoredPosition + Vector2.up * Time.deltaTime * speed;
            if (creditsText.anchoredPosition.y >= 5255)
            {
                uping = false;
                Debug.Log("Uping a Falso: " + uping);
                //enabled = false;
            }
        }
	}

    IEnumerator waitFewSeconds()
    {
        yield return new WaitForSeconds(2f);
        uping = true;
        yield return new WaitForSeconds(2f);
    }
}
