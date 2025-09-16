using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class PerSecondText : MonoBehaviour
{
    [SerializeField] GameObject Tuna;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Tuna.GetComponent<Rigidbody>().velocity;
        //velocity��ݏ悵�đ傫���ɕς���Mathf.Sprt�Ń��[�g������B
        float TotalVelocity = Mathf.Sqrt(velocity.z * velocity.z + velocity.x * velocity.x) / 2;
        this.GetComponent<Text>().text = TotalVelocity.ToString("F0") + "km/h";
    }
}
