using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isTest;

    public int lives = 3, charges = 2;
    public float sideSpeed = 5, laneDistance = 1, speed = 5; 
    private float currentSpeed, rotationSpeed = 1;
    private int targetNumber, currentNumber;
    private float[] positions = new float[4];
    [HideInInspector] public bool isRight, isLeft, isUp;
    private bool isBored, isBoost, isDamaged;
    public GameObject spawner, gameOverPanel, revivePanel;

    private bool isFirstLose = true;

    public ScoreManager scoreManager;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public Text scoreText;

    public Collider2D carCollider;

    public GameObject[] hearts, boosts;

    public Animator cameraAnimator, playerAnimator, swipeAnimator;
    public GameObject swipeTutorial;
    public Text tutorialText;

    public AddsManager addsManager;
    public ShowingAd showingUnityAd;
    public AdMobScripts adMobScripts;

    public int id;

    public void Awake()
    {
        id = PlayerPrefs.GetInt("id");
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[id];

        gameOverPanel.SetActive(false);

        float currentLane = -laneDistance * 1.5f;
        GameObject cam = GameObject.Find("Main Camera");

        currentSpeed = speed;
        rotationSpeed = sideSpeed * 3;

        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = currentLane;
            if (isTest == false)
            {
                Instantiate(spawner, new Vector3(currentLane, transform.position.y + 10, 0), Quaternion.identity, cam.transform);
            }
            currentLane += laneDistance;
        }

        currentNumber = Random.Range(0, positions.Length);
        targetNumber = currentNumber;

        transform.position = new Vector3(positions[currentNumber], transform.position.y, 0);

        if(isTest == true)
        {
            currentNumber =1;
            targetNumber = currentNumber;
            transform.position = new Vector3(positions[1], transform.position.y, 0);
        }
    }

    private void Update()
    {
        if (isTest == true && (isRight == true || isLeft == true))
        {
            currentSpeed = speed;
            swipeAnimator.SetBool("swipeRight", false);
            swipeTutorial.SetActive(false);
            tutorialText.text = "";
        }

        switch (lives)
        {
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
        }

        switch (charges)
        {
            case 0:
                boosts[0].SetActive(false);
                boosts[1].SetActive(false);
                boosts[2].SetActive(false);
                break;
            case 1:
                boosts[0].SetActive(true);
                boosts[1].SetActive(false);
                boosts[2].SetActive(false);
                break;
            case 2:
                boosts[0].SetActive(true);
                boosts[1].SetActive(true);
                boosts[2].SetActive(false);
                break;
            case 3:
                boosts[0].SetActive(true);
                boosts[1].SetActive(true);
                boosts[2].SetActive(true);
                break;
        }

        transform.position += new Vector3(0, currentSpeed * Time.deltaTime, 0);

        if (isBored == false)
        {
            LaneChange();
        }

        if(isUp == true && isBoost == false && charges >= 1)
        {
            StartCoroutine(Boost());
            isUp = false;
        }

        PlayerMovement();
    }

    private void LaneChange()
    {
        if (isRight && targetNumber < positions.Length - 1)
        {
            targetNumber++;
            isRight = false;
            isLeft = false;
            isBored = true;
        }
        if (isLeft && targetNumber > 0)
        {
            targetNumber--;
            isLeft = false;
            isRight = false;
            isBored = true;
        }
    }

    private void PlayerMovement()
    {
        int rotationDirection = 0;
        float positionX = transform.position.x;
        float targetDistance = positionX - positions[targetNumber];
        float distanceAbs = Mathf.Abs(targetDistance);
        float distanceToPoint = Mathf.Abs(positions[targetNumber] - positions[currentNumber]);
    
        

        if (positions[targetNumber] > positionX)
        {
            rotationDirection = -1;
        }
        else if (positions[targetNumber] < positionX)
        {
            rotationDirection = 1;
        }

        if (distanceAbs < distanceToPoint / 2)
        {
            rotationDirection = -rotationDirection;
        }

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * rotationDirection);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(positions[targetNumber], transform.position.y), sideSpeed * 0.1f * Time.deltaTime);

        if (transform.position.x == positions[targetNumber])
        {
            isBored = false;
            currentNumber = targetNumber;
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void Reviving()
    {
        revivePanel.SetActive(true);
    }

    public void Revive()
    {
        isFirstLose = false;
        revivePanel.SetActive(false);
        carCollider.enabled = true;
        spriteRenderer.color = new Color(255, 255, 255, 255);
        lives = 3;
        charges = 3;
        StartCoroutine(Invincible());
    }

    public void GameOver()
    {
        revivePanel.SetActive(false);
        carCollider.enabled = false;
        spriteRenderer.color = new Color(0,0,0,0);
        gameOverPanel.SetActive(true);
        if (scoreManager.score > PlayerPrefs.GetInt("highscore"))
        {
            scoreText.text = "NEW HIGHSCORE \n" + scoreManager.score.ToString();
            PlayerPrefs.SetInt("highscore", scoreManager.score);
        }
        else
        {
            scoreText.text = "SCORE \n" + scoreManager.score.ToString();
        }

        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + scoreManager.score);

        if(addsManager.skippableAdd == true)
        {
            addsManager.skippableAdd = false;
            if(PlayerPrefs.GetInt("adUnity") == 1)
            {
                showingUnityAd.ShowAd();
                PlayerPrefs.SetInt("adUnity", 0);
            }
            else
            {
                adMobScripts.ShowInterstitial();
                PlayerPrefs.SetInt("adUnity", 1);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            if (lives <= 1 && isBoost == false)
            {
                if (isFirstLose == true)
                {
                    Reviving();
                }
                else
                {
                    GameOver();
                }
            }
            else if(isBoost == true)
            {
                collision.gameObject.GetComponent<CarScript>().Destroying();
            }
            else if(isDamaged == false)
            {
                lives--;
                StartCoroutine(Invincible());
                collision.gameObject.GetComponent<CarScript>().Destroying();
            }
        }
        else if(collision.gameObject.CompareTag("SwipeLine"))
        {
            currentSpeed = 0;
            swipeTutorial.SetActive(true);
            swipeAnimator.SetBool("swipeRight", true);
            tutorialText.text = "swipe right";
        }
        else if (collision.gameObject.CompareTag("SwipeUpLine"))
        {
            currentSpeed = 0;
            swipeTutorial.SetActive(true);
            swipeAnimator.SetBool("swipeUp", true);
            tutorialText.text = "swipe up";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            if (lives <= 1 && isBoost == false)
            {
                if(isFirstLose == true)
                {
                    Reviving();
                }
                else
                {
                    GameOver();
                }
            }
            else if (isBoost == true)
            {
                collision.gameObject.GetComponent<CarScript>().Destroying();
            }
            else if (isDamaged == false)
            {
                lives--;
                StartCoroutine(Invincible());
                collision.gameObject.GetComponent<CarScript>().Destroying();
            }
        }
    }

    private IEnumerator Boost()
    {
        isBoost = true;
        cameraAnimator.SetTrigger("StartBoost");
        charges--;
        currentSpeed = speed * 2;
        if(isTest == true)
        {
            swipeTutorial.SetActive(false);
            swipeAnimator.SetBool("swipeUp", false);
            tutorialText.text = "GOOD LUCK";
        }
        yield return new WaitForSeconds(1f);
        cameraAnimator.SetTrigger("EndBoost");
        currentSpeed = speed;
        yield return new WaitForSeconds(0.7f);
        isBoost = false;
        if(isTest == true)
        {
            PlayerPrefs.SetInt("tutorial", 1);
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator Invincible()
    {
        isDamaged = true;
        playerAnimator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(1);
        isDamaged = false;
        playerAnimator.SetBool("isDamaged", false);
    }
}
