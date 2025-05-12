using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float jump = 2.3f;
    private float speed = 350;
    [Range(0, 1)][SerializeField] private float crouchSpeed = .5f;
    //[SerializeField] private bool airMovement = true;
    private float gravity = 4f;
    [SerializeField] private LayerMask floor;

    private bool isGrounded = true;
    private bool isJumping = false;
    private float JUMP_DURATION = 0.065f;
    private float jumpDuration = 0f;
    private bool isCrouching = false;
    private bool isClimbing = false;
    private bool facingLeft = false;
    private float groundDetectRadius = .7f;
    //private float standDetectRadius = 0.01f;
    private float invulnerabilityDuration = 0f;
    [SerializeField] private float INVULNERABILITY_TIME = 2f;
    private int health;
    private int MAX_HEALTH = 3;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Canvas ui;
    [SerializeField] GameObject sceneLoader;
    private Vector3 respawnPosition;
    private AudioSource music;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //jumpDuration = JUMP_DURATION;
        health = MAX_HEALTH;
        if (GameManager.instance.passedMusic != null)
        {
            music = gameObject.AddComponent<AudioSource>();
            music = AudioManager.instance.GetMusicPlayer(GameManager.instance.passedMusic, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, body.linearVelocityY);

        

        // Handle grounded flag
        isGrounded = CheckGrounded();

        if ((isGrounded || jumpDuration > 0) && (Input.GetButton("Jump") && !isJumping))
        {
            //body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            movement.y += jump;
            jumpDuration -= Time.deltaTime;
        }

        // Handle jump
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

        // Handle Crouching Flag
        if (Input.GetButton("Crouch") && isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        if (isCrouching)
        {
            movement.x *= crouchSpeed;
        }

        if (isGrounded)
        {
            isJumping = false;
        }

        // Ticks invulnerability down if player is invulnerable 
        if(invulnerabilityDuration > 0)
        {
            invulnerabilityDuration -= Time.deltaTime;
        }

        // Stuns player for one tenth of i-frames
        if(invulnerabilityDuration > INVULNERABILITY_TIME * .9f)
        {
            movement.x = 0;
            movement.y = 0;
        }


        //Debug.Log("invul dur = " + invulnerabilityDuration);

        //Print Functions to help with Debugging - Brvnson
        //Debug.Log("is grounded: " + isGrounded);
        //Debug.Log("is jumping: " + isJumping);
       
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

        // Apply movement to rigidbody
        body.linearVelocity = movement;
        HandleAnimations();

        //Debug.Log("y Velocity: " + body.linearVelocityY);
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
        animator.SetBool("damage", invulnerabilityDuration >= INVULNERABILITY_TIME * .9f);
        ui.GetComponent<StageUI>().UpdateHealth(health, MAX_HEALTH);
    }

    bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundDetectRadius);
        return Physics2D.Raycast(transform.position, Vector2.down, groundDetectRadius, floor);
    }

    //Visual Indicator for floor detection - Brvnson
    //void OnDrawGizmos()
    //{
    //    if (feetCollider == null)
    //    {
    //        return;
    //    }
    //    Vector2 rayOrigin = (Vector2)transform.position + feetCollider.offset;
    //    Gizmos.color = isGrounded ? Color.green : Color.red;
    //    Gizmos.DrawRay(rayOrigin, Vector2.down * groundDetectRadius);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }


    // Lowers player health when hurt and makes player invulnerable - Brvnson
    public void Hurt()
    {
        if (invulnerabilityDuration <= 0)
        {
            health -= 1;
            invulnerabilityDuration = INVULNERABILITY_TIME;
            //StartCoroutine("MakeInvincible");

            if (health == 0)
            {
                sceneLoader.GetComponent<SceneLoader>().LoadNextScene(GameManager.gameOverIndex);
            }
        }
    }

    //IEnumerator MakeInvincible()
    //{
    //    Physics2D.IgnoreLayerCollision(7, 8, true);
    //    yield return new WaitForSeconds(3f);
    //    Physics2D.IgnoreLayerCollision(7, 8, false);
    //}


    void OnTriggerEnter2D(Collider2D other)
    {
        Collectible collectible = other.gameObject.GetComponent<Collectible>();
        if (collectible != null)
        {
            if (collectible.GetCollectType() == Collectible.CollectType.Cherry)
            {
                GameManager.instance.cherries += 1;
                collectible.Collect();
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Heart)
            {
                if (health < MAX_HEALTH) { health += 1; }
                else { GameManager.instance.cherries += 10; }
                collectible.Collect();
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Diamond)
            {
                if (!collectible.IsCollected())
                {
                    GameManager.instance.diamonds += 1;
                    GameManager.instance.collectedDiamonds.Add(collectible.GetDiamondUID());
                }
                else { GameManager.instance.cherries += 20; }
                collectible.Collect();
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Flag)
            {
                respawnPosition = collectible.gameObject.transform.position;
                collectible.gameObject.GetComponent<Animator>().SetBool("activated", true);
                return;
            }

            if (collectible.GetCollectType() == Collectible.CollectType.Goal)
            {
                sceneLoader.GetComponent<SceneLoader>().LoadNextScene(GameManager.gameVictoryIndex);
                return;
            }

            while (GameManager.instance.cherries >= 100)
            {
                GameManager.instance.cherries -= 100;
                GameManager.instance.lives += 1;
            }
        
        }
    }

}
