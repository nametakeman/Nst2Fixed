using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float StartTime;
    public float NowTime;
    [SerializeField] TunaMove _tunaMove;
    [SerializeField] Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        NowTime = Time.time - StartTime;
        bool _goalCheck = _tunaMove.GoalCheck;

        if(!_goalCheck)
        {
            float TimerM = Mathf.Floor(NowTime / 60);
            float TimerS = Mathf.Floor(NowTime % 60);
            TimerText.text = TimerM.ToString("00") + ":" + TimerS.ToString("00");
        }

    }
}
