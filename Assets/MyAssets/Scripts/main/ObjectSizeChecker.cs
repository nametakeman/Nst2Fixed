using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSizeChecker : MonoBehaviour
{
    [SerializeField] GameObject _testObject;
    [SerializeField] GameObject _checkBlock;
    ObjectStatus _status = null;
    ObjectStatus[] statuses = new ObjectStatus[]
    {
        new JumpRock1()
    };

    // Start is called before the first frame update
    void Start()
    {

        foreach (ObjectStatus o in statuses)
        {
            if (o._objectName == _testObject.name)
            {
                _status = o;
                break;
            }
        }

        LineRenderer _lineRenderer = _testObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        Vector3 _startPosition = 
            new Vector3(_testObject.transform.position.x - (_status._width * 10),_testObject.transform.position.y,_testObject.transform.position.z - (_status._length * 10));

        Vector3 _secondPosition = 
            new Vector3(_startPosition.x,_startPosition.y,_testObject.transform.position.z + (_status._length * 10));

        Vector3 _thirdPosition =
            new Vector3(_testObject.transform.position.x + (_status._width * 10),_secondPosition.y,_secondPosition.z);

        Vector3 _foursPosition =
            new Vector3(_thirdPosition.x, _thirdPosition.y, _testObject.transform.position.z - (_status._length * 10));

        _lineRenderer.positionCount = 5;
        _lineRenderer.SetPositions(new Vector3[] { _startPosition, _secondPosition, _thirdPosition, _foursPosition, _startPosition });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
