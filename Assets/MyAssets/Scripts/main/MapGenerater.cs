using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    //座標をキーにしてオブジェクトの名前を値にするdictionaryの作成
    public Dictionary<string, int> ObjectNameDic = new Dictionary<string, int>();

    [SerializeField] GameObject CubePrefab;
    [SerializeField] GameObject JumpRock1;
    [SerializeField] GameObject BreakShipFront;
    [SerializeField] GameObject BreakShipBack;
    [SerializeField] GameObject Anchor;
    [SerializeField] GameObject Boat;
    [SerializeField] GameObject Taru;
    public float MapLength;
    [SerializeField] GameObject Goal;
    [SerializeField] GameObject WaitObjects;
    [SerializeField] GameObject Banana;
    [SerializeField] GameObject Buri;
    [SerializeField] GameObject EarthModel;
    [SerializeField] GameObject Bed;
    [SerializeField] GameObject AlarmClock;
    [SerializeField] GameObject Bill;
    [SerializeField] GameObject ItemBox;
    [SerializeField] GameObject TEst;
    bool MapCheck = true;

    //オブジェクトのプレハブを格納するリスト
    public List<GameObject> ObjectsType = new List<GameObject>();
    //オブジェクトの生成数を格納するdictionary.<プレハブ,生成数>
    public Dictionary<GameObject,int> ObjectPeace = new Dictionary<GameObject, int>();
    //オブジェクトの範囲を収納するdictionary.<プレハブ,範囲>
    public Dictionary<GameObject, float> ObjectRangeD = new Dictionary<GameObject, float>();
    //生成したオブジェクトのプレハブを置いていくリスト
    public List<GameObject> CreatedObject = new List<GameObject>();
    //生成したオブジェクトの座標を置いていくリスト
    public List<Vector3> CreatedObjectPos = new List<Vector3>();

    //objectStatusを継承するクラスのリスト
    ObjectStatus[] _objectStatuses = new ObjectStatus[]
    {
        new JumpRock1()
    };

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの種類を追加。後でどっか他のファイルにまとめられないか検討
        ObjectsType.Add(JumpRock1);
        ObjectsType.Add(BreakShipFront);
        ObjectsType.Add(BreakShipBack);
        ObjectsType.Add(Boat);
        ObjectsType.Add(Anchor);
        ObjectsType.Add(Taru);
        ObjectsType.Add(Banana);
        ObjectsType.Add(Buri);
        ObjectsType.Add(EarthModel);
        ObjectsType.Add(Bed);
        ObjectsType.Add(AlarmClock);
        ObjectsType.Add(Bill);
        ObjectsType.Add(ItemBox);
        //オブジェクトの生成数を追加。後でどっか他のファイルにまとめられないか検討
        ObjectPeace.Add(JumpRock1, 40);
        ObjectPeace.Add(BreakShipFront, 4);
        ObjectPeace.Add(BreakShipBack, 4);
        ObjectPeace.Add(Boat, 5);
        ObjectPeace.Add(Anchor, 10);
        ObjectPeace.Add(Taru, 60);
        ObjectPeace.Add(Banana, 5);
        ObjectPeace.Add(EarthModel, 10);
        ObjectPeace.Add(Bed, 4);
        ObjectPeace.Add (AlarmClock, 30);
        ObjectPeace.Add(Bill, 20);
        ObjectPeace.Add(ItemBox, 0);

        
        int BuriRandom = UnityEngine.Random.Range(0, 101);
        int BuriNum = 0;
        if(BuriRandom == 0)
        {
            BuriNum = 1;
        }
        ObjectPeace.Add(Buri, BuriNum);
        //オブジェクトごとの範囲のdictionary.後でどっか他のファイルにまとめられないか検討
        ObjectRangeD.Add(JumpRock1, 45);
        ObjectRangeD.Add(BreakShipFront, 150);
        ObjectRangeD.Add(BreakShipBack, 300);
        ObjectRangeD.Add(Boat, 150);
        ObjectRangeD.Add(Anchor, 130);
        ObjectRangeD.Add(Taru, 15);
        ObjectRangeD.Add(Banana, 130);
        ObjectRangeD.Add(Buri, 130);
        ObjectRangeD.Add(EarthModel, 130);
        ObjectRangeD.Add(Bed, 300);
        ObjectRangeD.Add(AlarmClock, 20);
        ObjectRangeD.Add(Bill, 150);
        ObjectRangeD.Add(ItemBox, 50);
        //キューブ作成用のリストを作成
        List<float> CubePosZ = new List<float>();
        List<float> CubePosY = new List<float>();

        GameObject _distanceOb = GameObject.FindWithTag("distance");
        if (_distanceOb == null) MapLength = 14000;
        else MapLength = _distanceOb.GetComponent<OfflineDistance>()._islong;

        while (MapCheck)
        {
            //Cubesタグの付いたオブジェクトをすべて取得して配列に格納
            GameObject[] Cubes = GameObject.FindGameObjectsWithTag("Cubes");
            //配列からz座標を抜き出してlistに追加していく
            for (int yakiniku = Cubes.Length; yakiniku > 0; yakiniku--)
            {
                CubePosZ.Add(Cubes[yakiniku - 1].transform.position.z);
                CubePosY.Add(Cubes[yakiniku - 1].transform.position.y);
            }
            //リストの表示用
            /*
            for (int sukiyaki = 0; sukiyaki < CubePosZ.Count; sukiyaki++)
            {
                Debug.Log("【マップ系】" + CubePosZ[sukiyaki]);
            }
            */
            //リストの中身を昇順に並べる。
            CubePosZ.Sort();
            CubePosY.Sort();
            int CubePosZEnd = CubePosZ.Count - 1;
            int CubePosYEnd = CubePosY.Count - 1;
            if (MapLength > CubePosZ[CubePosZEnd])
            {
                Instantiate(CubePrefab, new Vector3(1750, CubePosY[CubePosYEnd] - 0.1f, CubePosZ[CubePosZEnd] + 3500), Quaternion.identity);
            }
            else if (MapLength <= CubePosZ[CubePosZEnd])
            {
                Instantiate(Goal,new Vector3(1750, 0, CubePosZ[CubePosZEnd] + 1050),Quaternion.identity);
                MapCheck = false;
            }
            
            CubePosZ.Clear();
            CubePosY.Clear();
        }
        ObjectsCreate();
        Debug.Log("マップの生成が終了しました");
        WaitObjects.SetActive(true);

    }

   
    private async UniTask ObjectsCreate2()
    {
        //1cubeを分割するための二次元配列を準備、
        bool[,] _mapDivisioner = new bool[350, 350];
        //配列の初期化処理
        for(int i = 349; i >= 0; i--)
        {
            for(int i2 = 349; i2 >= 0; i2--)
            {
                _mapDivisioner[i, i2] = false;
            }
        }

        //対象となるオブジェクトを選択
        for(int i = _objectStatuses.Length - 1; i >= 0; i--)
        {
            ObjectStatus _targetedOb = _objectStatuses[i];
            //二次元配列の座標をいれるVector2リスト型
            List<Vector2> _registerPoses = new List<Vector2>();

            //mapDivisionerの0,0を始点として規則的に獲得可能な領域を調べる。発見後は始点の座標を保存しておく。オブジェクトの中心に直さない！
            for(int n = 0; n < 350;  n++)
            {
                for(int n2 = 0 ; n2 < 350; n2++)
                {
                    bool _result = await _searchRange(i,_mapDivisioner,new Vector2(n,n2));
                    if (!_result)
                    {
                        continue;
                    }
                    _registerPoses.Add(new Vector2(n,n2));
                }
            }

            Debug.Log("設置可能始点個数" + _registerPoses.Count());

            List<int> _registerRandomInt = new List<int>();
            List<Vector2> _dsidedPoses = new List<Vector2>();
            while (_dsidedPoses.Count <= _objectStatuses[i]._NumOfPiece)
            {
                //生成するぶんランダムでぶんまわす
                int _randomInt = UnityEngine.Random.Range(0, _registerPoses.Count);
                //もし既出ならそこで処理を終了させる。
                if (_registerRandomInt.Contains(_randomInt))
                {
                    continue;
                }


                //ちゃんと生成場所が確保できるかを確認する
                bool _result = await _searchRange(i, _mapDivisioner, _registerPoses[_randomInt]);
                _registerRandomInt.Add(_randomInt);
                if (!_result)
                {
                    continue;
                }

                //生成する
                Vector2 _fixedPos = _fixPos(i,_registerPoses[_randomInt], new Vector3(1750,0,1750));
                Instantiate(TEst, new Vector3(_fixedPos.x, 10, _fixedPos.y), Quaternion.identity);
            }
        }
    }

    private async UniTask<bool> _searchRange(int i, bool[,] _mapDivisioner, Vector2 _startPoint)
    {
        //始点はfalse
        for (int n3 = 0; n3 < _objectStatuses[i]._length * 2; n3++)
        {
            for (int n4 = 0; n4 < _objectStatuses[i]._width * 2; n4++)
            {

                if (_mapDivisioner[(int)_startPoint.x + n3,(int)_startPoint.y + n4])
                {
                    //ここはtrueになった時点で始点(n,n2)をずらす
                    return false;
                }
            }
        }
        return true;
    }

    //worldPosは生成するcubeの中心座標を渡す。
    private Vector2 _fixPos(int _objectNum, Vector2 _fixedPos, Vector3 _worldPos)
    {
        //ここで飛んでくるのは始点の座標
        //二次元配列の0,0(の中でも一番左下)にあたる基準のワールド座標を出す
        Vector2 _firstWdPos = new Vector2(0, _worldPos.z - 1750);//これcubeの大きさ変えるとバグるの注意！

        Vector2 _startWdPos = new Vector2(_firstWdPos.x + _fixedPos.x * 10, _firstWdPos.y + _fixedPos.y * 10);
        return new Vector2(_startWdPos.x + _objectStatuses[_objectNum]._width * 10,_startWdPos.y + _objectStatuses[_objectNum]._length * 10);
    }

    //マップ内のオブジェクトを作成するメソッド
    private void ObjectsCreate()
    {
        //Cubesタグの付いたオブジェクトをすべて取得して配列に格納
        GameObject[] Cubes = GameObject.FindGameObjectsWithTag("Cubes");

        //生成したオブジェクトのプレハブを置いていくリスト
        List<GameObject> CreatedObject = new List<GameObject>();
        //生成したオブジェクトの座標を置いていくリスト
        List<Vector3> CreatedObjectPos = new List<Vector3>();
        
        bool OutRangeCheck = false;

        //すべてのキューブに繰り返し生成する
        for (int udon = Cubes.Length; udon > 0; udon--)
        {

            for (int onigiri = ObjectsType.Count; onigiri > 0; onigiri--)
            {
                //ループの回数から今回生成するプレハブを取得してくる。
                GameObject CreateObjectPrefab = ObjectsType[onigiri - 1];
                //オブジェクトの生成数をdictionaryから取得してくる。
                int CreateObjectPeace = ObjectPeace[CreateObjectPrefab];

                //odenはアイテムの生成量。
                for (int oden = 1; oden <= CreateObjectPeace; oden++)
                {
                    //ここではテストのためにCubeを一個目にする。あとで変更！
                    Vector3 CubesO = Cubes[udon - 1].transform.position;
                    //オブジェクトを生成する場所をとりあえずランダムで生成
                    Vector3 ObjectPos = new Vector3();
                    ObjectPos.x = UnityEngine.Random.Range(CubesO.x - 665, CubesO.x + 666);
                    ObjectPos.z = UnityEngine.Random.Range(CubesO.z - 1750, CubesO.z + 1751);
                    ObjectPos.y = CreateObjectPrefab.transform.position.y;

                    //もし初めて生成するオブジェクトならそのまま設置
                    if (CreatedObject.Count == 0)
                    {
                        CreatedObject.Add(CreateObjectPrefab);
                        CreatedObjectPos.Add(ObjectPos);
                        Instantiate(CreateObjectPrefab, ObjectPos, Quaternion.Euler(CreateObjectPrefab.transform.localEulerAngles.x, CreateObjectPrefab.transform.localEulerAngles.y, CreateObjectPrefab.transform.localEulerAngles.z));
                    }
                    else
                    {
                        OutRangeCheck = OutRange(ObjectPos, ObjectRangeD[JumpRock1]);
                        if (OutRangeCheck)
                        {
                            CreatedObject.Add(CreateObjectPrefab);
                            CreatedObjectPos.Add(ObjectPos);
                            Instantiate(CreateObjectPrefab, ObjectPos, Quaternion.Euler(CreateObjectPrefab.transform.localEulerAngles.x, CreateObjectPrefab.transform.localEulerAngles.y, CreateObjectPrefab.transform.localEulerAngles.z));
                        }
                        else return;
                    }
                }
            }
            //1キューブ分終わったらリスト系の中身をリセットする
            CreatedObject.Clear();
            CreatedObjectPos.Clear();
        }

    }

    //任意の範囲内かどうかを返す。範囲がかぶっていたらfalse、被っていなかったらtrueを返す。
    private bool OutRange(Vector3 OPosComparison, float RangeComparison)
    {
        for (int SearchObject = CreatedObject.Count; SearchObject > 0; SearchObject--)
        {
            //以下がdictionaryから参照していく用の配列
            float[] xObjectPosA = new float[2];
            float[] zObjectPosA = new float[2];
            
            //それぞれループ毎で参照しているdictionaryの座標を取得してリストに格納
            xObjectPosA[0] = CreatedObjectPos[SearchObject - 1].x; zObjectPosA[2] = CreatedObjectPos[SearchObject - 1].z;
            GameObject ObjectName = CreatedObject[SearchObject - 1];
            
            //ObjectPos[x座標のプラス方向の範囲,x座標のマイナス方向の範囲,z座標のプラス方向の範囲,z座標のマイナス方向の範囲]
            xObjectPosA[0] += ObjectRangeD[ObjectName]; xObjectPosA[1] = xObjectPosA[0] - ObjectRangeD[ObjectName];
            zObjectPosA[0] += ObjectRangeD[ObjectName]; zObjectPosA[1] = zObjectPosA[0] - ObjectRangeD[ObjectName];
            //昇順にソートする
            Array.Sort(xObjectPosA);
            Array.Sort (zObjectPosA);
            
            //以下が比較されるオブジェクトの配列
            float[] xObjectPosB = new float[2];
            float[] zObjectPosB = new float[2];
            xObjectPosB[0] = OPosComparison.x + RangeComparison; xObjectPosB[1] = OPosComparison.x - RangeComparison;
            zObjectPosB[0] = OPosComparison.z + RangeComparison; zObjectPosB[1] = OPosComparison.z - RangeComparison;
            //昇順にソート
            Array.Sort(xObjectPosB);
            Array.Sort (zObjectPosB);

            //まずx軸から範囲内に入ってる時のみfalseを返す
            if ((xObjectPosB[0] <= xObjectPosA[0] && xObjectPosA[0] <= xObjectPosB[1]) || (xObjectPosB[0] <= xObjectPosA[1] && xObjectPosA[1] <= xObjectPosB[1] ))
            {

                return false;

            }else if ((zObjectPosB[0] <= zObjectPosA[0] && zObjectPosA[0] <= zObjectPosB[1]) || (zObjectPosB[0] <= zObjectPosA[1] && zObjectPosA[1] <= zObjectPosB[1]))
            {

                return false;

            }
        }
        return true;
    }
    


    void Update()
    {
        
    }
}
