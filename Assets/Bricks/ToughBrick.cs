using UnityEngine;

// A brick that takes 2 hits to break.
public class ToughBrick : BaseBrick
{
    protected override void Awake()
    {
        base.Awake();
        maxHealth = 2;
        currentHealth = maxHealth;
    }
}