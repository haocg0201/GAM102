using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarLeft : MonoBehaviour
{
    Rigidbody2D rb;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnBecameVisible() // event này được  gọi khi mà đối tượng chứa script này xuất hiện trên camera
    {
        active = true;
    }

    private void OnBecameInvisible() // event này được  gọi khi mà đối tượng chứa script này không xuất hiện trên camera
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            rb.velocity = Vector2.left * 2f;
        }
        
    }
}
