using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] cars;
    public int[] cost;
    public int id, coins;
    public Text buyButtonText, coinsText;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("car0", 1);
        for (int i = 0; i < cars.Length; i++)
        {
            cost[i] = 2300 * i;
        }
    }

    private void Update()
    {


        coinsText.text = coins.ToString();

        spriteRenderer.sprite = cars[id];

        if(PlayerPrefs.GetInt("car" + id.ToString()) == 1)
        {
            buyButtonText.text = "Choose";
        }
        else
        {
            buyButtonText.text = "Buy \n" + cost[id];
        }
    }

    public void NextCar()
    {
        if (id < cars.Length - 1)
        {
            id++;
        }
    }

    public void PreviousCar()
    {
        if (id > 0)
        {
            id--;
        }
    }

    public void BuyButton()
    {
        if (PlayerPrefs.GetInt("car" + id.ToString()) == 1)
        {
            PlayerPrefs.SetInt("id", id);
            SceneManager.LoadScene(2);
        }
        else
        {
            if(coins >= cost[id])
            {
                coins -= cost[id];
                PlayerPrefs.SetInt("coins", coins);
                PlayerPrefs.SetInt("car" + id.ToString(), 1);
            }
        }
    }
}
