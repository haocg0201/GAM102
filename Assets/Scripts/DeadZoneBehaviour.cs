using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneBehaviour : MonoBehaviour
{
    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, 0);
    }
}
