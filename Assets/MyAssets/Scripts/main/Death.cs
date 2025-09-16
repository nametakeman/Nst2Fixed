using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Death : MonoBehaviour
{
    [SerializeField] Text _siTx;
    [SerializeField] Text _deathTx;
    [SerializeField] GameObject _backBPanel;
    [SerializeField] GameObject _spaceGuidOb;
    [SerializeField] GameObject _itemOb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask deathFlow()
    {
        var cts = new CancellationTokenSource();
        CancellationToken _token = cts.Token;

        await deathUI(_token);
        //スペースが押されるまで処理待ち
        await UniTask.WaitUntil(() => Input.GetKey(KeyCode.Space), cancellationToken: _token);
        cts.Cancel();
        _itemOb.GetComponent<_Item>().cancellTasks();
        SceneManager.LoadScene("DeathRoom");
    }

    async UniTask deathUI(CancellationToken token)
    {
        await _backBPanel.GetComponent<Image>().DOFade(endValue: 0.9f, duration: 1.3f);
        _siTx.DOFade(endValue: 1f, duration: 1f);
        _deathTx.DOFade(endValue: 1f, duration: 1f);

        await UniTask.Delay(1000, cancellationToken: token);
        _spaceGuidOb.SetActive(true);
        Debug.Log("UI処理の終了");
    }
}
