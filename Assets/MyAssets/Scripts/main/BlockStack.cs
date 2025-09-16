using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    [SerializeField] GameObject Tuna;
    float _firstTime = new float();
    float _logPosX;
    float _logPosZ;

    private void Start()
    {
        _firstTime = Time.time;
    }
    private void FixedUpdate()
    {
        
    }

    IEnumerator tunaMove()
    {
        Tuna.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        yield return new WaitForSeconds(1);
        Tuna.GetComponent<Rigidbody>().AddForce(new Vector3(0, -100, 0));


    }

}
