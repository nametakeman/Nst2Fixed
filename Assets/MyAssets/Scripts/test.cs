using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject TEst;
    ObjectStatus[] _objectStatuses = new ObjectStatus[]
    {
        new JumpRock1()
    };
    // Start is called before the first frame update
    void Start()
    {
        ObjectsCreate2();
    }

    private async UniTask ObjectsCreate2()
    {
        //1cube�𕪊����邽�߂̓񎟌��z��������A
        bool[,] _mapDivisioner = new bool[350, 350];
        //�z��̏���������
        for (int i = 349; i >= 0; i--)
        {
            for (int i2 = 349; i2 >= 0; i2--)
            {
                _mapDivisioner[i, i2] = false;
            }
        }

        //�ΏۂƂȂ�I�u�W�F�N�g��I��
        for (int i = _objectStatuses.Length - 1; i >= 0; i--)
        {
            ObjectStatus _targetedOb = _objectStatuses[i];
            //�񎟌��z��̍��W�������Vector2���X�g�^
            List<Vector2> _registerPoses = new List<Vector2>();

            //mapDivisioner��0,0���n�_�Ƃ��ċK���I�Ɋl���\�ȗ̈�𒲂ׂ�B������͎n�_�̍��W��ۑ����Ă����B�I�u�W�F�N�g�̒��S�ɒ����Ȃ��I
            for (int n = 0; n < 350; n++)
            {
                for (int n2 = 0; n2 < 350; n2++)
                {
                    bool _result = await _searchRange(i, _mapDivisioner, new Vector2(n, n2));
                    if (!_result)
                    {
                        continue;
                    }
                    _registerPoses.Add(new Vector2(n, n2));
                    Debug.Log(n + "," + n2 + "count" + _registerPoses.Count);
                }
            }

            Debug.Log("�ݒu�\�n�_��" + _registerPoses.Count());

            List<int> _registerRandomInt = new List<int>();
            List<Vector2> _dsidedPoses = new List<Vector2>();
            /*
            while (_dsidedPoses.Count <= _objectStatuses[i]._NumOfPiece)
            {
                //��������Ԃ񃉃��_���łԂ�܂킷
                int _randomInt = UnityEngine.Random.Range(0, _registerPoses.Count);
                //�������o�Ȃ炻���ŏ������I��������B
                if (_registerRandomInt.Contains(_randomInt))
                {
                    continue;
                }


                //�����Ɛ����ꏊ���m�ۂł��邩���m�F����
                bool _result = _searchRange(i, _mapDivisioner, _registerPoses[_randomInt]);
                _registerRandomInt.Add(_randomInt);
                if (!_result)
                {
                    continue;
                }

                //��������
                Vector2 _fixedPos = _fixPos(i, _registerPoses[_randomInt], new Vector3(1750, 0, 1750));
                Instantiate(TEst, new Vector3(_fixedPos.x, 10, _fixedPos.y), Quaternion.identity);
            }
            */
        }



    }

    private async UniTask<bool> _searchRange(int i, bool[,] _mapDivisioner, Vector2 _startPoint)
    {
        //�n�_��false
        for (int n3 = 0; n3 < _objectStatuses[i]._length * 2; n3++)
        {
            for (int n4 = 0; n4 < _objectStatuses[i]._width * 2; n4++)
            {
                if ((int)_startPoint.x + n3 > _mapDivisioner.GetLength(0) - 1 || (int)_startPoint.y + n4 > _mapDivisioner.GetLength(1) - 1)
                {
                    return false;
                }

                Debug.Log("startPoint" + ((int)_startPoint.x + n3).ToString() + "," + ((int)_startPoint.y + n4).ToString() + "ArrayLength" + _mapDivisioner.GetLength(0) + "," + _mapDivisioner.GetLength(1));
                if (_mapDivisioner[(int)_startPoint.x + n3, (int)_startPoint.y + n4])
                {
                    //������true�ɂȂ������_�Ŏn�_(n,n2)�����炷
                    return false;
                }
            }
        }
        Debug.Log("register");
        return true;
    }

    //worldPos�͐�������cube�̒��S���W��n���B
    private Vector2 _fixPos(int _objectNum, Vector2 _fixedPos, Vector3 _worldPos)
    {
        //�����Ŕ��ł���͎̂n�_�̍��W
        //�񎟌��z���0,0(�̒��ł���ԍ���)�ɂ������̃��[���h���W���o��
        Vector2 _firstWdPos = new Vector2(0, _worldPos.z - 1750);//����cube�̑傫���ς���ƃo�O��̒��ӁI

        Vector2 _startWdPos = new Vector2(_firstWdPos.x + _fixedPos.x * 10, _firstWdPos.y + _fixedPos.y * 10);
        return new Vector2(_startWdPos.x + _objectStatuses[_objectNum]._width * 10, _startWdPos.y + _objectStatuses[_objectNum]._length * 10);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
