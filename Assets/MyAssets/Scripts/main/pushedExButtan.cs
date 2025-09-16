using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class pushedExButtan : MonoBehaviour
{
    [SerializeField] GameObject onemoreButtan;
    [SerializeField] GameObject returnMenu;
    [SerializeField] GameObject exitGame;
    [SerializeField] _Item _item;

    public void pushedOnemore()
    {
        _item.cancellTasks();
        SceneManager.LoadScene("main");
    }

    public void pushedMainmenu()
    {
        _item.cancellTasks();
        SceneManager.LoadScene("Start");
    }

    public void pushedEndgame()
    {
        Application.Quit();
    }

    public void lightningOnemoreButtan()
    {
        onemoreButtan.GetComponent<Image>().DOColor(Color.white, 0.3f);
    }

    public void lightningreturnMenu()
    {

    }

    public void lightningexitGame()
    {

    }




}
