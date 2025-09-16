using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System;


public class TunaMoveOn: MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] Volume Volume;
    float speed;
    [SerializeField] GameObject SeaFilter;
    [SerializeField] int Gravity;
    public bool InSea = true;
    GameObject GoalObject;
    [SerializeField] GameObject CountDownObject;
    public bool GoalCheck;
    [SerializeField] GameObject _nowTimeOb;
    [SerializeField] GameObject _rankingText;
    [SerializeField] GameObject SpaceGuidOb;
    [SerializeField] GameObject GoalPanel;
    [SerializeField] GameObject OnlineEnd;
    [SerializeField] Text WinOrtx;
    [SerializeField] float anchorAddSpeed;

    Animator tunaAnim;
    // Start is called before the first frame update
    void Start()
    {
        _rankingText.SetActive(false);
        float FirstSpeed = speed;
        rb = GetComponent<Rigidbody>();
        GoalObject = GameObject.FindWithTag("Goal");
        tunaAnim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (CountDownObject.GetComponent<CountDownOn>().StartCheck)
        {
            //�}�O���̌��ݍ��W�擾�p
            Vector3 pos = this.transform.position;

            //�������ǂ����𔻒f���ăt�B���^�[�𐧌䂷��
            if (pos.y >= 0.9f)
            {
                ColorAdjustments c;
                Volume.profile.TryGet<ColorAdjustments>(out c);
                c.colorFilter.value = new Color(1, 1, 1);
            }
            else if (pos.y <= 0.9f)
            {
                ColorAdjustments c;
                Volume.profile.TryGet<ColorAdjustments>(out c);
                c.colorFilter.value = new Color(0.6862745f, 0.9960784f, 1);
            }
            speed = 1;
            //�}�O���̍��̃X�s�[�h
            Vector3 NowSpeed = this.GetComponent<Rigidbody>().velocity;

            //���x���オ��ɂ�ĉ���������������
            float TotalNowSpeed = Mathf.Sqrt(NowSpeed.z * NowSpeed.z + NowSpeed.x * NowSpeed.x) / 2;
            float SeparateSpeed = Mathf.Floor(TotalNowSpeed / 50);
            if (SeparateSpeed >= 9)
            {
                speed = speed - 0.9f;
            }
            else
            {
                speed = speed - (SeparateSpeed / 10);
            }


            //�}�O���𓮂���
            float xMovement = Input.GetAxisRaw("Horizontal");
            float zMovement = Input.GetAxisRaw("Vertical");
            float NoSkyBack = 1;
            float jumpUp = 1;


            Vector3 osusi = new Vector3();

            //�󒆂ł͌��ɉ�����Ȃ����ߋ󒆂ɂ���ꍇ��NoSkyBack��force.z��0�ɂ���
            if (pos.y >= 0.9f && zMovement < 0)
            {
                jumpUp = 3f;
                NoSkyBack = 0;
            }

            osusi.x = speed * xMovement * jumpUp;
            osusi.z = speed * zMovement * NoSkyBack;
            rb.AddForce(osusi, ForceMode.Impulse);

            //�}�O���̑��x���擾����
            Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
            float TotalVelocity = Mathf.Sqrt(-velocity.z * -velocity.z + velocity.x * velocity.x) / 2;

            if (this.transform.position.z >= GoalObject.transform.position.z && GoalCheck == false)
            {
                GoalCheck = true;
                SpaceGuidOb.SetActive(true);
                //�I�����C���Ǝ��̏I���@�\
                OnlineEnd.GetComponent<OnlineEnd>().OnlineEndMethod();
                goalMethod("win");
            }


            //�K�����X�s�[�h�ɍ��킹�č~�点��B
            float kosihuriSpeed;
            kosihuriSpeed = TotalVelocity / 10;
            if(kosihuriSpeed <= 2)
            {
                kosihuriSpeed = 2;
            }
            tunaAnim.SetFloat("kosihuri", kosihuriSpeed);

        }else return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Jump"))
        {
            Vector3 UpForce = new Vector3(50, 30, 0);
                rb.AddForce(UpForce,ForceMode.Impulse);
        }
    }

    

    public void goalMethod(string winOr)
    {
        GoalCheck = true;
        GoalPanel.SetActive(true);
        float _nowTime = _nowTimeOb.GetComponent<Timer>().NowTime;

        if(winOr == "win")
        {
            WinOrtx.GetComponent<Text>().text = "You win!!";
        }
        else if(winOr == "lose")
        {
            WinOrtx.GetComponent<Text>().text = "You lose!!";
        }


        float[] _ranking = new float[10];

        for (int i = 9; i >= 0; i--)
        {
            _ranking[i] = PlayerPrefs.GetFloat("_ranking" + i, 300000);
        }

        if (_ranking[9] > _nowTime)
        {
            _ranking[9] = _nowTime;
            Array.Sort( _ranking );
        }

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat("_ranking" + i, _ranking[i]);
        }

        string _rankingString = null;
        for(int i = 0; i < 10; i++)
        {
            if(i == 0)
            {
                if (_ranking[i] == 300000)
                {
                    _rankingString += "\n" + (i + 1).ToString() + "�ʁF" + "No Data!!";
                }
                else
                {
                    int _rankingM = (int)(_ranking[i] / 60);
                    _rankingString = "1�ʁF" + _rankingM.ToString("00") + ":" + ((int)_ranking[i] - _rankingM * 60).ToString("00");
                }
            }
            else
            {
                if (_ranking[i] == 300000)
                {
                    _rankingString += "\n" + (i + 1).ToString() + "�ʁF" + "No Data!!";
                }
                else
                {
                    int _rankingM = (int)(_ranking[i] / 60);
                    _rankingString += "\n" + (i + 1).ToString() + "�ʁF" + _rankingM.ToString("00") + ":" +((int)_ranking[i] - _rankingM * 60).ToString("00");
                }
            }
        }

        //_rankingText.SetActive(true);
        //_rankingText.GetComponent<Text>().text = _rankingString;
        StartCoroutine(plzSpace());

    }

    IEnumerator plzSpace()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        _rankingText.GetComponent<Text>().text = "";
        OnlineEnd.GetComponent<OnlineEnd>().disconectNet();
        SpaceGuidOb.SetActive(false);
        SceneManager.LoadScene("Start");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "AnchorLing")
        {
            Vector3 addSpeed = new Vector3(0,0, anchorAddSpeed);
            rb.AddForce(addSpeed, ForceMode.Impulse);
        }
    }

}
