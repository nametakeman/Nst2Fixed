using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceBottan : MonoBehaviour
{
    [SerializeField] GameObject OfflineDistance;
    [SerializeField] GameObject InformationPanel;
    [SerializeField] int _shortDistance;
    [SerializeField] int _longDistance;

    public void shortBottan()
    {
        OfflineDistance.GetComponent<OfflineDistance>()._islong = _shortDistance;
        SceneManager.LoadScene("main");
    }

    public void longBottan()
    {
        OfflineDistance.GetComponent<OfflineDistance>()._islong = _longDistance;
        SceneManager.LoadScene("main");
    }

    public void exitBottan()
    {
        InformationPanel.SetActive(false);
    }
}
