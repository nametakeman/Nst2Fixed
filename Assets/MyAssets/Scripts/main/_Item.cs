using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class _Item : MonoBehaviour
{
    [SerializeField] Sprite MagicalKinokoImage;
    [SerializeField] Sprite DownForceImage;
    [SerializeField] Sprite HoutyouImg;
    [SerializeField] Sprite InvisiblerImg;
    [SerializeField] Sprite TunakanImg;
    [SerializeField] Sprite WingImg;
    [SerializeField] GameObject tunaOb;

    [SerializeField] GameObject _emptyItemField;
    IItemMe[] itemClasses;
    public GameObject[] _seas;
    int holdItemNum = -1;
    CancellationTokenSource _cancellationTokenSource;
    CancellationToken _cancellationToken;

    bool _downForceFlg = false;
    bool _upForceFlg = false;
    // Start is called before the first frame update
    void Start()
    {
        itemClasses = new IItemMe[]
        {
            new MagicalKinoko(),new DownForcer(),new Houtyou(),new Tunakan(),new Wing()
        };
        itemClasses[0].itemImage = MagicalKinokoImage;
        itemClasses[1].itemImage = DownForceImage;
        itemClasses[2].itemImage = HoutyouImg;
        itemClasses[3].itemImage = TunakanImg;
        itemClasses[4].itemImage = WingImg;

        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && holdItemNum != -1)
        {
            itemClasses[holdItemNum].mainProcess(tunaOb,this.gameObject,_cancellationToken);
            _emptyItemField.GetComponent<Image>().sprite = null;
            holdItemNum = -1;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _downForceFlg && tunaOb.transform.position.y >= 0.9f)
        {
            Vector3 DownForce = new Vector3(0, -10, 0);
            tunaOb.GetComponent<Rigidbody>().AddForce(DownForce);
        }

        if(Input.GetKey(KeyCode.Space) && _upForceFlg)
        {
            Vector3 DownForce = new Vector3(0, 5, 0);
            tunaOb.GetComponent<Rigidbody>().AddForce(DownForce);
        }

    }
    public void cancellTasks()
    {
        _cancellationTokenSource?.Cancel();
    }

    public void setObjects()
    {
        _seas = GameObject.FindGameObjectsWithTag("Sea");
    }
    
    public async UniTask _flagCtrler(string _flagName ,CancellationToken _token)
    {
        if(_flagName == "_downForceFlg")
        {
            _downForceFlg = true;
            await UniTask.Delay(8000, cancellationToken: _token);
            _downForceFlg = false;
        }
        else if(_flagName == "_upForceFlg")
        {
            _upForceFlg = true;
            await UniTask.Delay(5000, cancellationToken: _token);
            _downForceFlg = false;
        }
    }

    public async UniTask lotteryItem()
    {
        holdItemNum = UnityEngine.Random.Range(0,itemClasses.Length);
        //ここにルーレット(アイテム取得のディレイを入れる)
        _emptyItemField.GetComponent<Image>().sprite = itemClasses[holdItemNum].itemImage;
    }
}

interface IItemMe
{
    Sprite itemImage { get; set; }
    void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token);
}



class MagicalKinoko : IItemMe
{
    public Sprite itemImage
    {
        get; set;
    }
    public void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        Vector3 addSpeed = new Vector3(0, 0, 100);
        Tuna.GetComponent<Rigidbody>().AddForce(addSpeed, ForceMode.Impulse);
    }
}
class DownForcer : IItemMe
{
    float _timeLog = 0;
    bool _flag = false;
    GameObject _tuna;
    public Sprite itemImage
    {
        get; set;
    }
    public void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        _thisOb.GetComponent<_Item>()._flagCtrler("_downForceFlg",_token);
    }
}

class Houtyou : IItemMe
{
    public Sprite itemImage
    {
        get; set;
    }
    public void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        _asyncProcess(Tuna,_token); 
    }

    public async UniTask _asyncProcess(GameObject Tuna, CancellationToken _token)
    {
        Tuna.transform.Find("TunaModel").gameObject.SetActive(false);
        Tuna.transform.Find("NigiriModel").gameObject.SetActive(true);

        await UniTask.Delay(10000, cancellationToken: _token);
        Tuna.transform.Find("TunaModel").gameObject.SetActive(true);
        Tuna.transform.Find("NigiriModel").gameObject.SetActive(false);
    }
}

class Tunakan : IItemMe
{
    public Sprite itemImage
    {
        get; set;
    }
    public void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        asyncProcess(Tuna, _thisOb, _token);
    }

    async UniTask asyncProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        GameObject[] _seas = _thisOb.GetComponent<_Item>()._seas;
        var sequence = DOTween.Sequence();
        foreach(GameObject g in _seas)
        {
            sequence.Join(g.GetComponent<Renderer>().material.DOFade(endValue: 0.4f, duration: 1));
        }
        sequence.Play();

        await UniTask.Delay(10000, cancellationToken: _token);

        var sequence2 = DOTween.Sequence();
        foreach (GameObject g in _seas)
        {
            sequence2.Join(g.GetComponent<Renderer>().material.DOFade(endValue: 1, duration: 1));
        }
        sequence2.Play();
    }

}

class Wing : IItemMe
{
    public Sprite itemImage
    {
        get; set;
    }
    public void mainProcess(GameObject Tuna, GameObject _thisOb, CancellationToken _token)
    {
        _thisOb.GetComponent<_Item>()._flagCtrler("_upForceFlg",_token);
    }
}
