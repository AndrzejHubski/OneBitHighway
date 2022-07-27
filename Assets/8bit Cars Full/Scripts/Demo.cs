using UnityEngine;

public class Demo : MonoBehaviour
{
    CarFactory cf;

    void Start()
    {
        cf = gameObject.GetComponent<CarFactory>();

        float incX = .85f;
        float incY = 1.5f;
        float posX = -8.1f;
        float posY = 3f;
        float rot = -45f;

        // generate five rows of cars
        for (int r = 0; r < 5; r++)
        {
            // generate twenty cars in each row
            for (int i = 0; i < 20; i++)
            {
                cf.GenerateCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
                posX += incX;
                rot -= 5f;
            }
            posX = -8.1f;
            posY -= incY;
            rot = -45f;
        }
    }
}