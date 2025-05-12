using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position,
                               Camera.main.transform.forward,
                               out hit))
            {
                if (hit.collider.gameObject == gameObject) // If hit this enemy
                {
                    spawner?.EnemyDefeated();
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spawner?.EnemyDefeated();
            Destroy(gameObject); // Destroy enemy
            Destroy(collision.gameObject); // Destroy bullet
        }
    }
}
