using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public Transform cam;
    public float teleportPosition;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        if(cam.position.y - transform.position.y >= teleportPosition)
        {
            transform.position = new Vector3(0,cam.position.y, 0);
        }
    }
}
