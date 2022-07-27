using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int plusScore = 20;
    public int score;
    private float timer;

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            score += plusScore;
            timer = 0;
        }
    }
}
