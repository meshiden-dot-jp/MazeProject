using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform Follow;
    public Vector3 Offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void LateUpdate()
    {
        transform.position = Follow.position + Offset;
        transform.LookAt(Follow);
    }
}
