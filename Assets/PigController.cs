using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveForce = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.A))
        {
            turnLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            turnRight();
        }

        if(moveForce == 0)
        {
            rigidBody2D.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    public void turnLeft()
    {
        spriteRenderer.flipX = false;
       // rigidBody2D.AddForce(Vector2.left, ForceMode2D.Impulse);
       rigidBody2D.velocity = new Vector2(-1.0f, 0f) * 3;
    }

    public void turnRight()
    {
        spriteRenderer.flipX = true;
        //rigidBody2D.AddForce(Vector2.right, ForceMode2D.Impulse);
        rigidBody2D.velocity = new Vector2(1.0f, 0f) * 3;
    }

    public void jump()
    {
        //rigidBody2D.AddForce(Vector2.up, ForceMode2D.Impulse);
        rigidBody2D.velocity = new Vector2(0f, speed) * 5;
    }
}
