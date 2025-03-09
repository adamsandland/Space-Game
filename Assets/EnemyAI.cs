using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float detectionAngle = 10f;
    public float minSpeedFactor = 0.3f; // Minimum speed factor when rotating
    private Transform target;
    private float Health = 80f;

    private Rigidbody rb;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            target = null;
            if((player.position-transform.position).magnitude<150){
                target = player;
            }
            RotateTowardsTarget();
            MoveTowardsTarget();
            TryToShoot();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, toTarget);
        float speedFactor = Mathf.Lerp(1f, minSpeedFactor, angle / 180f); // Slow down as angle increases
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    void RotateTowardsTarget()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
    }

    void TryToShoot()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, toTarget);

        if (angle < detectionAngle && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player_Bullet"){
            Health-=15f;
        }else if(collision.gameObject.tag == "Player"){
            Health=0f;
        }
    }
}