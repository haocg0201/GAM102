using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    // 5->8s sinh coin, amount 5-15 coins,the position spawn is in front of player, sinh ngoai man hinh nguoi choi, player chay qua
    // ko an dc thi xoa no di
    private bool enebleSpawn;
    public Transform playerPosition;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        enebleSpawn = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (enebleSpawn)
        {
            enebleSpawn = false;
            int soluong = Random.Range(5, 21);// int thì không random ra 16 (do nguyên 0. => .9 còn float thì ra 16) 
            //
            int coinPositionX =Mathf.RoundToInt(playerPosition.position.x + Random.Range(20f, 25f));
            float coinPositionY = Mathf.Sin(coinPositionX) + 1.5f;
            for (int i = 0; i < soluong; i++)
            {
                Instantiate(coin, new Vector3(coinPositionX,coinPositionY,0),Quaternion.identity);
                coinPositionX++;
                coinPositionY = Mathf.Sin(coinPositionX)+1.5f;
            }
            StartCoroutine(WaitForSpawnCoin());
        }
    }

    IEnumerator WaitForSpawnCoin()
    {
        float timer = Random.Range(5f, 10f);
        yield return new WaitForSeconds(timer);
        enebleSpawn = true;
    }
}
