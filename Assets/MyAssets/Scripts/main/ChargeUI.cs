using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ChargeUI : MonoBehaviour
{
    [SerializeField] CountDown _countDown;
    [SerializeField] TunaMove _tuneMove;
    [SerializeField] GameObject _Tuna;
    [SerializeField] _Item _item;
    bool RBottanState = false;
    bool TBottanState = false;
    int _holdTime = 100; //“ü—Í‚É•K—v‚È’·‰Ÿ‚µŽžŠÔ(ƒ~ƒŠ•b)
    float _rFillAmount = 0;
    float _tFillAmount = 0;


    [SerializeField] Image _chargeImageR;
    [SerializeField] Image _chargeImageT;
    // Start is called before the first frame update
    void Start()
    {
        _chargeImageR.fillAmount = 0;
        _chargeImageT.fillAmount = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !RBottanState && _countDown.StartCheck)
        {
            RBottanState = true;
            waitR();
        }

        if (Input.GetKeyDown(KeyCode.T) && !TBottanState && _countDown.StartCheck)
        {
            TBottanState = true;
            waitT();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RBottanState = false;
            _rFillAmount = 0;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            TBottanState = false;
            _tFillAmount = 0;
        }
    }

    async UniTask waitR()
    {
        for(int i = _holdTime; i >= 0; i--)
        {
            _rFillAmount += 1f / _holdTime;
            _chargeImageR.fillAmount = _rFillAmount;
            if (!RBottanState)
            {
                return;
            }
            await UniTask.Delay(1);
        }
        RBottanState = false;
        _rFillAmount = 0;
        _item.cancellTasks();
        SceneManager.LoadScene("main");
    }

    async UniTask waitT()
    {
        for (int i = _holdTime; i >= 0; i--)
        {
            _tFillAmount += 1f / _holdTime;
            _chargeImageT.fillAmount = _tFillAmount;
            if (!TBottanState)
            {
                return;
            }
            await UniTask.Delay(1);
        }
        _Tuna.transform.position = new Vector3(_Tuna.transform.position.x,_Tuna.transform.transform.position.y,_Tuna.transform.position.z - 70);
        TBottanState = false;
        _tFillAmount = 0;
    }
}
