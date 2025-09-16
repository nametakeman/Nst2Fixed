using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//オブジェクトの間隔を見るためのスクリプト、x方向に指定のz座標分離れたオブジェクトを生成する。
public class ObjectMator : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] int Range;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Object, new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z + Range), Quaternion.Euler(Object.transform.localEulerAngles.x, Object.transform.localEulerAngles.y, Object.transform.localEulerAngles.z));
        Instantiate(Object, new Vector3(Object.transform.position.x + Range, Object.transform.position.y, Object.transform.position.z), Quaternion.Euler(Object.transform.localEulerAngles.x, Object.transform.localEulerAngles.y, Object.transform.localEulerAngles.z));
    }
}
