using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.left*1.1f ,ForceMode2D.Impulse);
        Destroy(gameObject,1f);
    }
}
