using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UniTask.Delay(3000);
        SceneManager.LoadScene("main");
    }
}
