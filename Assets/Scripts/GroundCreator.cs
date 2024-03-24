using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    public List<GameObject> lstGround;
    public List<GameObject> lstOldGrounds;
    public List<GameObject> lstEnemy;
    Vector3 endPosition;
    Vector3 nextPosition;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = new Vector3(21f, 0f, 0f);
        for (int i = 0; i < 2; i++) 
        { 
            float groundD = Random.Range(3f, 4f);
            nextPosition = new Vector3(endPosition.x + groundD, 0f, 0f);
            int groundID = Random.Range(0, lstGround.Count);

            int enemyID = Random.Range(0, lstEnemy.Count);

            // Instantiate sinh Prefabs 
            GameObject nGround = Instantiate(lstGround[groundID], nextPosition, Quaternion.identity,transform);
            lstOldGrounds.Add(nGround);
            // Instantiate sinh enemy
            Instantiate(lstEnemy[enemyID], new Vector3(nextPosition.x + Random.Range(5f, GroundLength(groundID)), 0f, 0f), Quaternion.identity);

            endPosition = new Vector3(nextPosition.x + GroundLength(groundID), 0f, 0f);
        }

    }

    public int GroundLength(int id)
    {
        switch (id)
        {
            default: return 0; 
                case 0: return 7; 
                case 1: return 12;
                case 2: return 16; 
                case 3: return 21;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.position, endPosition) < 50f)
        {
            float groundD = Random.Range(3f, 4f);
            nextPosition = new Vector3(endPosition.x + groundD, 0f, 0f);
            int groundID = Random.Range(0, lstGround.Count);
            int enemyID = Random.Range(0, lstEnemy.Count);

            // Instantiate sinh Prefabs 
            GameObject nGround = Instantiate(lstGround[groundID], nextPosition, Quaternion.identity, transform);
            lstOldGrounds.Add(nGround);

            // Instantiate sinh enemy
            Instantiate(lstEnemy[enemyID], new Vector3(nextPosition.x + Random.Range(5f, GroundLength(groundID)), 0f, 0f), Quaternion.identity);

            endPosition = new Vector3(nextPosition.x + GroundLength(groundID), 0f, 0f);
        }
        if (Vector2.Distance(player.position, lstOldGrounds[0].transform.position) > 50f)
        {

            GameObject objRemoval = lstOldGrounds[0];
            lstOldGrounds.RemoveAt(0);
            GameObject.Destroy(objRemoval);
        }

    }


}
