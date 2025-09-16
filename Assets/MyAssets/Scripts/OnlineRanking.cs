using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class OnlineRanking : MonoBehaviour
{
    [SerializeField] GameObject[] _rankingTextOb = new GameObject[5];
    [SerializeField] GameObject _informationOb;
    [SerializeField] GameObject _informationTx;
    [SerializeField] GameObject _enterNameOb;
    [SerializeField] InputField _enterNameField;

    bool _isOnline = false;
    bool _errorFlag = false;

    string _accountName = null;
    int _localScore = 30000000;
    List<string> _name = new List<string>();
    List<float> _score = new List<float>();
    CancellationTokenSource _sceneTaskCancell = new CancellationTokenSource();

    public void _taskCancell()
    {
        _sceneTaskCancell.Cancel();
        GameObject _scoreObject = null;
        _scoreObject = GameObject.FindWithTag("score");
        if (_scoreObject != null) Destroy(_scoreObject);

        SceneManager.LoadScene("Start");
    }

    // Start is called before the first frame update
    async void Start()
    {
        await connectionNetwork(_sceneTaskCancell.Token);
        if (_errorFlag) return;

        GameObject _scoreObject = null;
        _scoreObject = GameObject.FindWithTag("score");
        if (_scoreObject != null)
        {
            //playfab��float�ɑΉ����Ă��Ȃ�����100�{����int�^�ɐ��`�A��Ŗ߂��̖Y��Ȃ�
            _localScore =30000000 - (int)Mathf.Floor(_scoreObject.GetComponent<Score>()._score * 100);

            Destroy(_scoreObject);
            _informationTx.GetComponent<Text>().text = "���O����͂��Ă��������B";
            _enterNameOb.SetActive(true);
        }
        else
        {
            await roadRanking(_sceneTaskCancell.Token);
            if (_errorFlag) return;
            await startProcess2();
            if (_errorFlag) return;
        }
    }

    async UniTask startProcess2()
    {
        await _displayScore();
        if (_errorFlag) return;
        _informationOb.SetActive(false);
    }

    public void _exitScene()
    {
        _sceneTaskCancell?.Cancel();
        SceneManager.LoadScene("Start");
    }

    async UniTask connectionNetwork(CancellationToken _token)
    {
        string _createGUID = System.Guid.NewGuid().ToString();
        //playfab�Ƀ��O�C��
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = _createGUID,
                CreateAccount = true,
            }
            , result =>
            {
                _isOnline = true;
                _informationTx.GetComponent<Text>().text = "�C���^�[�l�b�g�ڑ��������܂���";
            }
            , error =>
            {
                _isOnline = false;
                _errorFlag = true;
                _informationTx.GetComponent<Text>().text = "�C���^�[�l�b�g�ڑ����s���܂���";
            }
            );

        await UniTask.WaitForSeconds(1, cancellationToken: _token);
    }

    async UniTask roadRanking(CancellationToken _token)
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "LongHighScore"
        }, result =>
        {
            int n = 0;
            foreach (var item in result.Leaderboard)
            {
                _name.Add(item.DisplayName);
                _score.Add(30000000 - (item.StatValue));


                
                n++;
                if (n >= 5)
                {
                    Debug.Log("end roop");
                    break;
                }
                
            }
            _informationTx.GetComponent<Text>().text = "�����L���O�̎擾�ɐ������܂���";
        }, error =>
        {
            _errorFlag = true;
            _informationTx.GetComponent<Text>().text = "�����L���O�̎擾�Ɏ��s���܂����B";
        }
        ) ;
        await UniTask.WaitForSeconds(1, cancellationToken: _token);

        

    }

    public void changeName()
    {
        if(_enterNameField.text == "")
        {
            _accountName = "Guest Kinoko";
        }else _accountName = _enterNameField.text;

        _asyncChangeName();
    }

    async UniTask _asyncChangeName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = _accountName
            },
            result =>
            {
                _informationTx.GetComponent<Text>().text = "�j�b�N�l�[����ݒ肵�܂����B";
            },
            error =>
            {
                _errorFlag = true;
                _informationTx.GetComponent<Text>().text = "�j�b�N�l�[���ݒ莸�s";
            }
            );
        await UniTask.Delay(1000, cancellationToken:_sceneTaskCancell.Token);
        _enterNameOb.SetActive( false );

        await _submitScore(_localScore);
        if (_errorFlag) return;
        await UniTask.Delay(1000, cancellationToken: _sceneTaskCancell.Token);

        await roadRanking(_sceneTaskCancell.Token);
        if (_errorFlag) return;

        await startProcess2();
    }

    async UniTask _submitScore(int _score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "LongHighScore",
                    Value = _score
                }
            }
        },
        result =>
        {
            _informationTx.GetComponent<Text>().text = "�X�R�A�o�^����";
        }, error =>
        {
            _errorFlag = true;
            _informationTx.GetComponent<Text>().text = "�X�R�A�o�^���s";
        }
        );
    }

    async UniTask _displayScore()
    {
        for(int i = 0; i < _score.Count; i++)
        {
            int _scoreM = (int)((_score[i] / 100) / 60);
            float _scoreS = ((_score[i] / 100) % 60);
            _rankingTextOb[i].GetComponent<Text>().text = _scoreM.ToString("00") + ":" + _scoreS.ToString("00") + "/" + _name[i];
        }
    }


}
