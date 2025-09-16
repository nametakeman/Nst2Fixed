using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretMenu : MonoBehaviour
{
    int secretCount = 0;
    [SerializeField] GameObject secretMenu;

    public void secretClicked2()
    {
        
        if(secretCount == 0)
        {
            secretCount += 1;
        }
        else if(secretCount == 4)
        {
            secretCount+= 1;
        }
        else if(secretCount == 6)
        {
            secretMenu.SetActive(true);
        }
        else
        {
            Debug.Log("リセットします2");
            secretCount = 0;
        }
        
    }
    public void secretClickedN()
    {
        if (secretCount == 1)
        {
            secretCount += 1;
        }
        else if (secretCount == 2)
        {
            secretCount += 1;
        }
        else if (secretCount == 3)
        {
            secretCount += 1;
        }
        else if (secretCount == 5)
        {
            secretCount += 1;
        }
        else
        {
            Debug.Log("リセットしますN" + secretCount);
            secretCount = 0;
        }
    }

    
}
