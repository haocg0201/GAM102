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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        isAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        // có thể sử dụng timer += Time.deltaTime; cũng tương tự 
        if (isAttack)
        {
            isAttack = false;
            animator.SetTrigger("atk");
            StartCoroutine(AttackEneble());
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        source.PlayOneShot(shootSound);
    }

    IEnumerator AttackEneble()
    {
        yield return new WaitForSeconds(2.5f);
        isAttack = true;
    }
}
