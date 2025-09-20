using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 入力を検知してActionを実行するクラス
/// </summary>
public class InputAction　: Singleton<InputAction>
{
    private UnityAction _forwardMove;
    private UnityAction _backwardMove;

    //前後が-1～1でX軸、左右が-1～1でY軸
    private Vector2 _moveInputValue;
    //getだけパブリックで他クラスから値を取得できるように、setはプライベート
    public Vector2 MoveInputValue { get => _moveInputValue; private set => _moveInputValue = value; }
    

    private void Update()
    {
        
    }
}
