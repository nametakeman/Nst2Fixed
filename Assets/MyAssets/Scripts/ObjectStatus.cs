using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ObjectStatus
{
    //オブジェクトの名前プレハブの名前を設定すること
    string _objectName { get; set; }

    //生成個数(後でランダムにする)
    int _NumOfPiece { get; set; }

    //マップ分割後の幅と奥行(width = x,length = y)
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
