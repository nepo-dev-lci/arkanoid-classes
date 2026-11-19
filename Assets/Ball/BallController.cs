using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float maxSpeed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Launch the ball in a random upward direction
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb.linearVelocity = direction * initialSpeed;
    }

    void Update()
    {
        Vector2 direction = rb.linearVelocity;
        direction.Normalize();
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * maxSpeed, .01f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit a brick
        BaseBrick brick = collision.gameObject.GetComponent<BaseBrick>();
        if (brick != null)
        {
            // This single line works for ALL brick types because of inheritance.
            // A BasicBrick will break. A ToughBrick will lose 1 health.
            // A BombBrick will override TakeHit() and explode.
            brick.TakeHit();
        }

        // Optional: Add a slight random angle on paddle hits to avoid endless loops
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Add a small nudge based on where it hit the paddle
            float hitOffset = transform.position.x - collision.transform.position.x;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + hitOffset * 2f, rb.linearVelocity.y);
        }
    }
}