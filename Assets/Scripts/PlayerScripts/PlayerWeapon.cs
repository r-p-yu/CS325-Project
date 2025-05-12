using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;

    public float smgDamage = 5f;
    public float smgFireRate = 0.1f;

    public float arDamage = 15f;
    public float arFireRate = 0.3f;

    public float heavyRifleDamage = 50f;
    public float heavyRifleFireRate = 1.0f;

    private float currentDamage;
    private float currentFireRate;
    private float nextFireTime;
    private bool isInitialized = false;

    void Start()
    {
        string selectedWeapon = GameManager.Instance.selectedWeaponType;

        switch (selectedWeapon)
        {
            case "SMG":
                currentDamage = smgDamage;
                currentFireRate = smgFireRate;
                break;
            case "AR":
                currentDamage = arDamage;
                currentFireRate = arFireRate;
                break;
            case "HeavyRifle":
                currentDamage = heavyRifleDamage;
                currentFireRate = heavyRifleFireRate;
                break;
            default:
                Debug.LogWarning("Unknown weapon type selected.");
                currentDamage = arDamage;
                currentFireRate = arFireRate;
                break;
        }

        isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentFireRate;
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 20f, ForceMode.Impulse);

        if (proj.TryGetComponent(out Projectile p))
        {
            p.SetDamage(currentDamage);
        }
    }
}
