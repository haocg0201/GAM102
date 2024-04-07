using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.down * 0.2f, ForceMode2D.Impulse);
        Destroy(gameObject, 1f);
    }
}
