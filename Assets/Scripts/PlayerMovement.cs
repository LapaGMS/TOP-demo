using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    private Vector2 movement;

    public PartSelector[] parts;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void LateUpdate()
    {
        if(movement.x > 0)
        {
            SetFlip(false);
        }
        else if(movement.x < 0)
        {
            SetFlip(true);
        }
    }

    void SetFlip(bool flip)
    {
        foreach (var p in parts)
        {
            p.targetRenderer.flipX = flip;
            p.SetFlip(flip);
        }
    }

    public Vector2 GetMovement()
    {
        return movement;
    }
}
