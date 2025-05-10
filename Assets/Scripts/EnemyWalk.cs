using UnityEngine;

public class BearWalk : MonoBehaviour
{
    [SerializeField] private float speed = 250f;
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;
    [SerializeField] private LayerMask floor;
    private float groundDetectRadius = 1f;
    private float wallDetectRadius = 0.6f;
    private float direction = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckGrounded() || CheckWall())
        {
            direction *= -1f;
        }

        Vector2 movement = new Vector2( direction * speed * Time.deltaTime, body.linearVelocityY);
        
        // Handles sprite flipping
        if (!facingLeft && movement.x < 0)
        {
            facingLeft = true;
        }
        else if (facingLeft && movement.x > 0)
        {
            facingLeft = false;
        }

        spriteRenderer.flipX = facingLeft;
        body.linearVelocity = movement;

    }

    bool CheckGrounded()
    {
        Debug.DrawLine(transform.position + new Vector3(direction,0,0) * groundDetectRadius,
            transform.position + new Vector3(direction, -1, 0) * groundDetectRadius);
        return Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0) * groundDetectRadius,
            Vector2.down, groundDetectRadius, floor);
    }

    bool CheckWall()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(direction, 0, 0) * wallDetectRadius);
        return Physics2D.Raycast(transform.position, new Vector3(direction, 0, 0), wallDetectRadius, floor);
    }

}



