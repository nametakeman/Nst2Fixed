using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownOn : MonoBehaviour
{
    public bool StartCheck;
    [SerializeField] Text CountDownText;
    GameObject TunaObject;
    [SerializeField] GameObject Timer;
    // Start is called before the first frame update
    void Start()
    {
        TunaObject = GameObject.Find("Tuna");
        Debug.Log("Goalオブジェクトを取得");
        Timer.SetActive(false);
        StartCoroutine("CountDownCoroutine");
    }

    private void FixedUpdate()
    {
        //ゴールしたかのbool値を取得
        bool GoalCheck = TunaObject.GetComponent<TunaMoveOn>().GoalCheck;
        if (GoalCheck)
        {
            StartCheck = false;
        }
    }

    IEnumerator CountDownCoroutine()
    {
        Debug.Log("コルーチンの開始");
        CountDownText.text = "3";
        yield return new WaitForSeconds(1);
        CountDownText.text = "2";
        yield return new WaitForSeconds(1);
        CountDownText.text = "1";
        yield return new WaitForSeconds(1);
        CountDownText.text = "START!!";
        StartCheck = true;
        Timer.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CountDownText.text = "";
    }
}
