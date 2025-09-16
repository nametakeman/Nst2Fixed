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
        //PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�ɐڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    //�}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //"Room"�Ƃ������O�̃��[���ɎQ������(���[�����Ȃ���΍쐬���ĎQ��)
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    //�Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("mainOn");
        }
    }

    //�z�X�g�̎�������l�̃v���C���[���������Ă�����V�[����Έڂ�����
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("mainOn");
        }
    }





    //���[�h�V�[�����Ă��瑊��I�u�W�F�N�g���擾���Ă���J�n�ɂ���B
}
