using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    string FolderName = "correctDatas";
    string _fileName = "localRanking.txt";
    string _fileName2 = "localRankingLong.txt";

    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }
        if(!File.Exists(FolderName + "/" + _fileName))
        {
            File.Create(FolderName + "/" + _fileName);
        }
        if(!File.Exists(FolderName + "/" + _fileName2))
        {
            File.Create(FolderName + "/" + _fileName2);
        }

        _setLocalRanking(new float[] {3000000,3000000, 3000000, 3000000, 3000000, 3000000, 3000000, 3000000, 3000000, 3000000 }, _fileName);
        _setLocalRanking(new float[] {3000000, 3000000,3000000, 3000000, 3000000, 3000000, 3000000, 3000000, 3000000, 3000000 }, _fileName2);
    }

    public float[] _getLocalRanking(string _fileType)
    {
        FileInfo fi = new FileInfo(FolderName + "/" + _fileType);
        string _readTxt = null;
        try
        {
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                _readTxt = sr.ReadToEnd();
            }
        } catch (Exception e)
        {
            Debug.Log(e);
        }
        
        if(_readTxt == "")
        {
            return new float[0];
        }

        //ランキングをコンマで区切って配列に代入（float化も忘れずに！)
        string[] _splitedTxt = _readTxt.Split(',');
        List<float> _rankingList = new List<float>();
        for(int i = 0; i < _splitedTxt.Length; i++)
        {
            _rankingList.Add(float.Parse(_splitedTxt[i]));
        }

        return _rankingList.ToArray();
    }

    public void _setLocalRanking(float[] _ranking, string _filePath)
    {
        string _setData = null;
        //float型の配列を結合
        for(int i = 0; i < _ranking.Length; i++)
        {
            if(i + 1 == _ranking.Length)
            {
                _setData += _ranking[i].ToString();
                break;
            }
            _setData += _ranking[i].ToString() + ",";
        }

        string fi = FolderName + "/" + _filePath;
        try
        {
            using (StreamWriter wr = new StreamWriter(fi, false, Encoding.UTF8))
            {
                wr.WriteLine(_setData);
            }
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
