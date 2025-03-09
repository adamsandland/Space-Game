using UnityEngine;

public class CameraAI : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f; // Distance behind the player
    public float followHeight = 2f; // Height above the player
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Calculate target position behind the player
        Vector3 targetPosition = player.position - player.forward * followDistance + Vector3.up * followHeight;

        // Move towards target position smoothly
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Apply velocity only if not already at the target position
        if (distance > 20f)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Rotate smoothly to always look at the player
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
    }
}
