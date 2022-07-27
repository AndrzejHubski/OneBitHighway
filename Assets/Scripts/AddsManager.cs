using UnityEngine;
using UnityEngine.SceneManagement;

public class AddsManager : MonoBehaviour
{
    public float timeAfterAdd;
    public bool skippableAdd;

    private void Start()
    {
        timeAfterAdd = PlayerPrefs.GetFloat("timeAfterAdd");
    }

    private void Update()
    {
        timeAfterAdd += Time.deltaTime;
        if (timeAfterAdd >= 300)
        {
            skippableAdd = true;
            timeAfterAdd = 0;
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("timeAfterAdd", timeAfterAdd);
    }
}
