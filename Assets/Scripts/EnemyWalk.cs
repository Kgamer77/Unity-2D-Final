using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BearWalk : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Transform[] points;
    public int targetPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[targetPoint].position) < 0.02f)
        {
            increaseTargetInt();
        }
        transform.position = Vector2.MoveTowards(transform.position, points[targetPoint].position, Speed * Time.deltaTime);

        if(targetPoint == 1)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if(targetPoint >= points.Length)
        {
            targetPoint = 0;
        }
    }
}



