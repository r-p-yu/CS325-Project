using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public Transform cameraTransform;
    public float mouseSens = 250f;
    float xRotation = 0f;
    Vector3 forward;
    Vector3 right;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //mouse-look camera
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            //movement based on camera direction 
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            right = cameraTransform.right;
            forward = cameraTransform.forward;

            forward.y = 0f;
            forward.Normalize();
            right.y = 0f;
            right.Normalize();

            Vector3 moveDirection = forward * vertical + right * horizontal;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
        
    }
}
