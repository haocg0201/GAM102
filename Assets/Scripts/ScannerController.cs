using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Plant"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("BBoar"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Boar"))
        {
            Destroy(collision.gameObject);
        }
    }
}
