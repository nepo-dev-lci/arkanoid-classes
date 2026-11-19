using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float minX = -7f;
    public float maxX = 7f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(horizontal * moveSpeed, 0);
        Vector2 newPosition = rb.position + movement * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        rb.MovePosition(newPosition);
    }
}