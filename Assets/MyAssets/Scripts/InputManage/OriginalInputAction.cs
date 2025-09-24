using System;
using UnityEngine;

/// <summary>
/// 入力を検知してActionを実行するクラス
/// </summary>
public class OriginalInputAction　: Singleton<OriginalInputAction>
{
    //移動入力処理を実行するためのデリゲート
    public Action<Vector2> MoveFunc;

    private KeyLibrary _keyLibrary;
    
    private void Awake()
    {
        _keyLibrary = new KeyLibrary();
    }

    private void Update()
    {
        //前後が-1～1でX軸、左右が-1～1でY軸
        Vector2 inputValue = Vector2.zero;
        //前進の検知
        foreach(string str in _keyLibrary.UpKeys)
        {
            if (Input.GetKey(str))
            {
                inputValue.x += 1;
                break;
            }
        }
        
        //後退の検知
        foreach (string str in _keyLibrary.BackKeys)
        {
            if (Input.GetKey(str))
            {
                inputValue.x -= 1;
                break;
            }
        }
        
        //右移動の検知
        foreach (string str in _keyLibrary.RightKeys)
        {
            if (Input.GetKey(str))
            {
                inputValue.y += 1;
                break;
            }
        }
        
        //左移動の検知
        foreach (string str in _keyLibrary.LeftKeys)
        {
            if (Input.GetKey(str))
            {
                inputValue.y -= 1;
                break;
            }
        }
        
        Debug.Log(inputValue);
        //デリゲートのヌルチェック、実行
        if (MoveFunc != null) MoveFunc(inputValue);

    }
}
