using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string _createGUID = System.Guid.NewGuid().ToString();
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = _createGUID,
                CreateAccount = true,
            }
            , result =>
            {
                Debug.Log("���O�C������!");
                SubmitScore(700);
                SetName();
            }
            ,error => Debug.Log("���O�C�����s"));
    }

    public void SetName()
    {
            PlayFabClientAPI.UpdateUserTitleDisplayName(
        new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = "janedo"
        },
        result =>
        {
            Debug.Log("���O�̕ύX�ɐ������܂���");
        },
        error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore
                }
            }
        }, result =>
        {
            Debug.Log($"�X�R�A{playerScore}���M�����I");
        }, error =>
        { Debug.Log(error.GenerateErrorReport()); }
        );

    }

    string CreateNewId()
    {
        return System.Guid.NewGuid().ToString();
    }
}
