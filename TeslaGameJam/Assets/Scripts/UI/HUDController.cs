using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HUDController : MonoBehaviour {
    
    public static HUDController instance;

    public bool startWithTuto = false;
    public RectTransform tuto;

    public MouseOrbitImproved orbitMous;
    public string ActualMission;
    public string NextMission;

    public RectTransform HUD, Win, Lose;

    public Sprite star_on, star_off;

    public Image star1, star2, star3;
    public Text Money_text1, Money_text2;

    public GameObject firstSelectedOnWin, firstSelectedOnLose;

    public void Awake()
    {
        instance = this;

    }
    public void Start()
    {
        if(startWithTuto){
            HUD.gameObject.SetActive(false);
            tuto.gameObject.SetActive(true);

            CameraInput.instance.stop = true;
            if (orbitMous) orbitMous.stop = true;
        }
    }

    public void Update()
    {
        if (startWithTuto)
        {
            if (InputController.instance.AButton > 0)
            {

                HUD.gameObject.SetActive(true);
                tuto.gameObject.SetActive(false);

                CameraInput.instance.stop = false;
                orbitMous.stop = false;
            }
        }
    }

    public void ActivateStars(int n, int money)
    {
        Money_text1.text = money + " $";
        Money_text2.text = money + " $";

        switch (n)
        {
            case 0:
                star1.sprite = star_off;
                star2.sprite = star_off;
                star3.sprite = star_off;
                break;
            case 1:
                star1.sprite = star_on;
                star2.sprite = star_off;
                star3.sprite = star_off;
                break;
            case 2:
                star1.sprite = star_on;
                star2.sprite = star_on;
                star3.sprite = star_off;
                break;
            case 3:
                star1.sprite = star_on;
                star2.sprite = star_on;
                star3.sprite = star_on;
                break;
            default:
                star1.sprite = star_off;
                star2.sprite = star_off;
                star3.sprite = star_off;
                break;
        }
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void GotoNextMission()
    {
        Debug.Log("sdagsafhgadfh");
        SceneManager.LoadScene(NextMission);

    }

    public void Restart()
    {
        SceneManager.LoadScene(ActualMission);
    }


    public void ShowWin(int nStar, int money)
    {
        CameraInput.instance.stop = true;
        if(orbitMous) orbitMous.stop = true;
        ActivateStars(nStar, money);
        Win.gameObject.SetActive(true);


        EventSystem.current.SetSelectedGameObject(firstSelectedOnWin);
    }


    public void ShowLose(int money)
    {
        CameraInput.instance.stop = true;
        if (orbitMous) orbitMous.stop = true;
        Lose.gameObject.SetActive(true);


        EventSystem.current.SetSelectedGameObject(firstSelectedOnLose);
    }
}
