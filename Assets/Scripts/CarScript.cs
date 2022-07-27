using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float carSpeed = 5;
    public GameObject particles;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    
    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        carSpeed = Random.Range(carSpeed * 0.5f, carSpeed * 1.5f);
    }

    private void Update()
    {
        transform.Translate(0, carSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision.transform.position.y > transform.position.y)
        {
            carSpeed = collision.gameObject.GetComponent<CarScript>().carSpeed;
        }
    }

    public void Destroying()
    {
        GameObject particleSystem = Instantiate(particles, transform.position, transform.rotation);
        particleSystem.SetActive(true);
        Destroy(gameObject);
    }
}
