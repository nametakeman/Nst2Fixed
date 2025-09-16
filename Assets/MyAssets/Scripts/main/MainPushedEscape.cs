using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainPushedEscape : MonoBehaviour
{
    bool MoveEscapeManuCheck;
    [SerializeField] GameObject EscapeManu;
    [SerializeField] GameObject Buttan1;
    [SerializeField] GameObject Buttan2;
    [SerializeField] GameObject Buttan3;
    Vector2 startPos;
    Vector2 startButtan1Pos;
    Vector2 startButtan2Pos;
    Vector2 startButtan3Pos;
    int ScreenWight;
    float StopZahyo;
    Vector2 ScreenWidhtVector;
    Vector2 ScreenWidhtWorld;
    // Start is called before the first frame update
    void Start()
    {
        ScreenWight = Screen.width;
        ScreenWidhtVector = new Vector2(ScreenWight, 0);
        ScreenWidhtWorld = Camera.main.ViewportToWorldPoint(ScreenWidhtVector);
        StopZahyo = ScreenWidhtWorld.x / 3;

        startPos = EscapeManu.transform.position;
        startButtan1Pos = Buttan1.transform.position;
        startButtan2Pos = Buttan2.transform.position;
        startButtan3Pos = Buttan3.transform.position;
        Debug.Log("StartPos‚Í" + startPos);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Vector2 nowPos = EscapeManu.transform.position;
            Vector2 nowButtan1Pos = Buttan1.transform.position;
            Vector2 nowButtan2Pos = Buttan2.transform.position;
            Vector2 nowButtan3Pos = Buttan3.transform.position;
            Debug.Log(startPos);
            Debug.Log(nowPos);

            if (Mathf.Floor(nowPos.x) > Mathf.Floor(startPos.x))
            {
                EscapeManu.transform.DOMove(new Vector2(Mathf.Floor(startPos.x), startPos.y), 0.9f);
                Buttan1.transform.DOMove(new Vector2(Mathf.Floor(startButtan1Pos.x), startButtan1Pos.y), 0.9f);
                Buttan2.transform.DOMove(new Vector2(Mathf.Floor(startButtan2Pos.x), startButtan2Pos.y), 0.9f);
                Buttan3.transform.DOMove(new Vector2(Mathf.Floor(startButtan3Pos.x), startButtan3Pos.y), 0.9f);

            }
            else if(Mathf.Floor(nowPos.x) == Mathf.Floor(startPos.x))
            {
                EscapeManu.transform.DOMove(new Vector2(Mathf.Floor(StopZahyo * 1.3f), nowPos.y), 0.9f);

                /*Buttan1.transform.DOMove(new Vector2(Mathf.Floor(nowButtan1Pos.x) + 1200, nowButtan1Pos.y), 0.9f);
                Buttan2.transform.DOMove(new Vector2(Mathf.Floor(nowButtan2Pos.x) + 1200, nowButtan2Pos.y), 0.9f);
                Buttan3.transform.DOMove(new Vector2(Mathf.Floor(nowButtan3Pos.x) + 1200, nowButtan3Pos.y), 0.9f);*/

                Buttan1.transform.DOMove(new Vector2(Mathf.Floor(StopZahyo / 2.7f), nowButtan1Pos.y), 0.9f);
                Buttan2.transform.DOMove(new Vector2(Mathf.Floor(StopZahyo / 2.7f), nowButtan2Pos.y), 0.9f);
                Buttan3.transform.DOMove(new Vector2(Mathf.Floor(StopZahyo / 2.7f), nowButtan3Pos.y), 0.9f);
            }

        }

    }
}
