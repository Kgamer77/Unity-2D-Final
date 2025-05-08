using System;

using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float jump = 1f;
    [SerializeField] private float Speed = 4f;
    [Range(0, 1)][SerializeField] private float crouchSpeed = .5f;
    [SerializeField] private bool airMovement = true;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private LayerMask floor;
    [SerializeField] private CircleCollider2D feetCollider;

    private bool isGrounded = true;
    private bool isJumping = false;
    [SerializeField] private float JUMP_DURATION = 0.5f;
    private float jumpDuration = 0.5f;
    private bool isCrouching = false;
    private bool isClimbing = false;
    private bool facingLeft = false;
    private float groundDetectRadius = 1f;
    private float standDetectRadius = 0.01f;
    private float invulnerabilityDuration = 0f;
    private int health;
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
        //jumpDuration = JUMP_DURATION;
        health = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, body.linearVelocityY);

        if (Input.GetButton("Jump") && isGrounded)
        {
            //body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            jumpDuration -= Time.deltaTime;
            movement.y += jump;
            //Debug.Log("pressing jump");
            isJumping = true;
        }

        //Detects Down input for crouching, affects animation and added to movement function - Brvnson
        if (Input.GetAxis("Vertical") < 0)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        //Left and Right movement - Brvnson
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (!isCrouching)
            { movement.x = Speed; }
            else
            {
                movement.x = crouchSpeed;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (!isCrouching)
            { movement.x = -Speed; }
            else
            {
                movement.x = -crouchSpeed;
            }
        }
        else
        {
            movement.x = 0;
        }

        if (isGrounded)
        {
            isJumping = false;
        }

        //ticks invulnerability down is player is invulnerable (current amount = 2 sec of invulnerability - Brvnson
        if(invulnerabilityDuration > 0)
        {
            invulnerabilityDuration -= 0.1f;
        }
        else
        {
            invulnerabilityDuration = 0;
        }

        if(invulnerabilityDuration > 3f)
        {
            movement.x = 0;
            movement.y = 0;
        }


        //Debug.Log("invul dur = " + invulnerabilityDuration);

        //Print Functions to help with Debugging - Brvnson
        //Debug.Log("is grounded: " + isGrounded);
        //Debug.Log("is jumping: " + isJumping);
       
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

        //Debug.Log("y Velocity: " + body.linearVelocityY);
    }

    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Floor"))
    //    {
    //        isGrounded = true;
    //        isJumping = false;
    //        jumpDuration = JUMP_DURATION;
    //    }
    //}

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

    bool CheckGrounded()
    {
        Vector2 rayOrigin = (Vector2)transform.position + feetCollider.offset;
        Debug.DrawLine(transform.position,transform.position + Vector3.down * groundDetectRadius);

        return Physics2D.Raycast(rayOrigin, Vector2.down, groundDetectRadius, floor).collider != null;
    }

    //Visual Indicator for floor detection - Brvnson
    void OnDrawGizmos()
    {
        if (feetCollider == null)
        {
            return;
        }
        Vector2 rayOrigin = (Vector2)transform.position + feetCollider.offset;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(rayOrigin, Vector2.down * groundDetectRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }


    //Lowers player health when hurt and makes player invulnerable - Brvnson
    public void Hurt()
    {
        if (invulnerabilityDuration == 0)
        {
            //Health -= 2;
            invulnerabilityDuration = 60f;
            StartCoroutine("Invul");
        }
    }

    IEnumerator Invul()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(7, 8, false);
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
                if (!collectible.IsCollected()) 
                {
                    GameManager.instance.diamonds += 1;
                    GameManager.instance.collectedDiamonds.Add(collectible.GetDiamondUID());
                }
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
