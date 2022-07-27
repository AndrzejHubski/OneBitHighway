using UnityEngine;

public class CarFactory : MonoBehaviour {

    public Transform prefab;
    private int carNo = 1;

	public Transform GenerateCar(Vector2 position, Quaternion rotation)
    {
        string spriteName = "Car" + carNo.ToString().PadLeft(2,'0');

        // Loop through all one hundred cars in order
        carNo++;
        if (carNo > 100)
        {
            carNo = 1;
        }

        return InstantiateCar(position, rotation, spriteName);
    }

    public Transform GenerateRandomCar(Vector2 position, Quaternion rotation)
    {
        // pick a random car
        string spriteName = "Car" + UnityEngine.Random.Range(1, 100).ToString().PadLeft(2, '0');
        
        return InstantiateCar(position, rotation, spriteName);
    }

    private Transform InstantiateCar(Vector2 position, Quaternion rotation, string spriteName)
    {
        Transform car = Instantiate(prefab, position, rotation);

        SpriteRenderer sr = car.GetComponent<SpriteRenderer>();

        Sprite carSprite = Resources.Load<Sprite>("Sprites/" + spriteName);
        sr.sprite = carSprite;

        Texture2D carTexture = PaintCar(sr.sprite.texture);
        Sprite newSprite = Sprite.Create(carTexture, sr.sprite.rect, new Vector2(.505f, .5f));
        sr.sprite = newSprite;
        sr.sprite.name = spriteName;

        return car;
    }

    private Texture2D PaintCar(Texture2D textureCopy)
    {
        // pick a random RGB color
        byte colorR = (byte)UnityEngine.Random.Range(0, 255);
        byte colorG = (byte)UnityEngine.Random.Range(0, 255);
        byte colorB = (byte)UnityEngine.Random.Range(0, 255);

        // no need to change color if random color is white
        if (colorR==0 && colorG==0 && colorB==0)
        {
            return textureCopy;
        }
        else
        {
            Texture2D texture = new Texture2D(101, 50);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            // loop through each row of pixels
            int y = 0;
            while (y < texture.height)
            {
                // loop through each pixel in the row
                int x = 0;
                while (x < texture.width)
                {
                    if (textureCopy.GetPixel(x, y) == Color.white)
                    {
                        texture.SetPixel(x, y, new Color32(colorR, colorG, colorB, 255));
                    }
                    else
                    {
                        texture.SetPixel(x, y, textureCopy.GetPixel(x, y));
                    }
                    x++;
                }
                y++;
            }
            texture.Apply();

            return texture;
        }
    }
}