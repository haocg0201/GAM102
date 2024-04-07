using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // di chuyen den A sau do nghi 2s, quay dau va di den diem B, lai nghi 2s va nguoc lai
    // speed 5
    // thay nguoi choi(mat huong phia player) => di chuyen ve phia nguoi choi
    // nang cao: húc trượt, đứng đơ 2s sau đó quay lại thấy người chơi lại húc

    public Transform boar;
    public Transform posA;
    public Transform posB;

    float speedForce = 3f;
    bool isFaceRight; // default=false
    public SpriteRenderer sr;
    public Animator anim;
    float timer;
    public bool atk;
    // huong cua boar
    Vector2 direction;
    public LayerMask playerLayer;
    public RaycastHit2D hitPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform != null && boar != null)
        {
            if (isFaceRight) // >> B
                    {
                        direction = Vector2.right;
                        if(Vector2.Distance(boar.position, posB.position) > 0.1f)
                        {
                            boar.position = Vector3.MoveTowards(boar.position,posB.position, speedForce * Time.deltaTime);
                            anim.Play("Boar_Walk");
                        }
                        else
                        {
                            timer += Time.deltaTime;
                            anim.Play("Boar_Idle");
                            if (timer > 2f)
                            {
                                sr.flipX = false;
                                isFaceRight = false;
                                timer = 0;
                            }
                
                        }
                    }
                    else // << A
                    {
                        direction = Vector2.left;
                        if (Vector2.Distance(boar.position, posA.position) > 0.1f)
                        {
                            boar.position = Vector3.MoveTowards(boar.position, posA.position, speedForce * Time.deltaTime);
                            //Vector3 targetPosition = posA.position; // Đích đến mà bạn muốn di chuyển tới
                            //Vector3 newPosition = Vector3.MoveTowards(boar.position, targetPosition, speedForce * Time.deltaTime);
                            //boar.position = newPosition;
                            anim.Play("Boar_Walk");

                        }
                        else
                        {
                            timer += Time.deltaTime;
                            anim.Play("Boar_Idle");
                            if (timer > 2f)
                            {
                                sr.flipX = true;
                                isFaceRight = true;
                                timer = 0;
                            }
                
                        }
                    }

                    RayDetect();
        }
        
    }

    public void RayDetect()
    {
        hitPlayer = Physics2D.Raycast(boar.position + Vector3.up, direction, 5f, playerLayer);
        if (hitPlayer) {
            //Debug.DrawRay(boar.position + Vector3.up,direction * hitPlayer.distance, Color.red);
        }
        else
        {
            //Debug.DrawRay(boar.position + Vector3.up, direction * 10f, Color.green);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Scanner"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
