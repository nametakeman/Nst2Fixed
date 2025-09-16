using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClickedBottan : MonoBehaviour
{
    [SerializeField] GameObject _distancePanel;
    Vector3 OriginalSize;
    Vector3 ExpansionSize;
    bool ObjectExpansing;
    Tween tweenLarge;
    Tween tweenSmall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseEnterStartBottan()
    {
        this.tweenSmall.Kill();
        OriginalSize = this.transform.localScale;
        this.tweenLarge = this.transform.DOScale(new Vector3(0.45f, 0.45f,0), 0.2f).SetEase(Ease.InSine);

    }

    public void OnMouseOverStartBottan()
    {
        this.tweenLarge.Kill();
        this.tweenSmall = this.transform.DOScale(new Vector3(0.418f, 0.418f, 0), 0.2f).SetEase(Ease.InSine);
    }


    public void OnClickStartBottan()
    {
        if(this.name == "ClickedBottan1")
        {
            _distancePanel.SetActive(true);

        }else if(this.name == "ClickedBottan2")
        {

            SceneManager.LoadScene("Matching");

        }else if(this.name == "ClickedBottan3")
        {

            SceneManager.LoadScene("Credit");

        }else if(this.name == "ExitBottan")
        {
            Application.Quit();
        }
    }

    public void EnterClickStartBottan()
    {
        if (this.name == "ClickedBottan1")
        {

        }
        else if (this.name == "ClickedBottan2")
        {
            

        }
        else if (this.name == "ClickedBottan3")
        {

            

        }
        else if (this.name == "ExitBottan")
        {
            
        }
    }


}
