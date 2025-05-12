using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float mouseSens = 250f;
    public bool canMove = true;
    private float xRotation = 0f;
    private Rigidbody rb;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Weapon")]
    public Transform weaponHolder; // Empty GameObject for weapon positioning
    public GameObject weaponModel; // Your AK74 model

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        if (weaponModel)
            weaponModel.SetActive(true);
    }

    void Update()
    {
        if (canMove)
        {
            HandleMouseLook();
            HandleMovement();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        // Get pure camera forward direction (ignore pitch rotation)
        Vector3 forward = transform.forward; // Use player's forward, not camera's
        Vector3 right = transform.right;    // Use player's right, not camera's

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate movement with consistent speed
        Vector3 movement = (forward * Input.GetAxis("Vertical") * speed) +
                          (right * Input.GetAxis("Horizontal") * speed);

        rb.velocity = movement;
    }
}