using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine.InputSystem;


public class TunaMove: MonoBehaviour
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
    public bool _death = false;
    [SerializeField] GameObject _nowTimeOb;
    [SerializeField] GameObject _rankingText;
    [SerializeField] GameObject SpaceGuidOb;
    [SerializeField] GameObject ItemOb;
    [SerializeField] GameObject _rankingOb;
    [SerializeField] GameObject _worldRankingBottan;
    [SerializeField] GameObject _rankingMethod;
    [SerializeField] GameObject EndBottanOb;
    [SerializeField] GameObject _mapGenerator;
    [SerializeField] Death _deathClass;
    [SerializeField] float anchorAddSpeed;

    private GameInput _gameInputs;
    private OriginalInputAction _originalInputAction;
    private Vector2 _moveInputValue;
    Animator _tunaAnim;

    string _fileName = "localRanking.txt";
    string _fileName2 = "localRankingLong.txt";
    
    void Awake()
    {
        _originalInputAction = OriginalInputAction.Instance;
        _originalInputAction.MoveFunc += OnMove;
        
        ItemOb.GetComponent<_Item>().setObjects();
        _rankingText.SetActive(false);
        float FirstSpeed = speed;
        rb = GetComponent<Rigidbody>();
        GoalObject = GameObject.FindWithTag("Goal");
        _tunaAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CountDownObject.GetComponent<CountDown>().StartCheck)
        {
            //マグロの現在座標取得用
            Vector3 pos = this.transform.position;

            //水中かどうかを判断してフィルターを制御する
            if (pos.y >= 0.9f)
            {
                ColorAdjustments c;
                Volume.profile.TryGet<ColorAdjustments>(out c);
                c.colorFilter.value = new Color(1, 1, 1);
            }
            else if (-28 < pos.y  && pos.y <= 0.9f)
            {
                ColorAdjustments c;
                Volume.profile.TryGet<ColorAdjustments>(out c);
                c.colorFilter.value = new Color(0.6862745f, 0.9960784f, 1);
            }
            else if(pos.y <= -28f　&& !_death)
            {
                _death = true;
                _deathClass.deathFlow();
            }

            speed = 1;
            //マグロの今のスピード
            Vector3 NowSpeed = this.GetComponent<Rigidbody>().velocity;

            //速度が上がるにつれて加速分が減少する
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


            //マグロを動かす
            /*
            float xMovement = Input.GetAxisRaw("Horizontal");
            float zMovement = Input.GetAxisRaw("Vertical");
            */
            float xMovement = _moveInputValue.y;
            float zMovement = _moveInputValue.x;
            float NoSkyBack = 1;
            float jumpUp = 1;


            Vector3 lastSpeed = new Vector3();

            //空中では後ろに下がらないため空中にいる場合はNoSkyBackでforce.zを0にする
            if (pos.y >= 0.9f && zMovement < 0)
            {
                jumpUp = 3f;
                NoSkyBack = 0;
            }

            lastSpeed.x = speed * xMovement * jumpUp;
            lastSpeed.z = speed * zMovement * NoSkyBack;
            //死んでる時は加速させない
            if (!_death)
            {
                rb.AddForce(lastSpeed, ForceMode.Impulse);
            }

            //マグロの速度を取得する
            Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
            float TotalVelocity = Mathf.Sqrt(-velocity.z * -velocity.z + velocity.x * velocity.x) / 2;
            
            //マグロがゴールした時の処理
            if (this.transform.position.z >= GoalObject.transform.position.z)
            {
                GoalCheck = true;
                goalMethod();
            }


            //尻尾をスピードに合わせて降らせる。
            float kosihuriSpeed;
            kosihuriSpeed = TotalVelocity / 10;
            if(kosihuriSpeed <= 2)
            {
                kosihuriSpeed = 2;
            }
            _tunaAnim.SetFloat("kosihuri", kosihuriSpeed);

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

    private async UniTask goalMethod()
    {
        string _localFileName = "";
        float _nowTime = _nowTimeOb.GetComponent<Timer>().NowTime;
        EndBottanOb.GetComponent<EndBottan>()._score = _nowTime;
        if (_mapGenerator.GetComponent<MapGenerater>().MapLength == 14000)
        {
            _localFileName = "localRanking.txt";
        }
        else
        {
            _localFileName = "localRankingLong.txt";
        }

        float[] _ranking = _rankingMethod.GetComponent<Ranking>()._getLocalRanking(_fileName);
        if (_ranking[9] >= _nowTime)
        {
            _ranking[9] = _nowTime;
        }
        Array.Sort(_ranking);

        string _rankingString = null;
        for(int i = 0; i < 10; i++)
        {
            if(i == 0)
            {
                if (_ranking[i] == 3000000)
                {
                    _rankingString += "\n" + (i + 1).ToString() + "位：" + "No Data!!";
                }
                else
                {
                    int _rankingM = (int)(_ranking[i] / 60);
                    _rankingString = "1位：" + _rankingM.ToString("00") + ":" + ((int)_ranking[i] % 60).ToString("00");
                }
            }
            else
            {
                if (_ranking[i] == 3000000)
                {
                    _rankingString += "\n" + (i + 1).ToString() + "位：" + "No Data!!";
                }
                else
                {
                    int _rankingM = (int)(_ranking[i] / 60);
                    _rankingString += "\n" + (i + 1).ToString() + "位：" + _rankingM.ToString("00") + ":" +((int)_ranking[i] % 60).ToString("00");
                }
            }
        }
        _rankingMethod.GetComponent<Ranking>()._setLocalRanking(_ranking, _localFileName);
        _rankingOb.SetActive(true);

        if(_mapGenerator.GetComponent<MapGenerater>().MapLength == 14000)
        {
            _worldRankingBottan.SetActive(false) ;
        }
        _rankingText.SetActive(true);

        _rankingText.GetComponent<Text>().text = _rankingString;
    }

    /// <summary>
    ///inputsystemに登録するメソッド、移動入力を代入するだけ
    /// </summary>
    private void OnMove(Vector2 vec2)
    {
        _moveInputValue = vec2;
    }
    
    private void OnDestroy()
    {
        //自身でインスタンス化した為マグロを消すタイミングでDisposeする
        _gameInputs?.Dispose();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "AnchorLing")
        {
            Vector3 addSpeed = new Vector3(0,0, anchorAddSpeed);
            rb.AddForce(addSpeed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Box001")
        {
            other.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("BreakBox", true);
            Destroy(other.gameObject);
            ItemOb.GetComponent<_Item>().lotteryItem();
        }
    }
}
