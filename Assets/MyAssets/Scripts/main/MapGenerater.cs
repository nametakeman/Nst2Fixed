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
    //���W���L�[�ɂ��ăI�u�W�F�N�g�̖��O��l�ɂ���dictionary�̍쐬
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

    //�I�u�W�F�N�g�̃v���n�u���i�[���郊�X�g
    public List<GameObject> ObjectsType = new List<GameObject>();
    //�I�u�W�F�N�g�̐��������i�[����dictionary.<�v���n�u,������>
    public Dictionary<GameObject,int> ObjectPeace = new Dictionary<GameObject, int>();
    //�I�u�W�F�N�g�͈̔͂����[����dictionary.<�v���n�u,�͈�>
    public Dictionary<GameObject, float> ObjectRangeD = new Dictionary<GameObject, float>();
    //���������I�u�W�F�N�g�̃v���n�u��u���Ă������X�g
    public List<GameObject> CreatedObject = new List<GameObject>();
    //���������I�u�W�F�N�g�̍��W��u���Ă������X�g
    public List<Vector3> CreatedObjectPos = new List<Vector3>();

    //objectStatus���p������N���X�̃��X�g
    ObjectStatus[] _objectStatuses = new ObjectStatus[]
    {
        new JumpRock1()
    };

    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g�̎�ނ�ǉ��B��łǂ������̃t�@�C���ɂ܂Ƃ߂��Ȃ�������
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
        //�I�u�W�F�N�g�̐�������ǉ��B��łǂ������̃t�@�C���ɂ܂Ƃ߂��Ȃ�������
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
        ObjectPeace.Add(ItemBox, 17);

        
        int BuriRandom = UnityEngine.Random.Range(0, 101);
        int BuriNum = 0;
        if(BuriRandom == 0)
        {
            BuriNum = 1;
        }
        ObjectPeace.Add(Buri, BuriNum);
        //�I�u�W�F�N�g���Ƃ͈̔͂�dictionary.��łǂ������̃t�@�C���ɂ܂Ƃ߂��Ȃ�������
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
        //�L���[�u�쐬�p�̃��X�g���쐬
        List<float> CubePosZ = new List<float>();
        List<float> CubePosY = new List<float>();

        GameObject _distanceOb = GameObject.FindWithTag("distance");
        if (_distanceOb == null) MapLength = 14000;
        else MapLength = _distanceOb.GetComponent<OfflineDistance>()._islong;

        while (MapCheck)
        {
            //Cubes�^�O�̕t�����I�u�W�F�N�g�����ׂĎ擾���Ĕz��Ɋi�[
            GameObject[] Cubes = GameObject.FindGameObjectsWithTag("Cubes");
            //�z�񂩂�z���W�𔲂��o����list�ɒǉ����Ă���
            for (int yakiniku = Cubes.Length; yakiniku > 0; yakiniku--)
            {
                CubePosZ.Add(Cubes[yakiniku - 1].transform.position.z);
                CubePosY.Add(Cubes[yakiniku - 1].transform.position.y);
            }
            //���X�g�̕\���p
            /*
            for (int sukiyaki = 0; sukiyaki < CubePosZ.Count; sukiyaki++)
            {
                Debug.Log("�y�}�b�v�n�z" + CubePosZ[sukiyaki]);
            }
            */
            //���X�g�̒��g�������ɕ��ׂ�B
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
        Debug.Log("�}�b�v�̐������I�����܂���");
        WaitObjects.SetActive(true);

    }

   
    private async UniTask ObjectsCreate2()
    {
        //1cube�𕪊����邽�߂̓񎟌��z��������A
        bool[,] _mapDivisioner = new bool[350, 350];
        //�z��̏���������
        for(int i = 349; i >= 0; i--)
        {
            for(int i2 = 349; i2 >= 0; i2--)
            {
                _mapDivisioner[i, i2] = false;
            }
        }

        //�ΏۂƂȂ�I�u�W�F�N�g��I��
        for(int i = _objectStatuses.Length - 1; i >= 0; i--)
        {
            ObjectStatus _targetedOb = _objectStatuses[i];
            //�񎟌��z��̍��W�������Vector2���X�g�^
            List<Vector2> _registerPoses = new List<Vector2>();

            //mapDivisioner��0,0���n�_�Ƃ��ċK���I�Ɋl���\�ȗ̈�𒲂ׂ�B������͎n�_�̍��W��ۑ����Ă����B�I�u�W�F�N�g�̒��S�ɒ����Ȃ��I
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

            Debug.Log("�ݒu�\�n�_��" + _registerPoses.Count());

            List<int> _registerRandomInt = new List<int>();
            List<Vector2> _dsidedPoses = new List<Vector2>();
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
                bool _result = await _searchRange(i, _mapDivisioner, _registerPoses[_randomInt]);
                _registerRandomInt.Add(_randomInt);
                if (!_result)
                {
                    continue;
                }

                //��������
                Vector2 _fixedPos = _fixPos(i,_registerPoses[_randomInt], new Vector3(1750,0,1750));
                Instantiate(TEst, new Vector3(_fixedPos.x, 10, _fixedPos.y), Quaternion.identity);
            }
        }
    }

    private async UniTask<bool> _searchRange(int i, bool[,] _mapDivisioner, Vector2 _startPoint)
    {
        //�n�_��false
        for (int n3 = 0; n3 < _objectStatuses[i]._length * 2; n3++)
        {
            for (int n4 = 0; n4 < _objectStatuses[i]._width * 2; n4++)
            {

                if (_mapDivisioner[(int)_startPoint.x + n3,(int)_startPoint.y + n4])
                {
                    //������true�ɂȂ������_�Ŏn�_(n,n2)�����炷
                    return false;
                }
            }
        }
        return true;
    }

    //worldPos�͐�������cube�̒��S���W��n���B
    private Vector2 _fixPos(int _objectNum, Vector2 _fixedPos, Vector3 _worldPos)
    {
        //�����Ŕ��ł���͎̂n�_�̍��W
        //�񎟌��z���0,0(�̒��ł���ԍ���)�ɂ������̃��[���h���W���o��
        Vector2 _firstWdPos = new Vector2(0, _worldPos.z - 1750);//����cube�̑傫���ς���ƃo�O��̒��ӁI

        Vector2 _startWdPos = new Vector2(_firstWdPos.x + _fixedPos.x * 10, _firstWdPos.y + _fixedPos.y * 10);
        return new Vector2(_startWdPos.x + _objectStatuses[_objectNum]._width * 10,_startWdPos.y + _objectStatuses[_objectNum]._length * 10);
    }

    //�}�b�v���̃I�u�W�F�N�g���쐬���郁�\�b�h
    private void ObjectsCreate()
    {
        //Cubes�^�O�̕t�����I�u�W�F�N�g�����ׂĎ擾���Ĕz��Ɋi�[
        GameObject[] Cubes = GameObject.FindGameObjectsWithTag("Cubes");

        //���������I�u�W�F�N�g�̃v���n�u��u���Ă������X�g
        List<GameObject> CreatedObject = new List<GameObject>();
        //���������I�u�W�F�N�g�̍��W��u���Ă������X�g
        List<Vector3> CreatedObjectPos = new List<Vector3>();
        
        bool OutRangeCheck = false;

        //���ׂẴL���[�u�ɌJ��Ԃ���������
        for (int udon = Cubes.Length; udon > 0; udon--)
        {

            for (int onigiri = ObjectsType.Count; onigiri > 0; onigiri--)
            {
                //���[�v�̉񐔂��獡�񐶐�����v���n�u���擾���Ă���B
                GameObject CreateObjectPrefab = ObjectsType[onigiri - 1];
                //�I�u�W�F�N�g�̐�������dictionary����擾���Ă���B
                int CreateObjectPeace = ObjectPeace[CreateObjectPrefab];

                //oden�̓A�C�e���̐����ʁB
                for (int oden = 1; oden <= CreateObjectPeace; oden++)
                {
                    //�����ł̓e�X�g�̂��߂�Cube����ڂɂ���B���ƂŕύX�I
                    Vector3 CubesO = Cubes[udon - 1].transform.position;
                    //�I�u�W�F�N�g�𐶐�����ꏊ���Ƃ肠���������_���Ő���
                    Vector3 ObjectPos = new Vector3();
                    ObjectPos.x = UnityEngine.Random.Range(CubesO.x - 665, CubesO.x + 666);
                    ObjectPos.z = UnityEngine.Random.Range(CubesO.z - 1750, CubesO.z + 1751);
                    ObjectPos.y = CreateObjectPrefab.transform.position.y;

                    //�������߂Đ�������I�u�W�F�N�g�Ȃ炻�̂܂ܐݒu
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
            //1�L���[�u���I������烊�X�g�n�̒��g�����Z�b�g����
            CreatedObject.Clear();
            CreatedObjectPos.Clear();
        }

    }

    //�C�ӂ͈͓̔����ǂ�����Ԃ��B�͈͂����Ԃ��Ă�����false�A����Ă��Ȃ�������true��Ԃ��B
    private bool OutRange(Vector3 OPosComparison, float RangeComparison)
    {
        for (int SearchObject = CreatedObject.Count; SearchObject > 0; SearchObject--)
        {
            //�ȉ���dictionary����Q�Ƃ��Ă����p�̔z��
            float[] xObjectPosA = new float[2];
            float[] zObjectPosA = new float[2];
            
            //���ꂼ�ꃋ�[�v���ŎQ�Ƃ��Ă���dictionary�̍��W���擾���ă��X�g�Ɋi�[
            xObjectPosA[0] = CreatedObjectPos[SearchObject - 1].x; zObjectPosA[2] = CreatedObjectPos[SearchObject - 1].z;
            GameObject ObjectName = CreatedObject[SearchObject - 1];
            
            //ObjectPos[x���W�̃v���X�����͈̔�,x���W�̃}�C�i�X�����͈̔�,z���W�̃v���X�����͈̔�,z���W�̃}�C�i�X�����͈̔�]
            xObjectPosA[0] += ObjectRangeD[ObjectName]; xObjectPosA[1] = xObjectPosA[0] - ObjectRangeD[ObjectName];
            zObjectPosA[0] += ObjectRangeD[ObjectName]; zObjectPosA[1] = zObjectPosA[0] - ObjectRangeD[ObjectName];
            //�����Ƀ\�[�g����
            Array.Sort(xObjectPosA);
            Array.Sort (zObjectPosA);
            
            //�ȉ�����r�����I�u�W�F�N�g�̔z��
            float[] xObjectPosB = new float[2];
            float[] zObjectPosB = new float[2];
            xObjectPosB[0] = OPosComparison.x + RangeComparison; xObjectPosB[1] = OPosComparison.x - RangeComparison;
            zObjectPosB[0] = OPosComparison.z + RangeComparison; zObjectPosB[1] = OPosComparison.z - RangeComparison;
            //�����Ƀ\�[�g
            Array.Sort(xObjectPosB);
            Array.Sort (zObjectPosB);

            //�܂�x������͈͓��ɓ����Ă鎞�̂�false��Ԃ�
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
