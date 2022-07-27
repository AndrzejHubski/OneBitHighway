using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerController.isUp = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerController.isRight = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerController.isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
