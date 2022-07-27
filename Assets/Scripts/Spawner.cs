using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cars;
    public float spawnTime = 5, difficultyMultiplier = 1;
    private float timer;

    private void Start()
    {
        spawnTime = Random.Range(0.8f * spawnTime, 1.7f * spawnTime);
        timer = Random.Range(spawnTime * 0.5f, spawnTime * 1.5f);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(cars[Random.Range(0, cars.Length)], transform.position, Quaternion.identity);
            timer = Random.Range(spawnTime * 0.5f, spawnTime * 1.5f);
        }
    }
}
