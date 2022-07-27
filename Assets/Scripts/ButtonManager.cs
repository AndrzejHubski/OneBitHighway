using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public RewAd rewAd;
    public AdMobScripts adMobScripts;

    public void RetryButton()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void ShopButton()
    {
        SceneManager.LoadScene(3);
    }

    public void ReviveButton()
    {
        if (PlayerPrefs.GetInt("rewardedUnity") == 1)
        {
            rewAd.ShowAd();
            PlayerPrefs.SetInt("rewardedUnity", 0);
        }
        else
        {
            adMobScripts.ShowRewardedVideo();
            PlayerPrefs.SetInt("rewardedUnity", 1);
        }
    }
}
