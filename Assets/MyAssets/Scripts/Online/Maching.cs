using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Maching : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        //PhotonServerSettingsの設定内容を使ってマスターサーバーに接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    //マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //"Room"という名前のルームに参加する(ルームがなければ作成して参加)
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    //ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("mainOn");
        }
    }

    //ホストの時もう一人のプレイヤーが入室してきたらシーンを偏移させる
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("mainOn");
        }
    }





    //ロードシーンしてから相手オブジェクトを取得してから開始にする。
}
