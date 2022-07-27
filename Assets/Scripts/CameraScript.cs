using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z) + offset;
       
    }
}
