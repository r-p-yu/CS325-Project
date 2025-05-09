using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera cam;
    public float heightOffset;
    public Shop shopkeep;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;    
        Vector3 adjustment = new Vector3(0, heightOffset, 0);
        transform.position = shopkeep.transform.position + adjustment;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
