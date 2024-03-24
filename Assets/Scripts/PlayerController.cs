using System.Collections;
using System.Collections.Generic;
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
    private Rigidbody2D Rigidbody2D;
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

    public List<AudioClip> audioClips;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpSlider.maxValue = hpMax;
        hpSlider.value = hpMax;
        speedForce =  3f;
        jumpForce = 8f;
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > 0)
        {
            timer-= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 3;
            speedForce += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                audioSource.PlayOneShot(audioClips[0]);
                //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
            }
            
            if(!isGround && isDoubleJump)
            {
                audioSource.PlayOneShot(audioClips[0]);
                //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                isDoubleJump = false;
            }
        }

        
    }

    private void FixedUpdate()
    {
        Vector3 po = gameObject.transform.position;
        canvas.transform.position = new Vector3(po.x,po.y + 1.5f,po.z);
        Rigidbody2D.velocity = new Vector2(speedForce, Rigidbody2D.velocity.y);
        //OverlapCheckGround();
        RaycastCheckGround();
        RayDetect();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            hpSlider.value -= 100;
            audioSource.PlayOneShot(audioClips[4]);
            if (hpSlider.value <= 300)
            {
                // Thiết lập màu sắc
                fillColor.changeImageColor(Color.red);
            }
            else if (hpSlider.value > 300 && hpSlider.value <= 700)
            {
                fillColor.changeImageColor(Color.yellow);
            }
            Destroy(collision.gameObject);
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

        if (collision.gameObject.tag == "Boar")
        {
            hpSlider.value -= 100;
            audioSource.PlayOneShot(audioClips[4]);
            if (hpSlider.value <= 300)
            {
                // Thiết lập màu sắc
                fillColor.changeImageColor(Color.red);
            }
            else if (hpSlider.value > 300 && hpSlider.value <= 700)
            {
                fillColor.changeImageColor(Color.yellow);
            }
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
        }
        else
        {
            isGround = false;
        }
    }

    public void RayDetect()
    {
        RaycastHit2D hitSomething = Physics2D.Raycast(transform.position + Vector3.right * 2f, Vector3.right, 5f);
        if(hitSomething)
        {
            if (hitSomething.collider.CompareTag("Boar")){
                Debug.DrawRay(transform.position, Vector3.right * hitSomething.distance, Color.yellow);
                //Debug.Log("Chạm Boar");
            }

            if (hitSomething.collider.CompareTag("Ground"))
            {
                Debug.DrawRay(transform.position, Vector3.right * hitSomething.distance, Color.blue);
                //Debug.Log("Chạm Ground đứng");
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * 5f, Color.black);
            //Debug.Log("Nothing");
        }
        
    }
}
