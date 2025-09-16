using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class pushedExButtanOn : MonoBehaviour
{
    [SerializeField] GameObject returnMenu;
    [SerializeField] GameObject OnlineEnd;
    [SerializeField] GameObject exitGame;

    public void pushedOnemore()
    {
        SceneManager.LoadScene("main");
    }

    public void pushedMainmenu()
    {
        OnlineEnd.GetComponent<OnlineEnd>().disconectNet();
        SceneManager.LoadScene("Start");
    }

    public void pushedEndgame()
    {
        Application.Quit();
    }

    

    public void lightningreturnMenu()
    {

    }

    public void lightningexitGame()
    {

    }




}
