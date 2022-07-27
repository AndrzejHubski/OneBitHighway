using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneLoader : MonoBehaviour
{

    public bool loadScene = false;

    public void Start()
    {
        PlayerPrefs.SetFloat("timeAfterAdd", 300);
    }

    // Update is called once per frame
    void Update()
    {
        if(loadScene == true && PlayerPrefs.GetInt("tutorial") == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if(loadScene == true)
        {
            SceneManager.LoadScene(1);
        }
    }
}
