using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ObjectStatus
{
    //�I�u�W�F�N�g�̖��O�v���n�u�̖��O��ݒ肷�邱��
    string _objectName { get; set; }

    //������(��Ń����_���ɂ���)
    int _NumOfPiece { get; set; }

    //�}�b�v������̕��Ɖ��s(width = x,length = y)
    int _length { get; set; }
    int _width { get; set; }
}

class JumpRock1 :  MonoBehaviour ,ObjectStatus
{
    public string _objectName { get; set; } = "JumpRock1";

    public int _NumOfPiece { get; set; } = 10;
    public int _length { get; set; } = 2;
    public int _width { get; set; } = 2;
}
