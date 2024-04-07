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
    public List<GameObject> lstOldEnemy;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = new Vector3(21f, 0f, 0f);
        for (int i = 0; i < 2; i++)
        {
            float groundD = Random.Range(3f, 4f);
            nextPosition = new Vector3(endPosition.x + groundD, 0f, 0f);
            int groundID = Random.Range(0, lstGround.Count);



            // Instantiate sinh Prefabs 
            GameObject nGround = Instantiate(lstGround[groundID], nextPosition, Quaternion.identity, transform);
            lstOldGrounds.Add(nGround);
            endPosition = new Vector3(nextPosition.x + GroundLength(groundID), 0f, 0f);

            if (groundID == 0 || groundID == 1)
            {
                spawnPlant();

            }
            else
            {
                int spawnRandom = Random.Range(0, 2);
                if (spawnRandom == 0)
                {
                    spawnBoar();
                }
                else
                {
                    spawnBoarLeft();
                }
            }
        }

    }

    public void spawnPlant()
    {
        // Instantiate sinh enemy
        GameObject enemy = Instantiate(lstEnemy[0], new Vector3(Random.Range(nextPosition.x + 5f, endPosition.x - 5f), 0, 0f), Quaternion.identity);
        lstOldEnemy.Add(enemy);
    }

    public void spawnBoar()
    {
        // Instantiate sinh enemy
        GameObject enemy = Instantiate(lstEnemy[1], new Vector3(Random.Range(nextPosition.x + 2f, endPosition.x - 2f), 0, 0f), Quaternion.identity);
        lstOldEnemy.Add(enemy);
    }

    public void spawnBoarLeft()
    {
        // Instantiate sinh enemy
        GameObject enemy = Instantiate(lstEnemy[2], new Vector3(Random.Range(endPosition.x - 5f, endPosition.x - 2f), 0, 0f), Quaternion.identity);
        lstOldEnemy.Add(enemy);
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
        if (Vector2.Distance(player.position, endPosition) < 50f)
        {
            float groundD = Random.Range(3f, 4f);
            nextPosition = new Vector3(endPosition.x + groundD, 0f, 0f);
            int groundID = Random.Range(0, lstGround.Count);

            // Instantiate sinh Prefabs 
            GameObject nGround = Instantiate(lstGround[groundID], nextPosition, Quaternion.identity, transform);
            lstOldGrounds.Add(nGround);

            endPosition = new Vector3(nextPosition.x + GroundLength(groundID), 0f, 0f);

            if (groundID == 0 || groundID == 1)
            {
                spawnPlant();
            }

            if (groundID == 2 || groundID == 3)
            {
                int spawnRandom = Random.Range(0, 2);
                if (spawnRandom == 0)
                {
                    spawnBoar();
                }
                else
                {
                    spawnBoarLeft();
                }
            }
        }
        if (Vector2.Distance(player.position, lstOldGrounds[0].transform.position) > 50f)
        {

            GameObject objRemoval = lstOldGrounds[0];
            lstOldGrounds.RemoveAt(0);
            GameObject.Destroy(objRemoval);
        }

        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    DestroyAllMonster();
        //}

    }

    public void DestroyAllMonster()
    {
        if (lstEnemy.Count > 0)
        {
            //foreach (GameObject gameObject in lstEnemy)
            //{
            //    //GameObject objRemoval = gameObject;
            //    lstOldEnemy.Remove(gameObject);
            //    GameObject.Destroy(gameObject);
            //}
        }

    }

    // nên chia ra spawn từng loại cho dễ dàng điều chỉnh vị trí xuất hiện các thứ :)

}
