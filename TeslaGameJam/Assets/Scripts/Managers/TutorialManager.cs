using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public List<String> hints;
    public List<GameObject> connectionGoal;
    public Text text;
    public Button button;
    int actualGoal;
    private

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (connectionGoal[actualGoal]) {
            Tower t = connectionGoal[actualGoal].GetComponent<Tower>();
            City c = connectionGoal[actualGoal].GetComponent<City>();

            bool goalReached = false;
            if (t)
            {
                goalReached = t.isOn;
            } else if (c)
            {
                goalReached = c.isActive;
            }
        }
        
    }

    void next()
    {
        actualGoal++;
        text.text = hints[actualGoal];
        if (connectionGoal[actualGoal])
        {
            button.enabled = false;
        }
        else
        {
            button.enabled=true;
        }
    }
}
