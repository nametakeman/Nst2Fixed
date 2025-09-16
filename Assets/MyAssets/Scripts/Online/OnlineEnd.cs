using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineEnd : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject TunaMoveOn;
    private static readonly ExitGames.Client.Photon.Hashtable propsToSet = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        propsToSet["end"] = "false";
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public void OnlineEndMethod()
    {
        //Photon�̃J�X�^���v���p�e�B���Z�b�g
        propsToSet["end"] = "true";
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public void disconectNet()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if ((string)targetPlayer.CustomProperties["end"] == "true" && targetPlayer.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {

            TunaMoveOn.GetComponent<TunaMoveOn>().goalMethod("lose");

        }
    }

    //���肪���[���ؒf�������̃R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        disconectNet();
        SceneManager.LoadScene("Start");
    }




}
