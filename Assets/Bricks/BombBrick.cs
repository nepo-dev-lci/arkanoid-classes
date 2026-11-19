using UnityEngine;

public class BombBrick : BaseBrick
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2.5f;
    public float ballPushForce = 15f;
    public LayerMask brickLayer;

    public override void TakeHit()
    {
        Debug.Log("hit detected");
        Explode();
    }

    private void Explode()
    {
        DeleteSurroundingBricks();
        PushBallAway();
        DestroyBrick();
    }

    private void DeleteSurroundingBricks()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, brickLayer);
        foreach (Collider2D hit in hits)
        {
            BaseBrick brick = hit.GetComponent<BaseBrick>();
            if (brick != null && brick != this)
            {
                brick.TakeHit();
            }
        }
    }

    private void PushBallAway()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                Vector2 pushDirection = (ball.transform.position - transform.position).normalized;
                ballRb.AddForce(pushDirection * ballPushForce, ForceMode2D.Impulse);
            }
        }
    }

    protected override void DestroyBrick()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}