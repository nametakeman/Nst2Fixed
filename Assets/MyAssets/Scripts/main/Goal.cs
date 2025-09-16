using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    
    Vector3 GoalPos;
    GameObject Tuna;
    // Start is called before the first frame update
    void Start()
    {
        Tuna = GameObject.Find("Tuna");
        GoalPos = this.transform.position;
    }

    // Update is called once per frame
}
