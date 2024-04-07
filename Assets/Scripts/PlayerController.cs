using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private Rigidbody2D rb;
    private Animator animator;
    public float speedForce;
    public float jumpForce;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int hpMax = 1000;
    public UnityEngine.UI.Slider hpSlider;
    [SerializeField] private Canvas canvas;
    [SerializeField] private FillImageBehaviourScript fillColor;
    public bool isGround;
    public bool isDoubleJump;
    public float timer;
    public Transform checkGroundPos;
    public LayerMask groundLayer;
    public Transform raycastPos;
    public float raycastTimer;
    public float bulletTimeRaining;

    public List<AudioClip> audioClips;
    AudioSource audioSource;
    float horizontalMove;
    RaycastHit2D hitSomething;
    public GameObject bulletRain;
    public bool isRainingBullet;
    public GroundCreator groundCreator;
    public GameObject cointBullet;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpSlider.maxValue = hpMax;
        hpSlider.value = hpMax;
        speedForce =  3f;
        jumpForce = 8f;
        timer = 3f;
        raycastTimer = 0;
        bulletTimeRaining = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        
        if (timer > 0)
        {
            timer-= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 3;
            speedForce += 0.2f;
            isRainingBullet = true;
        }

        if (bulletTimeRaining > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (bulletTimeRaining < 0)
        {
            bulletTimeRaining = 5f;
            isRainingBullet = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                audioSource.PlayOneShot(audioClips[0]);
                //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("isRunning", !isGround);
            }
            
            if(!isGround && isDoubleJump)
            {
                audioSource.PlayOneShot(audioClips[0]);
                //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isDoubleJump = false;
                animator.SetBool("isRunning", isGround);
            }
        }

        if (Input.GetKeyDown (KeyCode.Return))
        {
            RayDetect();
            raycastTimer += Time.deltaTime;

            if (raycastTimer >= 1f)
            {
                Debug.DrawRay(raycastPos.position, transform.position + Vector3.right * 10f, Color.clear);
                raycastTimer = 0;
            }         
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(gameManager.GetScore() > 0)
            {
                Instantiate(cointBullet, raycastPos.position, Quaternion.identity);
                gameManager.SubtractScore();
                gameManager.SetTextScore();
            }
        }

        //if (isRainingBullet)
        //{
        //    int randomBullet = UnityEngine.Random.Range(5, 16);
        //    for (int i = 0; i < randomBullet; i++)
        //    {
        //        float randomPosBulletRain = UnityEngine.Random.Range(0.5f, 3.5f);
        //        Instantiate(bulletRain, new Vector3(transform.position.x + 5f + randomPosBulletRain, 5f, 0), Quaternion.identity);
        //    }
        //    isRainingBullet = false;
        //}

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        // c1: xóa bằng thêm bên list khi sinh ra và xóa nó đi
        // c2: xóa bằng tìm thên theo tag (cách này cần set tất cả monster cfung tag hoặc tìm từng tag rồi xóa dần dần)

        //}
    }

    private void FixedUpdate()
    {
        //Vector3 po = gameObject.transform.position;
        //canvas.transform.position = new Vector3(po.x,po.y + 1.5f,po.z);
        //rb.velocity = new Vector2(horizontalMove * speedForce, rb.velocity.y);
        rb.velocity = new Vector2(speedForce, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);      
        RaycastCheckGround();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            culHP(200);
            audioSource.PlayOneShot(audioClips[4]);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Boar")
        {
            culHP(200);
            audioSource.PlayOneShot(audioClips[4]);
        }

        if (collision.gameObject.tag == "Plant")
        {
            culHP(200);
            audioSource.PlayOneShot(audioClips[4]);
        }

        if (collision.gameObject.tag == "BBoar")
        {
            culHP(200);
            audioSource.PlayOneShot(audioClips[4]);
        }
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            gameManager.AddScore();
            gameManager.SetTextScore();
            gameManager.SaveGame();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("DeadZone"))
        {
            culHP(1000);
            audioSource.PlayOneShot(audioClips[4]);
        }
        
        if (collision.CompareTag("Heart"))
        {
            culHP(-200);
            Destroy(collision.gameObject);
        }
    }

    public void culHP(int eHP)
    {
        hpSlider.value -= eHP;
        if (hpSlider.value <= 300)
        {
            // Thiết lập màu sắc
            fillColor.changeImageColor(Color.red);
        }
        else if (hpSlider.value > 300 && hpSlider.value <= 700)
        {
            fillColor.changeImageColor(Color.yellow);
        }
        else
        {
            fillColor.changeImageColor(Color.green);
        }

        if(hpSlider.value <= 0)
        {
            Time.timeScale = 0;
            gameManager.PlayerDead();
        }
    }

    public void OverlapCheckGround()
    {
        // học về overlap
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(checkGroundPos.position, 0.5f, groundLayer); // vi tri, dien tich vung , phải có groundLayer kia nó mới trả về mảng, nếu ko nó trả về 0
        if(collider2Ds.Length > 0)
        {
            isGround = true;
            
        }
        else
        {
            isGround = false;
        }
    }

    public void RaycastCheckGround()
    {
        //raycast không được chạy qua vật cần check mà vừa đủ để nó chạm vào, nếu ko nó không check được
        RaycastHit2D hitGround = Physics2D.Raycast(checkGroundPos.position, Vector2.down, 0.5f, groundLayer); // chung ta thong thuong chi su dung loai 2 va 4
        if (hitGround)
        {
            isGround = true;
            isDoubleJump = true;
            animator.SetBool("isRunning", isGround);
        }
        else
        {
            isGround = false;
        }
    }

    public void RayDetect()
    {
        hitSomething = Physics2D.Raycast(raycastPos.position + Vector3.right * 2f, Vector3.right, 10f);
        Debug.DrawRay(raycastPos.position, Vector3.right * 10f, Color.green);
        if (hitSomething)
        {
            if (hitSomething.collider.CompareTag("Boar") || hitSomething.collider.CompareTag("CBBoar"))
            {
                Debug.DrawRay(raycastPos.position, Vector3.right * 10f, Color.red);
                Debug.Log("Chạm Boar");
                Destroy(hitSomething.collider.gameObject);
            }

            if (hitSomething.collider.CompareTag("Plant"))
            {
                Debug.DrawRay(raycastPos.position, Vector3.right * hitSomething.distance, Color.red);
                Debug.Log("Chạm Plant");
                Destroy(hitSomething.collider.gameObject);
            }

            if (hitSomething.collider.CompareTag("Ground"))
            {
                Debug.DrawRay(raycastPos.position, Vector3.right * hitSomething.distance, Color.blue);
                Debug.Log("Chạm Ground đứng");
                Destroy(hitSomething.collider.gameObject);
            }
        }
        //else
        //{
        //    Debug.DrawRay(transform.position, Vector3.right * 5f, Color.green);
        //    //Debug.Log("Nothing");
        //}
    }
}
