using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private PlayerController playerController;
    private float startTouchPositionX, startTouchPositionY, currentTouchPositionX, currentTouchPositionY, timer;
    public float sensivity = 1, swipeTime;
    private bool isSwiped = false;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        float swipeDistanceX, swipeDistanceY;

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPositionX = Input.mousePosition.x;
            startTouchPositionY = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            currentTouchPositionX = Input.mousePosition.x;
            currentTouchPositionY = Input.mousePosition.y;
            swipeDistanceX = currentTouchPositionX - startTouchPositionX;
            swipeDistanceY = currentTouchPositionY - startTouchPositionY;
            if (timer < swipeTime && (Mathf.Abs(swipeDistanceX) > Screen.width / sensivity || Mathf.Abs(swipeDistanceY) > Screen.width / sensivity) && isSwiped == false)
            {
                if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
                {
                    HorizontalSwipe(swipeDistanceX);
                }
                else
                {
                    VerticalSwipe(swipeDistanceY);
                }
                timer = 0;
                startTouchPositionX = 0;
                startTouchPositionY = 0;
                currentTouchPositionX = 0;
                currentTouchPositionY = 0;
                isSwiped = true;
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSwiped = false;
        }
    }

    private void HorizontalSwipe(float distanceX)
    {
        if (distanceX > 0)
        {
            playerController.isRight = true;
        }
        if (distanceX < 0)
        {
            playerController.isLeft = true;
        }
    }

    private void VerticalSwipe(float distanceY)
    {
        if (distanceY > 0)
        {
            playerController.isUp = true;
        }
    }
}
