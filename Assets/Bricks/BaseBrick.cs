using UnityEngine;

// This is the base class. All other brick types will inherit from this
public class BaseBrick : MonoBehaviour
{
    [Header("Base Brick Settings")]
    public int maxHealth = 1;

    protected int currentHealth;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // This method is called when the ball collides with the brick
    // It is virtual so subclasses can override how damage is handled
    public virtual void TakeHit()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            DestroyBrick();
            return;
        }

        // vfx briefly flash red on hit
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke(nameof(ResetColor), 0.05f);
        }

    }

    protected void ResetColor()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    // virtual destroy method so that any brick can execute whatever logic they want before being destroyed
    protected virtual void DestroyBrick()
    {
        Destroy(gameObject);
    }

    // Used by bomb brick to destroy surrounding bricks
    public void ForceDestroy()
    {
        DestroyBrick();
    }
}
