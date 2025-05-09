using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera cam;
    public float heightOffset;
    public GameObject speaker;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;    
        Vector3 adjustment = new Vector3(0, heightOffset, 0);
        transform.position = speaker.transform.position + adjustment;
    }

    void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
