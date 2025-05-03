using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float jump = 1f;
    [SerializeField] private float speed = 4f;
    [Range(0, 1)][SerializeField] private float crouchSpeed = .5f;
    [SerializeField] private bool airMovement = true;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private LayerMask floor;

    private bool isGrounded = true;
    private bool isJumping = false;
    [SerializeField] private float JUMP_DURATION = 0.5f;
    private float jumpDuration = 0.5f;
    private bool isCrouching = false;
    private bool isClimbing = false;
    private bool facingLeft = false;
    private float groundDetectRadius = .7f;
    private float standDetectRadius = 0.01f;
    private float invulnerabilityDuration = 0f;
    private int health = 2;
    private int MAX_HEALTH = 3;


    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Canvas ui;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //health = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, body.linearVelocityY);

        if ((isGrounded || jumpDuration > 0) && (Input.GetButton("Jump") && !isJumping))
        {
            //body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            movement.y += jump;
            jumpDuration -= Time.deltaTime;
        }


        isGrounded = checkGrounded();

        if (isGrounded)
        {
            jumpDuration = JUMP_DURATION;
            isJumping = false;
        }
        else if (!isGrounded && !Input.GetButton("Jump"))
        {
            isJumping = true;
        }

        if (!isGrounded && isJumping)
        {
            movement.y -= gravity * Time.deltaTime;
        }

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
        HandleAnimations();
    }

    void HandleAnimations()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(body.linearVelocityX));
        animator.SetFloat("fallSpeed", body.linearVelocityY);
        animator.SetBool("grounded", isGrounded);
        animator.SetBool("crouching", isCrouching);
        animator.SetBool("climbing", isClimbing && body.linearVelocityY != 0);
        animator.SetBool("onLadder", isClimbing);
        animator.SetBool("damage", invulnerabilityDuration >= 0.3f);
        ui.GetComponent<StageUI>().UpdateHealth(health, MAX_HEALTH);
    }

    bool checkGrounded()
    {
        Debug.DrawLine(transform.position,transform.position + Vector3.down * groundDetectRadius);
        return Physics2D.Raycast(transform.position, Vector2.down, groundDetectRadius, floor);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Collectible collectible = other.gameObject.GetComponent<Collectible>();
        if (collectible != null)
        {
            if (collectible.GetCollectType() == Collectible.CollectType.Cherry)
            {
                GameManager.instance.cherries += 1;
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Heart)
            {
                if (health < MAX_HEALTH) { health += 1; }
                else { GameManager.instance.cherries += 10; }
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Diamond)
            {
                if (!collectible.IsCollected()) { GameManager.instance.diamonds += 1; }
                else { GameManager.instance.cherries += 20; }
            }

            while (GameManager.instance.cherries >= 100)
            {
                GameManager.instance.cherries -= 100;
                GameManager.instance.lives += 1;
            }

            // Have the object play the collection animation then destroy itself as a coroutine
            Destroy(other.gameObject);
        }
    }

}
