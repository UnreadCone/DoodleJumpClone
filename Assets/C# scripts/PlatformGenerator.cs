using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public void MovePlatform()
    {
        Vector3 newPosition = new Vector3(Random.Range(-2, 2), Random.Range(-3, -4), 0f);
        Vector3 newRotation = new Vector3( 0f , 0f , Random.Range(-15, 15));
        platform.transform.position = newPosition;
        platform.transform.eulerAngles = newRotation;
    }
}