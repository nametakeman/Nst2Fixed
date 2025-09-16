using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBottan : MonoBehaviour
{
    [SerializeField] GameObject ItemOb;
    public float _score;

    public void returnStartBottan()
    {
        ItemOb.GetComponent<_Item>().cancellTasks();
        SceneManager.LoadScene("Start");
    }

    public void onlineRankingBottan()
    {
        ItemOb.GetComponent<_Item>().cancellTasks();
        GameObject _scoreObject = new GameObject();
        _scoreObject.tag = "score";
        _scoreObject.AddComponent<dontDestroy>();
        _scoreObject.AddComponent<Score>();
        _scoreObject.GetComponent<Score>()._score = _score;

        SceneManager.LoadScene("OnlineRanking");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
