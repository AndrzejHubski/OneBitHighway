using UnityEngine;

public class TrafficDemo : MonoBehaviour {

    public CarFactory cf;

	// Use this for initialization
	void Start () {
        cf = gameObject.GetComponent<CarFactory>();

        // generate cars west to east
        float incX = 1.114f;
        float posX = -8.36f;
        float posY = -.35f;
        float rot = 0f;

        for (int i=0; i<16; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posX += incX;
        }

        // generate cars east to west
        incX = 1.114f;
        posX = 8.36f;
        posY = .35f;
        rot = 180f;

        for (int i = 0; i < 16; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posX -= incX;
        }

        // generate cars north to south (top)
        float incY = 1.114f;
        posX = -.35f;
        posY = 4.49f;
        rot = -90f;

        for (int i = 0; i < 4; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posY -= incY;
        }

        // generate cars north to south (bottom)
        posY = -1.114f;

        for (int i = 0; i < 4; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posY -= incY;
        }

        // generate cars south to north (bottom)
        posX = .35f;
        posY = -4.49f;
        rot = 90f;

        for (int i = 0; i < 4; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posY += incY;
        }

        // generate cars south to north (top)
        posY = 1.114f;

        for (int i = 0; i < 4; i++)
        {
            cf.GenerateRandomCar(new Vector2(posX, posY), Quaternion.Euler(0f, 0f, rot));
            posY += incY;
        }
    }
}