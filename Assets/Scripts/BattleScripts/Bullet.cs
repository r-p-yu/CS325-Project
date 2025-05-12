using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 50f;
    public float lifetime = 4f; 
    public int damage = 10;

    void Start()
    {
        // Automatically destroy bullet after lifetime expires
        Destroy(gameObject, lifetime);

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // If using trigger colliders
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // If using regular colliders
        Destroy(gameObject);
    }
}