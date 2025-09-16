using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.Rendering;

public class PosSharing : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Tuna;
    [SerializeField] GameObject enemyMarker;
    [SerializeField] GameObject myMarker;
    [SerializeField] GameObject MapGenerater;
    [SerializeField] float graphEndPos;

    float startTunaPos;
    float nowTunaPos;
    float tunaPers;
    float enemyPers;
    float graphStartPos;
    float mapLength;
    float mapGraphLength;
    float bunnoiti;
    private const string ScoreKey = "ZPos";

    private static readonly ExitGames.Client.Photon.Hashtable propsToSet = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        mapLength = MapGenerater.GetComponent<MapGenerater>().MapLength + 3500 - 700;
        graphStartPos = myMarker.transform.position.y;
        mapGraphLength = graphEndPos - graphStartPos;
        bunnoiti = mapGraphLength / (mapLength - 100);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ここに同期処理を書く
        //まずプレイヤーのx座標を取得してくる
        float myZPos = Tuna.transform.position.z;
        tunaPers = myZPos / mapLength;
        myMarker.transform.position = new Vector3(myMarker.transform.position.x,graphStartPos + mapGraphLength * tunaPers);

        //Photonのカスタムプロパティにセット
        propsToSet[ScoreKey] = myZPos;
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
        propsToSet.Clear();


    }

    //相手のカスタムプロパティが変更された時に呼ばれるコールバック
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            var enemyZPos = targetPlayer.CustomProperties[ScoreKey];
            enemyPers = (float)enemyZPos / mapLength;
            enemyMarker.transform.position = new Vector3(enemyMarker.transform.position.x, graphStartPos + mapGraphLength * enemyPers);

        }
    }
}
