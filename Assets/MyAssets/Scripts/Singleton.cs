using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトン用の基底クラス
/// このクラスは汎用性を持つ基底クラスとして作られ、複数のシングルトンが必要な場合でもこのクラスを汎用的に使用する
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //シングルトンのインスタンス
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //シーン内からゲームオブジェクトを検索
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    //インスタンスがシーン内に存在しない場合、新しく生成
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    
                    //シーンが切り替わっても破棄されないようにする
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
    
    //重複していないかを確認
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
