using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.transform.Rotate(90, 0, 0);
        rb.linearVelocity = transform.up * bulletSpeed; // Set initial velocity relative to orientation
        Destroy(gameObject, lifetime); // Destroy bullet after set time
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if bullet hits the player
        {
            Destroy(gameObject); // Destroy bullet on impact
            // Optionally: Apply damage to player here
        }
    }
}
