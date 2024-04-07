using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    // Mỗi 3s bắn 1 viên đạn, đạn sinh từ miệng nó
    // Viên đạn ra ngoài màn hình thì bị xóa
    // Nhân vật chạy qua nó thì xóa nó khỏi game
    // Sinh ra ở vị trí ngẫu nhiên
    public Transform bulletPos;
    public GameObject bullet;
    Animator animator;
    bool isAttack;
    AudioSource source;
    public AudioClip shootSound;
    public LayerMask playerMark;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // có thể sử dụng timer += Time.deltaTime; cũng tương tự 

        
        if (isAttack)
        {
            for ( int i = 0;i <= 2; i++)
            {
                animator.SetTrigger("atk");              
            }
        }      
    }

    // FixUpdate is called once per 0.02s
    private void FixedUpdate() 
    {
        RaycastDetectPlayer();
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        source.PlayOneShot(shootSound);
    }

    //IEnumerator AttackEneble()
    //{
    //    yield return new WaitForSeconds(2f);
    //}

    public void RaycastDetectPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(bulletPos.position, Vector2.left, 5f, playerMark);
        if (hitPlayer)
        {
            //Debug.DrawRay(bulletPos.position, Vector2.left * hitPlayer.distance, Color.red); // hitPlayer.distance: noi cham vao tia
            isAttack = true;
        }
        else
        {
            //Debug.DrawRay(bulletPos.position, Vector2.left * 5f, Color.green);
            isAttack = false;
        }
    }
}
